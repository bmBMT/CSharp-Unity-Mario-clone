using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private new Camera camera;
    private new Rigidbody2D rigidbody;
    private new Collider2D collider;

    private Vector2 velocity;
    private float inputAxis;
    
    public float moveSpeed = 6f;
    public float maxJumpHight = 4f;
    public float maxJumpTime = 1f;

    public float jumpForce => (2f * maxJumpHight) / (maxJumpTime / 2f); 
    public float gravity => (-2f * maxJumpHight) / Mathf.Pow((maxJumpTime / 2f), 2);

    public bool grounded { get; private set; } // публичный геттер, приватный сеттер // состояние нахождения на земле
    public bool jumping { get; private set; } // -//- // состояние прыжка
    // геттер нужен для получения значения поля в том числе из других скриптов, сеттер изменения поля
    public bool running => velocity.x != 0; // состояние бега
    public bool sliding => (inputAxis > 0f && velocity.x < 0f) || (inputAxis < 0f && velocity.x > 0f); // состояние изменения направления движения

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>(); // присваиваение rigidbody значения компонента Rigidbody2D
        collider = GetComponent<Collider2D>();
        camera = Camera.main; // назначение свойства
    }

    private void OnEnable()
    {
        rigidbody.isKinematic = false;
        collider.enabled = true;
    }

    private void OnDisable()
    {
        rigidbody.isKinematic = true;
        collider.enabled = false;
        jumping = false;
    }

    private void Update()
    {
        if (Time.timeScale != 0) {
            HorizontalMovement();
            
            grounded = rigidbody.Raycast(Vector2.down); // проверка на нахождения персонажа на земле

            if (grounded) {
                GroundedMovement();
            }

            ApplyGravity(); // применение гравитации
        }
    }

    private void HorizontalMovement()
    {
        Vector2 position = rigidbody.position;

        inputAxis = Input.GetAxis("Horizontal");// входная ось скорости назначается путем получения доступа к вхоному классу и получения его значения
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime); // назначение скорости
    
        if (rigidbody.Raycast(Vector2.right * velocity.x)) { // если удар по твердому телу произошел, выполняется действие
            velocity.x = 0f;
        }

        if (inputAxis == 0 || sliding) {
            velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed/2, moveSpeed * Time.deltaTime);
        }

        if (velocity.x > 0f) { // если движется в право
            transform.eulerAngles = Vector3.zero; // смотрит на право
        } else if (velocity.x < 0f) { // если движется в лево
            transform.eulerAngles = new Vector3(0f, 180f, 0f); // смотрит на лево
        }
    }

    private void GroundedMovement()
    {
        velocity.y = Mathf.Max(velocity.y, 0f); /* ограничение изменения по оси Y (либо она будет равна 0 при нахождении на замле) либо (будет 
        больше 0 при нахождении в воздухе) */
        
        jumping = velocity.y > 0f; // установление статуса прыжка условием: изменение по оси Y больше нуля

        if (Input.GetButtonDown("Jump")) // если вводимая кнопка отвечает за команду "прыжок", то выполняются действия
        {
            velocity.y = jumpForce; // присвоение изменения по оси Y
            jumping = true; // присвоение персонажу статуса прыжка
        }
    }

    private void ApplyGravity()
    {
        bool falling = velocity.y < 0f || !Input.GetButton("Jump"); /* присвоение статуса падения если (изменение по оси Y ниже нуля) или 
        (отжата кнопка отвечающая за прыжок) */
        float multiplier = falling ? 2f : 1f;

        velocity.y += gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravity / 2f);
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position; // сохранение нашей текущей позиции
        position += velocity * Time.fixedDeltaTime; //  изменение позиции

        Vector2 leftEdge = camera.ScreenToWorldPoint(Vector2.zero); // присвоение переменной значение границы по левой стороне
        Vector2 rightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)); // -//- по правой стороне
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f); // ограничение Mathf.Clamp(позиция игрока, начало

        if (position.x == leftEdge.x + 0.5f || position.x == rightEdge.x - 0.5f) {
            velocity.x = 0f;
        }

        rigidbody.MovePosition(position); // перемещение твердого тела в определенное положение
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) // если персонаж приземлился на объекта типа Enemy (враг)
        {
            if (transform.DotTest(collision.transform, Vector2.down)) // проверка на приземлеине объекта
            {
                velocity.y = jumpForce / 2f; // изменение позиции объекта позиции по оси Y (соверешение прыжка)
                jumping = true; // включение анимации прыжка
            }
        }
        else if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUp")) // если слой маски не равен "PowerUp", то выполняется действие
        {
            if (transform.DotTest(collision.transform, Vector2.up)) {
                velocity.y = 0f; // скорость изменения по оси Y = 0
            }
        }
    }
}
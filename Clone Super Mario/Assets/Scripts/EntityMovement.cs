using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    public float speed = 2f; // скорость передвижения
    public Vector2 direction = Vector2.left; // направление движения

    private new Rigidbody2D rigidbody;  // ссылка на твердое тело
    private Vector2 velocity; // скорость изменения по оси

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>(); // получение компонента твердого тела
        enabled = false; // отключение передвижения
    }

    private void OnBecameVisible()
    {
        enabled = true; // запуск передвижения при видимости объекта в камере
    }

    private void OnBecameInvisible()
    {
        enabled = false; // отключение передвижения когда в камере объект не виден
    }

    private void OnEnable()
    {
        rigidbody.WakeUp();
    }

    private void OnDisable()
    {
        rigidbody.velocity = Vector2.zero; // в отключенном состоянии скорость передвижения приравнивается к нулю
        rigidbody.Sleep();
    }

    private void FixedUpdate()
    {
        velocity.x = direction.x * speed; // установление изменения по оси X
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime; // установление изменения по оси  Y

        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime); // изменение позиции объекта в течении времени

        if (rigidbody.Raycast(direction)) {
            direction = -direction; // если задел стену изменяет направление передвижения

            if(direction == Vector2.left) {
                transform.eulerAngles = Vector3.zero;
            } else if(direction == Vector2.right) {
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }

            if (rigidbody.Raycast(Vector2.down)) {
                velocity.y = Mathf.Max(velocity.y, 0f);
            }
        }
    }

}

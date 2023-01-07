using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite shellSprite; // спрайт сплющенного вида
    public float shellSpeed = 12f; // скорость толчка

    private bool shelled; // сплющенное состояние
    private bool pushed; // состояние толчка

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!shelled && collision.gameObject.CompareTag("Player")) /* если объект не находится в сплющенном состоянии и столкнувшаяся коллизия 
        принадлежит игроку, то выполняются действия */
        {
            Player player = collision.gameObject.GetComponent<Player>(); // получение ссылки на скрипт "Player"
                 
            if (player.starpower)
            {
                Hit();
            }        
            else if (collision.transform.DotTest(transform, Vector2.down)) { // если коллизия марио ударила по объекту нижней частью
                EnterShell(); // вызов функции EnterShell
            } else {
                player.Hit(); // вызов функции Hit() в скрипте "Player"
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) // функция выполнения при сталкивание с коллизией
    {
        if (shelled && other.CompareTag("Player")) // если объект находится в сплющенном состоянии и действие произошло с игроком, ...
        {
            if (!pushed) // если объект не в состоянии толчка, ...
            {
                Vector2 direction = new Vector2(transform.position.x - other.transform.position.x, 0f); 
                PushShell(direction); // толчок (направление)
            }
            else 
            {
                Player player = other.GetComponent<Player>();
                
                if (player.starpower) {
                    Hit();
                }
                else {
                    player.Hit();
                }
            }
        }
        else if (!shelled && other.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Hit();
        }
    }

    private void EnterShell()
    {
        shelled = true; // присваивание состояния
        
        GetComponent<EntityMovement>().enabled = false; // отключение движения
        GetComponent<AnimatedSprite>().enabled = false; // отключение анимации
        GetComponent<SpriteRenderer>().sprite = shellSprite; // изменение рендерируемого объекта
        
        GameManager.Instance.AddScore(100);
    }

    private void PushShell(Vector2 direction)
    {
        pushed = true; // присваивание состояния

        GetComponent<Rigidbody2D>().isKinematic = false;

        EntityMovement movement = GetComponent<EntityMovement>(); // ссылка на скрипт EntityMovement
        movement.direction = direction.normalized; // установка направления движения
        movement.speed = shellSpeed; // -//- скорости движения
        movement.enabled = true; // запуск движения

        gameObject.layer = LayerMask.NameToLayer("Shell"); // изменения слоя маски с Enemy на Shell

        GameManager.Instance.AddScore(100);
    }

    private void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false; // отключение анимации
        GetComponent<SpriteRenderer>().sprite = shellSprite;
        gameObject.transform.eulerAngles = new Vector3(180f, 0f, 0f);
        GetComponent<DeathAnimation>().enabled = true; // запуск анимации смерти
        Destroy(gameObject, 3f); // удаление объекта через 3 секунды
        
        GameManager.Instance.AddScore(100);
    }

    private void OnBecameInvisible() // функция выполнения когда объект не виден игроку
    {
        if (pushed) {
            Destroy(gameObject); // удаление объекта
        }
    }
}

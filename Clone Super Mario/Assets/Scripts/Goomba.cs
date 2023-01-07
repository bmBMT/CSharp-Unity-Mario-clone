using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Sprite flatSprite;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // если тег объекта Player
        {
            Player player = collision.gameObject.GetComponent<Player>();
            
            if (player.starpower)
            {
                Hit();
            }        
            else if (collision.transform.DotTest(transform, Vector2.down)) { // если коллизия марио ударила по объекту нижней частью
                Flatten(); // вызов функции Flatten
            } else {
                player.Hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shell")) {
            Hit();
        }
    }

    private void Flatten()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = flatSprite; // замена рендерируемого объекта
        Destroy(gameObject, 0.5f); // удаления объекта через 0.5 секунд после уничтожения
        GameManager.Instance.AddScore(100);
    }

    private void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        gameObject.transform.eulerAngles = new Vector3(180f, 0f, 0f);
        Destroy(gameObject, 3f);
        GameManager.Instance.AddScore(100);
    }
}

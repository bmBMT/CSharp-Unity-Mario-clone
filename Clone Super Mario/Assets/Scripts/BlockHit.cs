using System.Collections;
using UnityEngine;

public class BlockHit : MonoBehaviour
{
    public GameObject item;
    public Sprite emptyBlock; // спрайт уничтоженного блока
    public int maxHits = -1; // баксимальное количество ударов по блоку

    private bool animating; // состояние анимирования

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!animating && maxHits != 0 && collision.gameObject.CompareTag("Player")) /* условие будет выполняться если блок не в состоянии 
        анимирования, не уничтожен (максимально кол-во ударов не равно 0), столкнувшийся коллайдер принадлежит игроку */
        {
            if (collision.transform.DotTest(transform, Vector2.up)) { // если произошел удар верхней части объекта игрока
                Hit();
            }
        }
    }

    private void Hit()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>(); // ссылка на компонент рендерируемого спрайта
        spriteRenderer.enabled = true;

        maxHits--;

        if (maxHits == 0) {
            spriteRenderer.sprite = emptyBlock; // замена рендерируемого спрайта на спрайт уничтоженного блока
        }
    
        if (item != null) {
            Instantiate(item, transform.position, Quaternion.identity);
        }

        StartCoroutine(Animate()); // запуск анимации после удара по блоку
    }

    private IEnumerator Animate()
    {
        animating = true; // присвоение состояния анимировния

        Vector3 restingPosition = transform.localPosition; // получение начальной позиции объекта
        Vector3 animatedPosition = restingPosition + Vector3.up * 0.5f ; // анимированная позиция

        yield return Move(restingPosition, animatedPosition); // движение вверх
        yield return Move(animatedPosition, restingPosition); // возвращение на начальную позицию

        animating = false;
    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsed = 0f; // время которое прошло с начала анимации
        float duration = 0.125f; // продолжительность анимации

        while(elapsed < duration)
        {
            float t = elapsed / duration; // процент времени в анимации

            transform.localPosition = Vector3.Lerp(from, to, t); // интерполяция между двумя позициями
            elapsed += Time.deltaTime; // прибавление времени после начала анимации

            yield return null;
        }
    
        transform.localPosition = to;
    }
}

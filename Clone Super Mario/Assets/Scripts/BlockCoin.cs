using System.Collections;
using UnityEngine;

public class BlockCoin : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.AddCoin();

        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        Vector3 restingPosition = transform.localPosition; // получение начальной позиции объекта
        Vector3 animatedPosition = restingPosition + Vector3.up * 2f ; // анимированная позиция

        yield return Move(restingPosition, animatedPosition); // движение вверх
        yield return Move(animatedPosition, restingPosition); // возвращение на начальную позицию

        Destroy(gameObject);
    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsed = 0f; // время которое прошло с начала анимации
        float duration = 0.25f; // продолжительность анимации

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

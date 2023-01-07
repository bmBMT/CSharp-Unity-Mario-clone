using System.Collections;
using UnityEngine;

public class BlockItem : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
        BoxCollider2D triggerCollider = GetComponent<BoxCollider2D>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        rigidbody.isKinematic = true; 
        circleCollider.enabled = false; // отключение физического коллайдера
        triggerCollider.enabled = false; // отключение триггера
        spriteRenderer.enabled = false; // отключение рендера спрайта

        yield return new WaitForSeconds(0.25f); // ожидание 0.25 секунд

        spriteRenderer.enabled = true; // включение рендера спрайта

        Vector3 startPosition = transform.localPosition; // получение начальной позиции
        Vector3 endPosition = transform.localPosition + Vector3.up; // получение конечной позиции

        float elapsed = 0f;
        float duration = 0.5f;

        while(elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(startPosition, endPosition, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = endPosition;
        rigidbody.isKinematic = false;
        circleCollider.enabled = true;
        triggerCollider.enabled = true;
    }
}

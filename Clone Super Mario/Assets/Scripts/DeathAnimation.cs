using System.Collections;
using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // ссылка на рендерируемый спрайт
    public Sprite deadSprite; // ссылка на спрайт при смерти

    private void Reset()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();        
    }

    private void OnEnable()
    {
        UpdateSprite(); // вызов функции
        DisablePhysics(); // -//-
        StartCoroutine(Animate()); // запуск подпрограммы
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void UpdateSprite()
    {
        spriteRenderer.enabled = true; // активация рендера спрайта
        spriteRenderer.sortingOrder = 10; // изменение порядка в слое на 10

        if (deadSprite != null) { // если к переменной deadSprite привязан определенный спрайт, выполняется дествие
            spriteRenderer.sprite = deadSprite; // передача спрайта deadSprite на рендер
        }
    }

    private void DisablePhysics()
    {
        Collider2D[] colliders = GetComponents<Collider2D>(); // получение коллайдеров объекта

        foreach (Collider2D collider in colliders) { 
            collider.enabled = false; // отключение каждого коллайдера находящегося в массиве коллайдеров объекта
        }

        GetComponent<Rigidbody2D>().isKinematic = true; // отключение влияния физического движка на объект

        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        EntityMovement entityMovement = GetComponent<EntityMovement>();

        if (playerMovement != null) {
            playerMovement.enabled = false; // отключение движения персонажа при смерти
        }

        if (entityMovement != null) {
            entityMovement.enabled = false; // -//- мобов (врагов) при смерти
        }
    }

    private IEnumerator Animate()
    {
        float elapsed = 0f; // время прошедшее с начала анимации
        float duration = 3f; // продолжительность анимации

        float jumpVelocity = 10f;
        float gravity = -36f; // гравитация

        Vector3 velocity = Vector3.up * jumpVelocity; // установление скорости

        while (elapsed < duration) 
        {
            transform.position += velocity * Time.deltaTime; // совершеие прыжка
            velocity.y += gravity * Time.deltaTime; // падение
            elapsed += Time.deltaTime; // прибавление времени после начала анимации
            yield return null;
        }
    }
}

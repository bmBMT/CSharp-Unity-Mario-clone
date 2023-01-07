using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] sprites; // хранение спрайтов
    public float framerate = 1f / 6f; // частота кадров

    private SpriteRenderer spriteRenderer; // ссылка на средство визуализации спрайтов
    private int frame; // индексация спрайтов в массиве sprites

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // получение компонента визуализации спрайтов
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(Animate), framerate, framerate); // запуск анимации
    }

    private void OnDisable()
    {
        CancelInvoke(); // отключение анимации
    }

    private void Animate()
    {
        frame++; // увеличение значения итерации (цикличность)

        if (frame >= sprites.Length) { // если индекс больше количества спрайтов, индекс = 0
            frame = 0;
        }
        
        if (frame >= 0 && frame < sprites.Length) { // если индекс не меньше 0 и находится в пределах длины количества спрайтов запустится анимация
            spriteRenderer.sprite = sprites[frame];
        }
    }
}

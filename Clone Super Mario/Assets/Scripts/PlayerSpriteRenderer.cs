using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; } // переменая хранящая рендер спрайт 
    private PlayerMovement movement; // переменная хранящая состояние движения персонажа

    public Sprite idle; // спрайт стандартного состояние
    public Sprite jump; // -//- прыжка
    public Sprite slide; // -//- изменения направления
    public AnimatedSprite run; // -//- бега

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // получение рендер спрайт
        movement = GetComponentInParent<PlayerMovement>(); // получение состояние движения персонажа
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
        run.enabled = false;
    }

    private void LateUpdate()
    {
        run.enabled = movement.running;
        
        if (movement.jumping) { // при состоянии прыжка рендерится спрайт прыжка
            spriteRenderer.sprite = jump;
        } else if (movement.sliding) { // -//- изменения направления рендерится соответствующий спрайт
            spriteRenderer.sprite = slide;
        } else if (!movement.running){ // при неподвижном состоянии рендерится спрайт idle
            spriteRenderer.sprite = idle;
        }
    }   
}

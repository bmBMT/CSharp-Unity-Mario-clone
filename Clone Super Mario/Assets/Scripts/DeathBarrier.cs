using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) // функция выполнения при достижения триггера
    {
        if (other.CompareTag("Player")) // если другой объект - игрок, ...
        {
            other.gameObject.SetActive(false); // отключение объекта игрока
            GameManager.Instance.ResetLevel(3f); // обращение к гейм менеджеру для перезагрузки уровня
        }
        else // если другой объект
        {
            Destroy(other.gameObject); // уничтожение объекта
        }
    }
}

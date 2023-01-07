using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform player;

    public float height = 24f;
    public float undergroundHeight = 8.5f;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 cameraPosition = transform.position; // получение текущей позиции камеры
        if (cameraPosition.x < 211.56) {
            cameraPosition.x = Mathf.Max(cameraPosition.x, player.position.x); /* изменение переменной с позицией камеры (ограничение по пройденному пути
            камеры) */
        }
        transform.position = cameraPosition; // изменение позиции самой камеры
    }

    public  void SetUnderground(bool underground)
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.y = underground ? undergroundHeight : height;
        transform.position = cameraPosition;
    }
}

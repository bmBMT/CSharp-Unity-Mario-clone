using UnityEngine;

public class BridgeMovement : MonoBehaviour
{
    public float speed = 2f;

    public Vector3 direction;
    public float time;

    private float localTime;

    private void Start() {
        localTime = time;
    }

    void FixedUpdate()
    {
        transform.Translate(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime, 0);
        localTime -= Time.deltaTime;

    if (localTime <= 0){
        direction = -direction;
        localTime = time;
    }
    }
}
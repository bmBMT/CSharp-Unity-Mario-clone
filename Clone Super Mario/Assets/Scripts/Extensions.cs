using UnityEngine;

public static class Extensions
{
    private static LayerMask layerMask = LayerMask.GetMask("Default"); // указание слоя проверки удара круга

    public static bool Raycast(this Rigidbody2D rigidbody, Vector2 direction)
    {
        if (rigidbody.isKinematic) { // если физическией движок контролирует твердое тело персонажа, то возвращает false
            return false;
        }

        float radius = 0.25f; // радуис отбрасывания
        float distance = 0.375f; // дистанция отбрасывания

        RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position, radius, direction.normalized, distance, layerMask);
        return hit.collider != null && hit.rigidbody != rigidbody;
    }

    public static bool DotTest(this Transform transform, Transform other, Vector2 testDirection) 
    {
        Vector2 direction = other.position - transform.position; // направление к блоку относительно персонажа
        return Vector2.Dot(direction.normalized, testDirection) > 0.10f; //возвращает true если если скалярное произведение двух векторов больше 0.25
    }
}

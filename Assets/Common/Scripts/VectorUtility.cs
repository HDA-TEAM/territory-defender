using UnityEngine;

public class VectorUtility
{
    public static Vector3 Format3dTo2dZeroZ(Vector3 vector3)
    {
        vector3.z = 0;
        return  vector3;
    }
    public static bool IsTwoPointReached(Vector2 pointA,Vector2 pointB)
    {
        if (Vector2.Distance(pointA, pointB) < 0.5f)
        {
            return true;
        }
        return false;
    }
    public static Vector2 Vector2MovingAToB(Vector2 pointA,Vector2 pointB, float movementSpeed)
    {
        return Vector2.MoveTowards(
            pointA,
            pointB,
            Time.deltaTime * movementSpeed * 10f);
    }
}

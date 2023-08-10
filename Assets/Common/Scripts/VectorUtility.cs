using UnityEngine;

public static class VectorUtility
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
            Time.deltaTime * movementSpeed * 200f);
    }
    public static float GetZAngleOfTwoPoint(Vector3 startPoint, Vector3 endPoint)
    {
        Vector3 direction = endPoint - startPoint;

        // Calculate the angle between the direction vector and the forward vector (Z-axis).
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return angle;
    }
}

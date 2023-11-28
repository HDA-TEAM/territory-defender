using UnityEngine;

public static class VectorUtility
{
    public static Vector3 Format3dTo2dZeroZ(Vector3 vector3)
    {
        vector3.z = 0;
        return  vector3;
    }
    public static bool IsTwoPointReached(Vector3 pointA,Vector3 pointB)
    {
        if (Vector3.Distance(pointA, pointB) < 0.5f)
        {
            return true;
        }
        return false;
    }
    public static Vector3 Vector3MovingAToB(Vector3 pointA,Vector3 pointB, float movementSpeed)
    {
        return Vector3.MoveTowards(
            pointA,
            pointB,
            Time.deltaTime * movementSpeed);
    }
    public static float GetZAngleOfTwoPoint(Vector3 startPoint, Vector3 endPoint)
    {
        Vector3 direction = endPoint - startPoint;

        // Calculate the angle between the direction vector and the forward vector (Z-axis).
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return angle;
    }
}

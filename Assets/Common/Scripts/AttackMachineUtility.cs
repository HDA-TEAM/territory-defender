using Unity.VisualScripting;
using UnityEngine;

public static class AttackMachineUtility
{
        public static float GetCooldownTime(float minSpeed, float maxSpeed)
        {
                return Random.Range(minSpeed, maxSpeed);
        }
}

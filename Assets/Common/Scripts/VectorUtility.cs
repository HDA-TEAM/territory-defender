using UnityEngine;

namespace GamePlay.Scripts.Tower
{
    public class VectorUtility
    {
        public static Vector3 Format3dTo2dZeroZ(Vector3 vector3)
        {
            vector3.z = 0;
            return  vector3;
        }
    }
}

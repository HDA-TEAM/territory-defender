using UnityEngine;

namespace GamePlay.Scripts.Tower
{
    public class GameObjectUtility
    {
        public static float Distance2dOfTwoGameObject(GameObject goA, GameObject goB)
        {
            return Vector2.Distance(goA.transform.position, goB.transform.position);
        }
    }
}

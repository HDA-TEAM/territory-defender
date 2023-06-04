using UnityEngine;

namespace Stage
{
    [CreateAssetMenu(fileName = "TowerKitOS", menuName = "ScriptableObject/Stage/TowerKitOS")]
    public class TowerKitOS : ScriptableObject
    {
        Vector2 _place;
        int _stage;
        public Vector2 Place() => _place;
        public int Stage () => _stage;
    }
}

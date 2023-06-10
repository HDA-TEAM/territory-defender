using UnityEngine;
using GamePlay.Scripts;
[CreateAssetMenu(fileName = "TowerKitConfig", menuName = "ScriptableObject/Stage/TowerKitConfig")]
public class TowerKitConfig : ScriptableObject
{
    Vector2 _place;
    int _stage;
    public Vector2 Place() => _place;
    public int Stage () => _stage;
}

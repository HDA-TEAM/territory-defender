using GamePlay.Scripts.Datas;
using UnityEngine;

public enum TowerId
{
    ArcherTower = 1,
    SpearTower = 20,
    ElephantTower = 40,
    DrumTower = 60
}

[CreateAssetMenu(fileName = "TowerDataConfig", menuName = "ScriptableObject/Configs/TowerDataConfig")]
public class TowerDataConfig : SingleUnitDataConfig<TowerId>
{
}

using UnityEngine;

namespace GamePlay.Scripts.Data
{
    [CreateAssetMenu(fileName = "TowerDataConfig", menuName = "ScriptableObject/Configs/TowerDataConfig")]
    public class TowerDataConfig : SingleUnitDataConfig<UnitId.Tower>
    {
        // Tạo class bọc CommonTowerConfig
    }
}
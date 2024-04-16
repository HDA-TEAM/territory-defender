using System;
using UnityEngine;

namespace GamePlay.Scripts.Data
{
    [Serializable]
    public struct UnitDataComposite
    {
        public UnitBase UnitBase;
    } 

    [CreateAssetMenu(fileName = "TotalUnitDataConfig", menuName = "ScriptableObject/Common/Configs/TotalUnitDataConfig")]
    public class TotalUnitDataConfig : ScriptableObject
    {
        [SerializeField] private TowerDataConfig _towerDataConfig;
        [SerializeField] private AllyTroopsDataConfig _allyTroopsDataConfig;
        [SerializeField] private EnemyDataConfig _enemyDataConfig;
        [SerializeField] private InGameHeroDataConfig _heroDataConfig;
    }
}
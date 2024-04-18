using System;
using UnityEngine;
using UnityEngine.Serialization;

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
        [FormerlySerializedAs("_towerDataConfig")]
        [SerializeField] private TowerDataConfigBase _towerDataConfigBase;
        [FormerlySerializedAs("_allyTroopsDataConfig")]
        [SerializeField] private AllyTroopsDataConfigBase _allyTroopsDataConfigBase;
        [FormerlySerializedAs("_enemyDataConfig")]
        [SerializeField] private EnemyDataConfigBase _enemyDataConfigBase;
        [FormerlySerializedAs("_heroDataConfig")]
        [SerializeField] private InGameHeroDataConfigBase _heroDataConfigBase;

        #region Access
        public UnitDataComposite GetSingleUnitDataConfig(string key)
        {
            if (Enum.TryParse(key, out UnitId.Tower towerKey))
                return _towerDataConfigBase.GetUnitConfigById(towerKey);
            
            if (Enum.TryParse(key, out UnitId.Ally allyKey))
                return _allyTroopsDataConfigBase.GetUnitConfigById(allyKey);
            
            if (Enum.TryParse(key, out UnitId.Enemy enemyKey))
                return _enemyDataConfigBase.GetUnitConfigById(enemyKey);
            
            if (Enum.TryParse(key, out UnitId.Hero heroKey))
                return _heroDataConfigBase.GetUnitConfigById(heroKey);

            return new UnitDataComposite();
        }
        public TowerDataConfigBase TowerDataConfigBase
        {
            get
            {
                return _towerDataConfigBase;
            }
        }
        public AllyTroopsDataConfigBase AllyTroopsDataConfigBase
        {
            get
            {
                return _allyTroopsDataConfigBase;
            }
        }
        public EnemyDataConfigBase EnemyDataConfigBase
        {
            get
            {
                return _enemyDataConfigBase;
            }
        }
        public InGameHeroDataConfigBase HeroDataConfigBase
        {
            get
            {
                return _heroDataConfigBase;
            }
        }
        
        #endregion
    }
}
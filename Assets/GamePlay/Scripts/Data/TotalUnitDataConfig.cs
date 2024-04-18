using System;
using UnityEngine;
using UnityEngine.UI;

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

        #region Access
        public UnitDataComposite GetSingleUnitDataConfig(string key)
        {
            if (Enum.TryParse(key, out UnitId.Tower towerKey))
                return _towerDataConfig.GetUnitConfigById(towerKey);
            
            if (Enum.TryParse(key, out UnitId.Ally allyKey))
                return _allyTroopsDataConfig.GetUnitConfigById(allyKey);
            
            if (Enum.TryParse(key, out UnitId.Enemy enemyKey))
                return _enemyDataConfig.GetUnitConfigById(enemyKey);
            
            if (Enum.TryParse(key, out UnitId.Hero heroKey))
                return _heroDataConfig.GetUnitConfigById(heroKey);

            return new UnitDataComposite();
        }
        public TowerDataConfig TowerDataConfig
        {
            get
            {
                return _towerDataConfig;
            }
        }
        public AllyTroopsDataConfig AllyTroopsDataConfig
        {
            get
            {
                return _allyTroopsDataConfig;
            }
        }
        public EnemyDataConfig EnemyDataConfig
        {
            get
            {
                return _enemyDataConfig;
            }
        }
        public InGameHeroDataConfig HeroDataConfig
        {
            get
            {
                return _heroDataConfig;
            }
        }
        
        #endregion
    }
}
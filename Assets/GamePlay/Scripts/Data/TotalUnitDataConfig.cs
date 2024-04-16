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

        #region Access
        public UnitDataComposite GetSingleUnitDataConfig(string key)
        {
            var towerKey = Enum.Parse<UnitId.Tower>(key);
            if (_towerDataConfig.IsExistUnit(towerKey))
                return _towerDataConfig.GetUnitConfigById(towerKey);
            
            var allyKey = Enum.Parse<UnitId.Ally>(key);
            if (_allyTroopsDataConfig.IsExistUnit(allyKey))
                return _allyTroopsDataConfig.GetUnitConfigById(allyKey);
            
            var enemyKey = Enum.Parse<UnitId.Enemy>(key);
            if (_enemyDataConfig.IsExistUnit(enemyKey))
                return _enemyDataConfig.GetUnitConfigById(enemyKey);
            
            var heroKey = Enum.Parse<UnitId.Hero>(key);
            if (_heroDataConfig.IsExistUnit(heroKey))
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
using Common.Loading.Scripts;
using Common.Scripts;
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
        [SerializeField] private TowerDataConfigBase _towerDataConfigBase;
        [SerializeField] private AllyTroopsDataConfigBase _allyTroopsDataConfigBase;
        [SerializeField] private EnemyDataConfigBase _enemyDataConfigBase;
        [SerializeField] private InGameHeroDataConfigBase _heroDataConfigBase;

        #region Access
        public UnitDataComposite GetSingleUnitDataConfig(string key)
        {
            if (Enum.TryParse(key, out UnitId.Tower towerKey))
                return _towerDataConfigBase.GeConfigByKey(towerKey);
            
            if (Enum.TryParse(key, out UnitId.Ally allyKey))
                return _allyTroopsDataConfigBase.GeConfigByKey(allyKey);
            
            if (Enum.TryParse(key, out UnitId.Enemy enemyKey))
                return _enemyDataConfigBase.GeConfigByKey(enemyKey);
            
            if (Enum.TryParse(key, out UnitId.Hero heroKey))
                return _heroDataConfigBase.GeConfigByKey(heroKey);

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
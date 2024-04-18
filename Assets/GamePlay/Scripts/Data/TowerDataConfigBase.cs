using Common.Scripts;
using UnityEngine;

namespace GamePlay.Scripts.Data
{
    [CreateAssetMenu(fileName = "TowerDataConfig", menuName = "ScriptableObject/Configs/TowerDataConfig")]
    public class TowerDataConfigBase : DataConfigBase<UnitId.Tower, UnitDataComposite>
    {
        [SerializeField] private NextAvailableUpgradeTowers _nextAvailableUpgradeTowers;
        public NextAvailableUpgradeTowers NextAvailableUpgradeTowers
        {
            get
            {
                return _nextAvailableUpgradeTowers;
            }
        }
    }
}
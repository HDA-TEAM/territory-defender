using UnityEngine;

namespace GamePlay.Scripts.Data
{
    [CreateAssetMenu(fileName = "TowerDataConfig", menuName = "ScriptableObject/Configs/TowerDataConfig")]
    public class TowerDataConfig : SingleUnitDataConfig<UnitId.Tower>
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
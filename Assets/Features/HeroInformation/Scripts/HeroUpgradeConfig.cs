using UnityEngine;

namespace Features.HeroInformation.Scripts
{
    [CreateAssetMenu(fileName = "HeroUpgradeConfig", menuName = "ScriptableObject/Config/HeroUpgradeConfig")]
    public class HeroUpgradeConfig : ScriptableObject
    {
        [SerializeField] private int _expUnitPerLevel;

        public int GetExpNeed(int nextLevel)
        {
            return nextLevel * _expUnitPerLevel;
        }
    }
}

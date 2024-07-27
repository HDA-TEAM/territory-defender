using Common.Scripts;
using Common.Scripts.Data.DataAsset;
using UnityEngine;

namespace GamePlay.Scripts.Character.Stats
{
    [CreateAssetMenu(fileName = "HeroStatsByLevelConfig", menuName = "ScriptableObject/HeroStatsByLevelConfig")]
    public class HeroStatsByLevel : Stats
    {
        [SerializeField] private UnitId.Hero _heroId;
        [SerializeField] private HeroDataAsset _heroDataAsset;
        public override float GetStat(StatId statId)
        {
            int curLevel = _heroDataAsset.GetHeroLevel(_heroId);
            switch (statId)
            {
                case StatId.MaxHeal:
                    {
                        float originVal = base.GetStat(statId);
                        return originVal + (curLevel - 1) * 0.05f * originVal;
                    }
                case StatId.AttackDamage:
                    {
                        float originVal = base.GetStat(statId);
                        return originVal + (curLevel - 1) * 0.05f * originVal;
                    }
                default:
                    return base.GetStat(statId);
            }
        }
    }
}

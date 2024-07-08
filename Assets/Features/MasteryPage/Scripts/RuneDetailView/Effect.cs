using AYellowpaper.SerializedCollections;
using UnityEngine;
namespace Features.MasteryPage.Scripts.Rune
{
    public enum EffectId
    {
        //Red rune effect
        Pierce = 1,
        Damage = 2,
        Crit = 3,
        
        //Yellow rune effect
        AttackSpeed = 11,
        Ranged = 12,
        Cooldown = 13,
        DoubleAttack = 14,
        
        //Purple rune effect
        Hp = 21,
        Armor = 22,
        GoldGather = 23, //Gold gather based on destroy each enemy
    }
    
    [CreateAssetMenu(fileName = "EffectDataAsset", menuName = "ScriptableObject/DataAsset/EffectDataAsset")]
    public class Effect : ScriptableObject
    {
        [SerializedDictionary("EffectStats", "int")] 
        [SerializeField] private SerializedDictionary<EffectId, int> _effectStatsDataDict = new SerializedDictionary<EffectId, int>();

        // Method to get the effect value based on the EffectStats
        public int GetEffectValue(EffectId stat)
        {
            if (_effectStatsDataDict.TryGetValue(stat, out int value))
            {
                return value;
            }
            return 0; // Consider returning a default value or handling this scenario appropriately
        }

        public SerializedDictionary<EffectId, int> GetAllEffectStats()
        {
            return _effectStatsDataDict;
        }
    }

}

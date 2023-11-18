using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(menuName = "Unit stats")]
public class Stats : ScriptableObject
{
    [SerializedDictionary("StatName", "Value")]
    [SerializeField] private SerializedDictionary<StatId, float> _statDict = new SerializedDictionary<StatId, float>();

    public float GetStat(StatId statId)
    {
        if (_statDict.TryGetValue(statId, out float res))
            return res;
        Debug.LogError($"No stat value found for key {statId} on {this.name}");
        return 0;
    }
    public float GetStat(StatId statId, int level)
    {
        if (_statDict.TryGetValue(statId, out float res))
        {
            return StatCalculatePerLevel(statId, level, res);
        }
        Debug.LogError($"No stat value found for key {statId} on {this.name} with level {level}");
        return 0;
    }
    protected virtual float StatCalculatePerLevel(StatId statId, int level, float value)
    {
        return value;
    }
}

public enum StatId
{
    /// Common
    AttackDamage = 0,
    AttackSpeed = 1,
    AttackRange = 2,
    DetectRange = 3,

    /// Characteristic
    HealingPerSecond = 100,
    MaxHeal = 101,
    Armour = 105,
    MovementSpeed = 106,
    LifeReduce = 107,
    DropCoinWhenDie = 108,
    TimeToRevive = 109,

    ///Tower
    CoinNeedToBuild = 200,
    CoinNeedToUpgrade = 201,
    TimeToSpawnUnit = 202,


}

using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(menuName = "Unit stats")]
public class Stats : ScriptableObject
{
    [SerializedDictionary("StatId", "Value")]
    [SerializeField] private SerializedDictionary<StatId, float> _statDict = new SerializedDictionary<StatId, float>();
    
    [SerializedDictionary("InformationId", "Value")]
    [SerializeField] private SerializedDictionary<InformationId, string> _informationDict = new SerializedDictionary<InformationId, string>();
    #region Stats access
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
    #endregion
    #region Information access
    public string GetInformation(InformationId informationId)
    {
        if ((_informationDict).TryGetValue(informationId, out string res))
            return res;
        Debug.LogError($"No stat value found for key {informationId} on {this.name}");
        return "";
    }
    #endregion
    
}

public enum InformationId
{
    ///Common
    Name = 0,
    Description = 1,
    
}
public enum StatId
{
    /// Common
    AttackDamage = 0,
    AttackSpeed = 1,
    AttackRange = 2,
    DetectRange = 3,
    CampingRange = 4,
    BuffRange = 4,

    /// Characteristic
    Level = 20,
    Exp = 21,
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

public enum TroopBehaviourType
{
    Tower = 0,
    Melee = 1,
    Ranger = 2,
    MeleeAndRanger = 3,
}


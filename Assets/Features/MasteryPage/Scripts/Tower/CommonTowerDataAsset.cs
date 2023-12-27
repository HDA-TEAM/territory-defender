
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "FeatureTowerDataAsset", menuName = "ScriptableObject/DataAsset/FeatureTowerDataAsset")]
public class CommonTowerDataAsset : ScriptableObject
{
    [SerializedDictionary("TowerId", "TowerType")] [SerializeField]
    private SerializedDictionary<TowerId, Stats> _towerTypeDict = new SerializedDictionary<TowerId, Stats>();
    
    private TowerId _towerId;
    public Stats GetTowerType(TowerId towerId)
    {
        _towerTypeDict.TryGetValue(towerId, out Stats towerStats);
        if (!towerStats)
        {
            Debug.LogError("Tower type not exist in dictionary");
            return _towerTypeDict[0];
        }
        return towerStats;
    }
    
    public TowerId GetTowerId(Stats stats)
    {
        foreach (var kvp in _towerTypeDict)
        {
            if (kvp.Value == stats)
            {
                _towerId = kvp.Key;
                return _towerId;
            }
        }
        return _towerId;
    }
    
    public List<Stats> GetAllTowerData()
    {
        return _towerTypeDict.Values.ToList();
    }
    
}


using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum TowerId
{
    ArcherTower = 1,
    SpearTower = 20,
    ElephantTower = 40,
    DrumTower = 60
}

[CreateAssetMenu(fileName = "TowerDataConfig", menuName = "ScriptableObject/Configs/TowerDataConfig")]
public class TowerDataConfig : ScriptableObject
{
    [SerializedDictionary("TowerId", "TowerType")]
    [SerializeField] private SerializedDictionary<TowerId, UnitBase> _towerTypeDict = new SerializedDictionary<TowerId, UnitBase>();

    public UnitBase GetTowerType(TowerId towerId)
    {
        _towerTypeDict.TryGetValue(towerId, out UnitBase towerBase);
        if (!towerBase)
        {
            Debug.LogError("Tower type not exist in dictionary");
            return _towerTypeDict[0];
        }
        return towerBase;
    }
    
    public List<UnitBase> GetAllTowerData()
    {
        return _towerTypeDict.Values.ToList();
    }
}

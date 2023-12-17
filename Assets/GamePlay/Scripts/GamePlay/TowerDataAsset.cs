using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public enum TowerId
{
    ArcherTower = 1,
    SpearTower = 20,
    ElephantTower = 40,
    DrumTower = 60
}

[CreateAssetMenu(fileName = "TowerDataAsset", menuName = "ScriptableObject/DataAsset/TowerDataAsset")]
public class TowerDataAsset : ScriptableObject
{
    [SerializedDictionary("TowerId", "TowerType")]
    [SerializeField] private SerializedDictionary<TowerId, UnitBase> _towerTypeDict = new SerializedDictionary<TowerId, UnitBase>();
    // [SerializedDictionary("KitId", "TowerKit")]
    // [SerializeField] private SerializedDictionary<int,TowerKit> _towerKits = new SerializedDictionary<int, TowerKit>();
    // public TowerKit CurrentSelectedTowerKit;
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
    // public void LoadRuntimeData(ref List<TowerKit> towerKits)
    // {
    //     _towerKits.Clear();
    //     foreach (var towerKit in towerKits)
    //     {
    //         _towerKits.Add(towerKit.TowerKitId,towerKit);
    //     }
    // }
    
    public List<UnitBase> GetAllTowerData()
    {
        return _towerTypeDict.Values.ToList();
    }
}

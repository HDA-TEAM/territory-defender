using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private SerializedDictionary<TowerId, TowerBase> _towerTypeDicts = new SerializedDictionary<TowerId, TowerBase>();
    // [SerializedDictionary("KitId", "TowerKit")]
    // [SerializeField] private SerializedDictionary<int,TowerKit> _towerKits = new SerializedDictionary<int, TowerKit>();
    // public TowerKit CurrentSelectedTowerKit;
    public TowerBase GetTowerType(TowerId towerId)
    {
        _towerTypeDicts.TryGetValue(towerId, out TowerBase towerBase);
        if (!towerBase)
        {
            Debug.LogError("Tower type not exist in dictionary");
            return _towerTypeDicts[0];
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
}

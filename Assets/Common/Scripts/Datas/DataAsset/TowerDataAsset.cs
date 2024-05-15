using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Common.Scripts;
using Common.Scripts.Datas;
using UnityEngine;
using TowerDataConfig = Features.MasteryPage.Scripts.Tower.TowerDataConfig;

[CreateAssetMenu(fileName = "TowerDataAsset", menuName = "ScriptableObject/DataAsset/TowerDataAsset")]
public class TowerDataAsset : BaseDataAsset<TowerDataModel>
{
    public void SaveTowers(SerializedDictionary<UnitId.Tower, TowerDataConfig> towerTypeDict)
    {
        List<TowerDataSaver> newTowerList = new List<TowerDataSaver>(); // Create a new list for towers
        foreach (var kvp in towerTypeDict)
        {
            if (kvp.Value != null && kvp.Value._runeLevels is { Count: > 0 })
            {
                var towerSoSaver = new TowerDataSaver
                {
                    TowerId = kvp.Key,
                    RuneLevels = kvp.Value._runeLevels
                };
                newTowerList.Add(towerSoSaver); // Add to the new list
            }
        }
        _model.TowerList = newTowerList; // Update the model's TowerList only, without overwriting the entire model
        SaveData();
    }
    
    public List<TowerDataSaver> GetTowers()
    {
        LoadData(); // Load the data from json file into _model
        //TowerList = _model.TowerList ?? new List<TowerDataSaver>();
        return _model.TowerList;
    }
}

[Serializable]
public struct TowerDataModel : IDefaultDataModel
{
    public List<TowerDataSaver> TowerList;
    public bool IsEmpty()
    {
        return (TowerList == null || TowerList.Count == 0);
    }
    public void SetDefault()
    {
        // Ensure defaults are set for both lists
        TowerList = new List<TowerDataSaver>
        {
            new TowerDataSaver
            {
                TowerId = 0, // Default Tower ID
                RuneLevels = new List<RuneLevel>() // Default empty rune levels
            }
        };
    }
}

[Serializable]
public struct TowerDataSaver
{
    public UnitId.Tower TowerId;
    public List<RuneLevel> RuneLevels;
}

[Serializable]
public struct RuneLevel
{
    public RuneId RuneId;
    public int Level;

    public RuneLevel(RuneId runeId, int level)
    {
        RuneId = runeId;
        Level = level;
    }
}

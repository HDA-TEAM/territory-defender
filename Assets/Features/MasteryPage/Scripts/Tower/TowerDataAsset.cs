using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Common.Scripts;
using UnityEngine;
using TowerDataConfig = Features.MasteryPage.Scripts.Tower.TowerDataConfig;

[CreateAssetMenu(fileName = "TowerDataAsset", menuName = "ScriptableObject/DataAsset/TowerDataAsset")]
public class TowerDataAsset : BaseDataAsset<TowerDataModel>
{
    public void SaveTowers(SerializedDictionary<UnitId.Tower, TowerDataConfig> towerTypeDict)
    {
        _model = ConvertToTowerDataModel(towerTypeDict);
        SaveData();
    }

    private TowerDataModel ConvertToTowerDataModel(SerializedDictionary<UnitId.Tower, TowerDataConfig> towerTypeDict)
    {
        List<TowerSoSaver> newTowerList = new List<TowerSoSaver>(); // Create a new list for towers
        foreach (var kvp in towerTypeDict)
        {
            if (kvp.Value != null && kvp.Value._runeLevels is { Count: > 0 })
            {
                var towerSoSaver = new TowerSoSaver
                {
                    TowerId = kvp.Key,
                    RuneLevels = kvp.Value._runeLevels
                };
                newTowerList.Add(towerSoSaver); // Add to the new list
            }
        }
        _model.TowerList = newTowerList; // Update the model's TowerList only, without overwriting the entire model

        return _model;
    }

    public List<TowerSoSaver> LoadTowers()
    {
        LoadData(); // Load the data from json file into _model
        return _model.TowerList;
    }
}

[Serializable]
public struct TowerDataModel : IDefaultDataModel
{
    public List<TowerSoSaver> TowerList;

    public bool IsEmpty()
    {
        return (TowerList == null || TowerList.Count == 0);
    }

    public void SetDefault()
    {
        // Ensure defaults are set for both lists
        TowerList = new List<TowerSoSaver>
        {
            new TowerSoSaver
            {
                TowerId = 0, // Default Tower ID
                RuneLevels = new List<RuneLevel>() // Default empty rune levels
            }
        };
    }
}

[Serializable]
public struct TowerSoSaver
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

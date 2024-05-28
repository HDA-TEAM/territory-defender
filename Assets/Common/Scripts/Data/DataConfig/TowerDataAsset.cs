using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Common.Scripts;
using Common.Scripts.Data.DataAsset;
using Common.Scripts.Data.DataConfig;
using GamePlay.Scripts.Data;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerDataAsset", menuName = "ScriptableObject/DataAsset/TowerDataAsset")]
public class TowerDataAsset : LocalDataAsset<TowerDataModel>
{
    [SerializedDictionary("TowerId", "TowerDataConfig")] 
    public SerializedDictionary<UnitId.Tower, TowerDataConfig> _towerTypeDict = new SerializedDictionary<UnitId.Tower, TowerDataConfig>();

    [SerializeField] private List<TowerDataConfigBase> _towerDataConfigBases;
    public int _returnStar;
    
    public TowerDataConfig GetTower(UnitId.Tower towerId)
    {
        _towerTypeDict.TryGetValue(towerId, out TowerDataConfig tower);
        if (!tower)
        {
            Debug.LogError("Tower type not exist in dictionary");
            return _towerTypeDict[0];
        }
        return tower;
    }

    public List<TowerDataConfig> GetAllTowerDataConfig()
    {
        return _towerTypeDict.Values.ToList();
    }
    private void LoadTowerDataFromLocal(List<TowerDataSaver> towerDataSavers)
    {
        //_towerTypeDict.Clear(); // Clear existing data

        foreach (var saver in towerDataSavers)
        {
            if (!_towerTypeDict.ContainsKey(saver.TowerId))
            {
                // Create a new TowerDataConfig instance and initialize it
                TowerDataConfig towerDataConfig = ScriptableObject.CreateInstance<TowerDataConfig>();
                towerDataConfig.InitializeRune(saver.RuneLevels);
                _towerTypeDict.Add(saver.TowerId, towerDataConfig);
            }
        }
    }
    public void UpdateTowerDataConfig()
    {
        // TODO: load TowerDataConfig data
        var towerSavers = GetTowers();  // Retrieve the list of TowerDataSaver from the asset

        if (towerSavers.Count > 0)
        {
            Debug.Log("Exist towerSavers");
        }
        
        LoadTowerDataFromLocal(towerSavers);  
       // return _towerDataAsset.GetTowers();
    }
    
    // New
    public void SaveTowers(SerializedDictionary<UnitId.Tower, TowerDataConfig> towerTypeDict)
    {
        Debug.Log("SaveTowers");
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

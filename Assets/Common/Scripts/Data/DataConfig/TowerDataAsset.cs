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
    [SerializedDictionary("TowerId", "TowerDataSO")] 
    public SerializedDictionary<UnitId.Tower, TowerDataConfig> _towerTypeDict = new SerializedDictionary<UnitId.Tower, TowerDataConfig>();

    [SerializeField] private List<TowerDataConfigBase> _towerDataConfigBases;
    public int _returnStar;
    
    public List<TowerData> TowerDatas
    {
        //LoadData(); // Load the data from json file into _model
        get
        {
            LoadData();
            return _model.ListTowerDatas ??= new List<TowerData>();
        }
    }
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

    public List<TowerDataConfig> GetAllTowerData()
    {
        return _towerTypeDict.Values.ToList();
    }
    private void LoadTowerDataFromLocal(List<TowerData> towerDataSavers)
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
        var towerDatas = TowerDatas;  // Retrieve the list of TowerDataSaver from the asset
        
        if (towerDatas.Count > 0)
        {
            Debug.Log("Exist towerSavers");
        }
        LoadTowerDataFromLocal(towerDatas);
    }
    
    // New
    public void SaveTowers(SerializedDictionary<UnitId.Tower, TowerDataConfig> towerTypeDict)
    {
        List<TowerData> newTowerList = new List<TowerData>(); // Create a new list for towers
        foreach (var kvp in towerTypeDict)
        {
            if (kvp.Value != null && kvp.Value._runeLevels is { Count: > 0 })
            {
                var towerSoSaver = new TowerData
                {
                    TowerId = kvp.Key,
                    RuneLevels = kvp.Value._runeLevels
                };
                newTowerList.Add(towerSoSaver); // Add to the new list
            }
        }
        _model.ListTowerDatas = newTowerList; // Update the model's TowerList only, without overwriting the entire model
        SaveData();
    }
    
    
}

[Serializable]
public struct TowerDataModel : IDefaultDataModel
{
    public List<TowerData> ListTowerDatas;
    public bool IsEmpty()
    {
        return (ListTowerDatas == null || ListTowerDatas.Count == 0);
    }
    public void SetDefault()
    {
        // Ensure defaults are set for both lists
        ListTowerDatas = new List<TowerData>
        {
            new TowerData
            {
                TowerId = 0, // Default Tower ID
                RuneLevels = new List<RuneLevelData>() // Default empty rune levels
            }
        };
    }
}

[Serializable]
public struct TowerData
{
    public UnitId.Tower TowerId;
    public List<RuneLevelData> RuneLevels;
}

[Serializable]
public struct RuneLevelData
{
    public RuneId RuneId;
    public int Level;

    public RuneLevelData(RuneId runeId, int level)
    {
        RuneId = runeId;
        Level = level;
    }
}

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
    public SerializedDictionary<UnitId.Tower, TowerDataSo> _towerTypeDict = new SerializedDictionary<UnitId.Tower, TowerDataSo>();

    [SerializeField] private List<TowerDataConfigBase> _towerDataConfigBases;
    public int _returnStar;

    // private void Awake()
    // {
    //     this.UpdateTowerDataConfig();
    // }

    public List<TowerData> TowerDatas
    {
        // Load the data from json file into _model
        get
        {
            // Todo: Would change when LoadData() be fixed
            //     LoadData();
            
            return _model.ListTowerDatas ?? (_model.ListTowerDatas = new List<TowerData>());
        }
    }
    public TowerDataSo GetTower(UnitId.Tower towerId)
    {
        _towerTypeDict.TryGetValue(towerId, out TowerDataSo tower);
        if (!tower)
        {
            Debug.LogError("Tower type not exist in dictionary");
            return _towerTypeDict[0];
        }
        return tower;
    }

    public List<TowerDataSo> GetAllTowerData()
    {
        return _towerTypeDict.Values.ToList();
    }
    private void LoadTowerDataFromLocalToTowerDict(List<TowerData> towerDataSavers)
    {
        foreach (var saver in towerDataSavers)
        {
            if (!_towerTypeDict.ContainsKey(saver.TowerId))
            {
                // Create a new TowerDataConfig instance and initialize it
                TowerDataSo towerDataSo = ScriptableObject.CreateInstance<TowerDataSo>();
                towerDataSo.InitializeRune(saver.RuneLevels);
                _towerTypeDict.Add(saver.TowerId, towerDataSo);
            }
        }
    }
    public void UpdateTowerDataConfig()
    {
        // TODO: load TowerDataConfig data
        var towerDatas = TowerDatas;  // Retrieve the list of TowerDataSaver from the asset
        
        // if (towerDatas.Count > 0)
        // {
        //     // Clear current data before loading
        //     towerDatas.Clear();
        // }
        LoadTowerDataFromLocalToTowerDict(towerDatas);
    }
    
    // New
    public void SaveTowers(SerializedDictionary<UnitId.Tower, TowerDataSo> towerTypeDict)
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
        ListTowerDatas = new List<TowerData>();
        // Ensure defaults are set for both lists
        // ListTowerDatas = new List<TowerData>
        // {
        //     new TowerData
        //     {
        //         TowerId = 0, // Default Tower ID
        //         RuneLevels = new List<RuneData>() // Default empty rune levels
        //     }
        // };
    }
}

[Serializable]
public struct TowerData
{
    public UnitId.Tower TowerId;
    public List<RuneData> RuneLevels;
}



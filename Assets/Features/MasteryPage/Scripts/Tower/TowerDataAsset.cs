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
        var model = new TowerDataModel
        {
            TowerList = new List<TowerSoSaver>()
        };

        foreach (var kvp in towerTypeDict)
        {
            if (kvp.Value != null && kvp.Value._runeLevels != null && kvp.Value._runeLevels.Count > 0)
            {
                var towerSoSaver = new TowerSoSaver
                {
                    TowerId = kvp.Key,
                    RuneLevels = kvp.Value._runeLevels
                };
                model.TowerList.Add(towerSoSaver);
            }
        }
        return model;
    }

    public TowerDataModel LoadTowers()
    {
        LoadData(); // Load the data from json file into _model
        return _model;
    }
}

[Serializable]
public struct TowerDataModel : IDefaultDataModel
{
    public List<TowerSoSaver> TowerList;
    public bool IsEmpty()
    {
        return TowerList == null || TowerList.Count == 0;
    }

    public void SetDefault()
    {
        TowerSoSaver towerSoSaver = new TowerSoSaver()
        {
            TowerId = 0,
            RuneLevels = new List<RuneLevel>()
        };
        TowerList = new List<TowerSoSaver> { towerSoSaver };
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

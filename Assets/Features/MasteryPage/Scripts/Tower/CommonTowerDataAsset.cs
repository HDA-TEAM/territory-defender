
using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "CommonTowerDataAsset", menuName = "ScriptableObject/DataAsset/CommonTowerDataAsset")]
public class CommonTowerDataAsset : BaseDataAsset<TowerDataModel>
{
    public void SaveTowers(SerializedDictionary<TowerId, CommonTowerSO> towerTypeDict)
    {
        _model = ConvertToTowerDataModel(towerTypeDict);
        SaveData();
    }

    private TowerDataModel ConvertToTowerDataModel(SerializedDictionary<TowerId, CommonTowerSO> towerTypeDict)
    {
        var model = new TowerDataModel
        {
            _towerList = new List<TowerSoSaver>()
        };

        foreach (var kvp in towerTypeDict)
        {
            if (kvp.Value != null && kvp.Value.RuneLevels != null && kvp.Value.RuneLevels.Count > 0)
            {
                var towerSoSaver = new TowerSoSaver
                {
                    _towerId = kvp.Key,
                    _runeLevels = kvp.Value.RuneLevels
                };
                model._towerList.Add(towerSoSaver);
            }
        }

        return model;
    }
    
    public TowerDataModel LoadTowers()
    {
        LoadData(); // Load the data into _model
        return _model;
    }
}

[Serializable]
public struct TowerDataModel : IDefaultCustom
{
    public List<TowerSoSaver> _towerList;
    public bool IsEmpty()
    {
        return _towerList == null || _towerList.Count == 0;
    }

    public void SetDefault()
    {
        TowerSoSaver towerSoSaver = new TowerSoSaver()
        {
            _towerId = 0,
            _runeLevels = new List<RuneLevel>()
        };
        _towerList = new List<TowerSoSaver> { towerSoSaver };
    }
}

[Serializable]
public struct TowerSoSaver
{
    public TowerId _towerId;
    public List<RuneLevel> _runeLevels;

}

[Serializable]
public struct RuneLevel
{
    public RuneId _runeId;
    public int _level;
    
    public RuneLevel(RuneId runeId, int level)
    {
        _runeId = runeId;
        _level = level;
    }
}


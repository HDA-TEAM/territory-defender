
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;


[CreateAssetMenu(fileName = "CommonTowerDataAsset", menuName = "ScriptableObject/DataAsset/CommonTowerDataAsset")]
public class CommonTowerDataAsset : ScriptableObject
{
    [SerializedDictionary("TowerId", "CommonTowerSO")] [SerializeField]
    private SerializedDictionary<TowerId, CommonTowerSO> _towerTypeDict = new SerializedDictionary<TowerId, CommonTowerSO>();

    [SerializeField] private TestDataAsset _testDataAsset;
    [SerializeField]
    private string _dataFilename = "TowerData.json";
    
    private TowerId _towerId;
    public CommonTowerSO GetTowerType(TowerId towerId)
    {
        _towerTypeDict.TryGetValue(towerId, out CommonTowerSO tower);
        if (!tower)
        {
            Debug.LogError("Tower type not exist in dictionary");
            return _towerTypeDict[0];
        }
        return tower;
    }

    public List<CommonTowerSO> GetAllTowerData()
    {
        return _towerTypeDict.Values.ToList();
    }

    public void UpdateTowerData(TowerId towerId, RuneComposite runeComposite)
    {
        _towerTypeDict.TryGetValue(towerId, out CommonTowerSO curTower);
        if (!curTower)
        {
            Debug.LogError("Tower type not exist in dictionary");
        }
        else
        {
            RuneLevel runeLevel = new RuneLevel(runeComposite.RuneId, runeComposite.Level);
            int index = curTower.RuneLevels.FindIndex(r => r._runeId == runeComposite.RuneId);
            if (index != -1)
            {
                // RuneId exists, update the rune
                //curTower.UpdateRune(index);
                UpdateRune(curTower, index, 1);
            }
            else
            {
                // RuneId does not exist, add a new rune
                AddRune(curTower, runeLevel);
                //curTower.AddRune(runeLevel);
            }
            //SaveTowers();
        }
    }
    
    private void AddRune(CommonTowerSO towerSo, RuneLevel runeLevel)
    {
        if (towerSo.RuneLevels == null)
        {
            towerSo.RuneLevels = new List<RuneLevel>();
        }

        // Add the new rune
        towerSo.RuneLevels.Add(runeLevel);

        // Optionally, sort the RuneLevels list by RuneId
        towerSo.RuneLevels.Sort((a, b) => a._runeId.CompareTo(b._runeId));
        
    }
    
    private void UpdateRune(CommonTowerSO towerSo, int index, int newLevel)
    {
        if (towerSo.RuneLevels == null || index < 0 || index >= towerSo.RuneLevels.Count)
        {
            Debug.LogError("Invalid index or RuneLevels is not initialized.");
            return;
        }

        // Update the level of the existing rune
        RuneLevel existingRuneLevel = towerSo.RuneLevels[index];
        towerSo.RuneLevels[index] = new RuneLevel(existingRuneLevel._runeId, newLevel);
        
    }

    private void SaveTowers()
    {
        TowerDataModel model = ConvertToTowerDataModel();
        string json = JsonUtility.ToJson(model);
        string filePath = Path.Combine(Application.persistentDataPath, _dataFilename);
        File.WriteAllText(filePath, json);
        Debug.Log("Tower data saved to: " + filePath);
    }
    
    private TowerDataModel ConvertToTowerDataModel()
    {
        var model = new TowerDataModel
        {
            _towerList = new List<TowerSoSaver>()
        };

        foreach (var kvp in _towerTypeDict)
        {
            var towerSoSaver = new TowerSoSaver
            {
                _towerId = kvp.Key,
                _runeLevels = kvp.Value.RuneLevels
            };
            model._towerList.Add(towerSoSaver);
        }

        return model;
    }
}


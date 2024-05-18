using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Common.Scripts;
using GamePlay.Scripts.Data;
using UnityEngine;
using TowerDataConfig = Features.MasteryPage.Scripts.Tower.TowerDataConfig;


[CreateAssetMenu(fileName = "TowerRuneDataAsset", menuName = "ScriptableObject/DataAsset/TowerRuneDataAsset")]
public class TowerRuneDataAsset : ScriptableObject
{
    [SerializedDictionary("TowerId", "TowerDataConfig")] 
    public SerializedDictionary<UnitId.Tower, TowerDataConfig> _towerTypeDict = new SerializedDictionary<UnitId.Tower, TowerDataConfig>();
    public TowerDataAsset _towerDataAsset;

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

    // public void UpdateTowerData(UnitId.Tower towerId, RuneComposite runeComposite)
    // {
    //     _towerTypeDict.TryGetValue(towerId, out TowerDataConfig curTower);
    //     if (!curTower)
    //     {
    //         Debug.LogError("Tower type not exist in dictionary");
    //     }
    //     else
    //     {
    //         RuneLevel runeLevel = new RuneLevel(runeComposite.RuneId, runeComposite.Level);
    //         int index = curTower._runeLevels.FindIndex(r => r.RuneId == runeComposite.RuneId);
    //         if (index != -1)
    //         {
    //             // RuneId exists, update the rune
    //             UpgradeRune(curTower, index);
    //         }
    //         else
    //         {
    //             // RuneId does not exist, add a new rune
    //             AddRune(curTower, runeLevel);
    //         }
    //
    //         _towerDataAsset.SaveTowers(_towerTypeDict);
    //     }
    // }
    //
    // private void AddRune(TowerDataConfig towerDataConfig, RuneLevel runeLevel)
    // {
    //     if (towerDataConfig._runeLevels == null)
    //     {
    //         Debug.Log("First time......"); // No join
    //         towerDataConfig._runeLevels = new List<RuneLevel>();
    //     }
    //
    //     // Set the level of the rune to 1 regardless of its current level
    //     RuneLevel newRune = new RuneLevel
    //     {
    //         RuneId = runeLevel.RuneId,
    //         Level = 1 // Set level to 1 for the new rune
    //     };
    //     towerDataConfig._runeLevels.Add(newRune);
    //
    //     // foreach (var item in towerDataConfig._runeLevels)
    //     // {
    //     //     Debug.Log("ID: "+ item.RuneId + "..." + item.Level);
    //     // }
    //     // Optionally, sort the RuneLevels list by RuneId
    //     towerDataConfig._runeLevels.Sort((a, b) => a.RuneId.CompareTo(b.RuneId));
    // }
    // private void UpgradeRune(TowerDataConfig towerDataConfig, int index)
    // {
    //     if (towerDataConfig._runeLevels == null || index < 0 || index >= towerDataConfig._runeLevels.Count)
    //     {
    //         Debug.LogError("Invalid index or RuneLevels is not initialized.");
    //         return;
    //     }
    //
    //     // Increment the level of the existing rune
    //     RuneLevel currentRuneLevel = towerDataConfig._runeLevels[index];
    //     currentRuneLevel.Level++;  // Increment the level by 1
    //     towerDataConfig._runeLevels[index] = currentRuneLevel;
    // }
    //
    // public void ResetRuneLevel(UnitId.Tower towerId)
    // {
    //     // Attempt to get the tower configuration
    //     if (!_towerTypeDict.TryGetValue(towerId, out TowerDataConfig towerDataConfig))
    //     {
    //         Debug.LogError("Tower type not exist in dictionary for reset");
    //         return;
    //     }
    //
    //     _returnStar = 0;
    //     
    //     for (int i = 0; i < towerDataConfig._runeLevels.Count; i++)
    //     {
    //         RuneLevel rune = towerDataConfig._runeLevels[i];
    //         if (towerDataConfig._runeLevels[i].Level > 0)
    //         {
    //             _returnStar += rune.Level;
    //             rune.Level = 0;
    //             towerDataConfig._runeLevels[i] = rune;
    //         }
    //     }
    //     
    //     // Remove all runes with level
    //     towerDataConfig._runeLevels.RemoveAll(rune => rune.Level == 0);
    //     
    //     // Save changes to disk or server
    //     _towerDataAsset.SaveTowers(_towerTypeDict);
    // }
    private void LoadTowerRuneDataFromSavers(List<TowerDataSaver> towerDataSavers)
    {
        //_towerTypeDict.Clear(); // Clear existing data

        foreach (var saver in towerDataSavers)
        {
            if (!_towerTypeDict.ContainsKey(saver.TowerId))
            {
                // Create a new TowerDataConfig instance and initialize it
                TowerDataConfig config = ScriptableObject.CreateInstance<TowerDataConfig>();
                config.Initialize(saver.RuneLevels);
                _towerTypeDict.Add(saver.TowerId, config);
            }
        }
    }
    public void UpdateTowerDataConfig()
    {
        // TODO: load TowerDataConfig data
        var towerSavers = _towerDataAsset.GetTowers();  // Retrieve the list of TowerDataSaver from the asset

        if (towerSavers.Count > 0)
        {
            Debug.Log("Exist towerSavers");
        }
        
        LoadTowerRuneDataFromSavers(towerSavers);  
       // return _towerDataAsset.GetTowers();
    }
}

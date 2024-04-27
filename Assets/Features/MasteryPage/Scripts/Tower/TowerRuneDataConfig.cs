using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Common.Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using TowerDataConfig = Features.MasteryPage.Scripts.Tower.TowerDataConfig;


[CreateAssetMenu(fileName = "TowerRuneDataConfig", menuName = "ScriptableObject/DataAsset/TowerRuneDataConfig")]
public class TowerRuneDataConfig : ScriptableObject
{
    [SerializedDictionary("TowerId", "TowerDataConfig")] [SerializeField]
    private SerializedDictionary<UnitId.Tower, TowerDataConfig> _towerTypeDict = new SerializedDictionary<UnitId.Tower, TowerDataConfig>();
    [SerializeField] private TowerDataAsset _towerDataAsset;

    public int _returnStar;
    
    //private TowerId _towerId;
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

    public void UpdateTowerData(UnitId.Tower towerId, RuneComposite runeComposite)
    {
        _towerTypeDict.TryGetValue(towerId, out TowerDataConfig curTower);
        if (!curTower)
        {
            Debug.LogError("Tower type not exist in dictionary");
        }
        else
        {
            RuneLevel runeLevel = new RuneLevel(runeComposite.RuneId, runeComposite.Level);
            int index = curTower._runeLevels.FindIndex(r => r.RuneId == runeComposite.RuneId);
            if (index != -1)
            {
                // RuneId exists, update the rune
                UpgradeRune(curTower, index);
            }
            else
            {
                // RuneId does not exist, add a new rune
                AddRune(curTower, runeLevel);
            }

            _towerDataAsset.SaveTowers(_towerTypeDict);
        }
    }
    
    private void AddRune(TowerDataConfig towerDataConfig, RuneLevel runeLevel)
    {
        if (towerDataConfig._runeLevels == null)
        {
            towerDataConfig._runeLevels = new List<RuneLevel>();
        }

        // Set the level of the rune to 1 regardless of its current level
        RuneLevel newRune = new RuneLevel
        {
            RuneId = runeLevel.RuneId,
            Level = 1 // Set level to 1 for the new rune
        };
        towerDataConfig._runeLevels.Add(newRune);

        // Optionally, sort the RuneLevels list by RuneId
        towerDataConfig._runeLevels.Sort((a, b) => a.RuneId.CompareTo(b.RuneId));
    }

    
    private void UpgradeRune(TowerDataConfig towerDataConfig, int index)
    {
        if (towerDataConfig._runeLevels == null || index < 0 || index >= towerDataConfig._runeLevels.Count)
        {
            Debug.LogError("Invalid index or RuneLevels is not initialized.");
            return;
        }

        // Increment the level of the existing rune
        RuneLevel currentRuneLevel = towerDataConfig._runeLevels[index];
        currentRuneLevel.Level++;  // Increment the level by 1
        towerDataConfig._runeLevels[index] = currentRuneLevel;
    }
    
    public void ResetRuneLevel(UnitId.Tower towerId, RuneComposite runeComposite)
    {
        // Attempt to get the tower configuration
        if (!_towerTypeDict.TryGetValue(towerId, out TowerDataConfig towerDataConfig))
        {
            Debug.LogError("Tower type not exist in dictionary for reset");
            return;
        }

        // Find the index of the rune to be reset
        int index = towerDataConfig._runeLevels.FindIndex(r => r.RuneId == runeComposite.RuneId);
        if (index == -1)
        {
            Debug.LogError("RuneId not found for reset");
            return;
        }

        // Get the current rune level
        RuneLevel currentRuneLevel = towerDataConfig._runeLevels[index];

        // Calculate the stars to return based on the current rune level
        _returnStar = currentRuneLevel.Level;

        // Reset the rune level to 0
        currentRuneLevel.Level = 0;
        towerDataConfig._runeLevels[index] = currentRuneLevel;

        towerDataConfig._runeLevels.RemoveAt(index);
       // towerDataConfig.Remove()
        // Optionally save changes to disk or server
        _towerDataAsset.SaveTowers(_towerTypeDict);
    }
    public TowerDataModel GetTowerDataAsset()
    {
        return _towerDataAsset.LoadTowers();
    }
}

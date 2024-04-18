using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Common.Scripts;
using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu(fileName = "CommonTowerConfig", menuName = "ScriptableObject/DataAsset/CommonTowerConfig")]
public class CommonTowerConfig : ScriptableObject
{
    [SerializedDictionary("TowerId", "CommonTowerSO")] [SerializeField]
    private SerializedDictionary<UnitId.Tower, CommonTowerSO> _towerTypeDict = new SerializedDictionary<UnitId.Tower, CommonTowerSO>();
    [FormerlySerializedAs("_commonTowerDataAsset")]
    [SerializeField] private TowerDataAsset _towerDataAsset;

    //private TowerId _towerId;
    public CommonTowerSO GetTower(UnitId.Tower towerId)
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

    public void UpdateTowerData(UnitId.Tower towerId, RuneComposite runeComposite)
    {
        _towerTypeDict.TryGetValue(towerId, out CommonTowerSO curTower);
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
                UpdateRune(curTower, index);
            }
            else
            {
                // RuneId does not exist, add a new rune
                AddRune(curTower, runeLevel);
            }

            _towerDataAsset.SaveTowers(_towerTypeDict);
        }
    }

    private void AddRune(CommonTowerSO towerSo, RuneLevel runeLevel)
    {
        if (towerSo._runeLevels == null)
        {
            towerSo._runeLevels = new List<RuneLevel>();
        }

        // Set the level of the rune to 1 regardless of its current level
        RuneLevel newRune = new RuneLevel
        {
            RuneId = runeLevel.RuneId,
            Level = 1 // Set level to 1 for the new rune
        };
        towerSo._runeLevels.Add(newRune);

        // Optionally, sort the RuneLevels list by RuneId
        towerSo._runeLevels.Sort((a, b) => a.RuneId.CompareTo(b.RuneId));
    }


    private void UpdateRune(CommonTowerSO towerSo, int index)
    {
        if (towerSo._runeLevels == null || index < 0 || index >= towerSo._runeLevels.Count)
        {
            Debug.LogError("Invalid index or RuneLevels is not initialized.");
            return;
        }

        // Increment the level of the existing rune
        RuneLevel existingRuneLevel = towerSo._runeLevels[index];
        existingRuneLevel.Level++; // Increment the level by 1
        towerSo._runeLevels[index] = existingRuneLevel;
    }


    public TowerDataModel GetTowerDataAsset()
    {
        return _towerDataAsset.LoadTowers();
    }
}

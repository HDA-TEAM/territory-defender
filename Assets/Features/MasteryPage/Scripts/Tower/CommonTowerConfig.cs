
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;


[CreateAssetMenu(fileName = "CommonTowerConfig", menuName = "ScriptableObject/DataAsset/CommonTowerConfig")]
public class CommonTowerConfig : ScriptableObject
{
    [SerializedDictionary("TowerId", "CommonTowerSO")] [SerializeField]
    private SerializedDictionary<TowerId, CommonTowerSO> _towerTypeDict = new SerializedDictionary<TowerId, CommonTowerSO>();

    [SerializeField] private CommonTowerDataAsset _commonTowerDataAsset;

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
                UpdateRune(curTower, index);
            }
            else
            {
                // RuneId does not exist, add a new rune
                AddRune(curTower, runeLevel);
            }
            
            _commonTowerDataAsset.SaveTowers(_towerTypeDict);
        }
    }
    
    private void AddRune(CommonTowerSO towerSo, RuneLevel runeLevel)
    {
        if (towerSo.RuneLevels == null)
        {
            towerSo.RuneLevels = new List<RuneLevel>();
        }

        // Set the level of the rune to 1 regardless of its current level
        RuneLevel newRune = new RuneLevel
        {
            _runeId = runeLevel._runeId,
            _level = 1 // Set level to 1 for the new rune
        };
        towerSo.RuneLevels.Add(newRune);

        // Optionally, sort the RuneLevels list by RuneId
        towerSo.RuneLevels.Sort((a, b) => a._runeId.CompareTo(b._runeId));
    }

    
    private void UpdateRune(CommonTowerSO towerSo, int index)
    {
        if (towerSo.RuneLevels == null || index < 0 || index >= towerSo.RuneLevels.Count)
        {
            Debug.LogError("Invalid index or RuneLevels is not initialized.");
            return;
        }

        // Increment the level of the existing rune
        RuneLevel existingRuneLevel = towerSo.RuneLevels[index];
        existingRuneLevel._level++;  // Increment the level by 1
        towerSo.RuneLevels[index] = existingRuneLevel;
    }


    public TowerDataModel GetTowerDataAsset()
    {
        return _commonTowerDataAsset.LoadTowers();
    }
}


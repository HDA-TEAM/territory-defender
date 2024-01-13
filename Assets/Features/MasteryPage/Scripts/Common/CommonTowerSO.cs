using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Path = System.IO.Path;

[CreateAssetMenu(fileName = "CommonTowerSO", menuName = "ScriptableObject/DataAsset/CommonTowerSO")]
public class CommonTowerSO : ScriptableObject
{
    public TowerId _towerId;
    
    public Action _onTowerDataUpdatedAction;
    
    public List<RuneLevel> RuneLevels;
    
    public TowerId GetTowerId()
    {
        return _towerId;
    }
    #region MasteryPage access

    public List<RuneLevel> GetAllRuneDatLevels()
    {
        return RuneLevels;
    }
    public void AddRune(RuneLevel runeLevel)
    {
        if (RuneLevels == null)
        {
            RuneLevels = new List<RuneLevel>();
        }
        
        // Rune not found, add as new
        RuneLevels.Add(new RuneLevel(runeLevel._runeId, 1));

        // Sort the RuneLevels list by RuneId
        RuneLevels.Sort((a, b) => a._runeId.CompareTo(b._runeId));

        // Find the position of the new rune after sorting
        int index = RuneLevels.FindIndex(r => r._runeId == runeLevel._runeId);
        Debug.Log("Rune ID: " + RuneLevels[index]._runeId + ", Level: " + RuneLevels[index]._level + ", Position: " + index);
    
        DataAssetSaver.SaveTowerData(_towerId, RuneLevels);
    
       // _onTowerDataUpdatedAction?.Invoke();
    }
    
    public void UpdateRune(int index)
    {
        if (RuneLevels == null)
        {
            RuneLevels = new List<RuneLevel>();
        }

        // Rune with the same ID found, increment its level
        RuneLevels[index] = new RuneLevel(RuneLevels[index]._runeId, RuneLevels[index]._level + 1);
        
        //_onTowerDataUpdatedAction?.Invoke();
    }
    #endregion
}





using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class DataAssetSaver
{
    public static void SaveTowerData(TowerId towerId, List<RuneLevel> runeLevels)
    {
        // Define the file path
        string filePath = Path.Combine(Application.persistentDataPath, "towerDataAsset.json");
        TowerDataAssetList towerDataAssetList;

        // Load existing TowerDataAssetList
        if (File.Exists(filePath) && new FileInfo(filePath).Length > 0)
        {
            string jsonData = File.ReadAllText(filePath);
            towerDataAssetList = JsonUtility.FromJson<TowerDataAssetList>(jsonData);
        }
        else
        {
            towerDataAssetList = new TowerDataAssetList();
        }

        // Initialize the _towerList if it's null
        if (towerDataAssetList._towerList == null)
        {
            towerDataAssetList._towerList = new List<TowerSoSaver>();
        }

        // Find the TowerSoSaver with the same TowerId
        var existingTowerIndex = towerDataAssetList._towerList.FindIndex(t => t._towerId == towerId);
        if (existingTowerIndex != -1)
        {
            // Update the RuneLevels of the existing TowerSoSaver
            TowerSoSaver existingTower = towerDataAssetList._towerList[existingTowerIndex];
            existingTower._runeLevels = runeLevels;
            towerDataAssetList._towerList[existingTowerIndex] = existingTower;
        }
        else
        {
            // Create and add a new TowerSoSaver
            TowerSoSaver newTowerSoSaver = new TowerSoSaver
            {
                _towerId = towerId,
                _runeLevels = runeLevels
            };
            towerDataAssetList._towerList.Add(newTowerSoSaver);
        }

        // Save the updated TowerDataAssetList to a file
        JsonSaver.SaveToJsonFile(towerDataAssetList, filePath);

        Debug.Log("Data saved to: " + filePath);
    }

}

[Serializable]
public struct TowerDataAssetList
{
    public List<TowerSoSaver> _towerList;

    public void AddTower(TowerSoSaver towerSoSaver)
    {
        if (_towerList == null)
        {
            _towerList = new List<TowerSoSaver>();
        }
        _towerList.Add(towerSoSaver);
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



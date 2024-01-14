
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class DataAssetSaver
{
    public static void SaveTowerData(TowerId towerId, List<RuneLevel> runeLevels)
    {
        // Define the file path
        string filePath = Path.Combine(Application.persistentDataPath, "towerDataAsset.json");
        TowerDataModel towerDataModel;

        // Load existing TowerDataAssetList
        if (File.Exists(filePath) && new FileInfo(filePath).Length > 0)
        {
            string jsonData = File.ReadAllText(filePath);
            towerDataModel = JsonUtility.FromJson<TowerDataModel>(jsonData);
        }
        else
        {
            towerDataModel = new TowerDataModel();
        }

        // Initialize the _towerList if it's null
        if (towerDataModel._towerList == null)
        {
            towerDataModel._towerList = new List<TowerSoSaver>();
        }

        // Find the TowerSoSaver with the same TowerId
        var existingTowerIndex = towerDataModel._towerList.FindIndex(t => t._towerId == towerId);
        if (existingTowerIndex != -1)
        {
            // Update the RuneLevels of the existing TowerSoSaver
            TowerSoSaver existingTower = towerDataModel._towerList[existingTowerIndex];
            existingTower._runeLevels = runeLevels;
            towerDataModel._towerList[existingTowerIndex] = existingTower;
        }
        else
        {
            // Create and add a new TowerSoSaver
            TowerSoSaver newTowerSoSaver = new TowerSoSaver
            {
                _towerId = towerId,
                _runeLevels = runeLevels
            };
            towerDataModel._towerList.Add(newTowerSoSaver);
        }

        // Save the updated TowerDataAssetList to a file
        JsonSaver.SaveToJsonFile(towerDataModel, filePath);

        Debug.Log("Data saved to: " + filePath);
    }

}

public interface IDefaultCustom
{
    public bool IsEmpty();
    public void SetDefault();
}

public class DataAsset<T> : ScriptableObject where T: struct, IDefaultCustom
{
    // Check file exist function
    private bool IsFileExist(string filePath)
    {
        return File.Exists(filePath) && new FileInfo(filePath).Length > 0;
    }

    private string GetFilePath(string filename)
    {
        return Path.Combine(Application.persistentDataPath, filename);
    }
    public void SaveData(string filename, T model)
    {
        string filePath = GetFilePath(filename);

        if (!IsFileExist(filePath))
        {
            model = new T();
            model.SetDefault();
            
            Debug.Log("Model: " + model);
        }
        
        Debug.Log("Model 2: " + model);
        string data = JsonConvert.SerializeObject(model);
        JsonSaver.SaveToJsonFile(data, filePath);
    }
    
    public void LoadData(string filename, out T model)
    {
        string filePath = GetFilePath(filename);

        if (IsFileExist(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            model = JsonConvert.DeserializeObject<T>(jsonData);
        }

        else
        {
            model = new T();
            model.SetDefault();
        }
    }
    
}
public abstract class BaseDataAsset<T>: DataAsset<T> where T: struct, IDefaultCustom // Model
{
    [SerializeField] private string _filename;
    [SerializeField] protected T _model;
    
    public void SaveData()
    {
        base.SaveData(_filename, _model);
    }
    
    public void LoadData()
    {
        base.LoadData(_filename, out _model);
    }
    
    // Luu va Doc file

}

[Serializable]
public struct TowerDataModel : IDefaultCustom
{
    public List<TowerSoSaver> _towerList;
    public bool IsEmpty()
    {
        Debug.Log("Is empty");
        return _towerList == null || _towerList.Count == 0;
    }

    public void SetDefault()
    {
        TowerSoSaver towerSoSaver = new TowerSoSaver()
        {
            _towerId = 0,
            _runeLevels = new List<RuneLevel>()
        };
        _towerList = new List<TowerSoSaver>();
        Debug.Log("Set default " + towerSoSaver);
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



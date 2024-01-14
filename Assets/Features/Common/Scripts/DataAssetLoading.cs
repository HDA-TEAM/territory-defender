using System;
using System.IO;
using UnityEngine;

public static class DataAssetLoading
{
    public static TowerDataModel LoadTowerDataAssetList()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "towerDataAsset.json");

        // Check if the file exists
        if (File.Exists(filePath))
        {
            // Read the JSON data from the file
            string jsonData = File.ReadAllText(filePath);
        
            // Deserialize the JSON data to a TowerDataModel object
            TowerDataModel towerDataModel = JsonUtility.FromJson<TowerDataModel>(jsonData);
        
            // Check if the deserialized data is empty
            if (towerDataModel._towerList == null || towerDataModel._towerList.Count == 0)
            {
                // If empty, return a new TowerDataModel instance
                return new TowerDataModel();
            }
        
            // Return the deserialized TowerDataModel
            return towerDataModel;
        }
        else
        {
            // If the file does not exist, return a new TowerDataModel instance
            return new TowerDataModel();
        }
    }


}


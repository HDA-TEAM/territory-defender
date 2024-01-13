using System;
using System.IO;
using UnityEngine;

public static class DataAssetLoading
{
    public static TowerDataAssetList LoadTowerDataAssetList()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "towerDataAsset.json");

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            TowerDataAssetList towerDataAssetList = JsonUtility.FromJson<TowerDataAssetList>(jsonData);
            
            if (towerDataAssetList._towerList == null || towerDataAssetList._towerList.Count == 0)
            {
                return new TowerDataAssetList();
            }
            return towerDataAssetList;
        }
        else
        {
            return new TowerDataAssetList();
        }
    }

}


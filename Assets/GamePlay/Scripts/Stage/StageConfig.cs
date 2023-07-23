using GamePlay.Scripts;
using GamePlay.Scripts.Tower;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "StageConfig_", menuName = "ScriptableObject/Stage/StageConfig")]
public class StageConfig : ScriptableObject
{
    public StageChapterKey StageChapterKey;
    public StageIdKey StageIdKey;
    
    public TowerKitSetConfig towerKitSetConfig;
    public RouteSetConfig routeSetConfig;
    // public int CountNumberTowerKit()
    // {
    //     return towerKitLocation.Count;
    // }
    public void CreateNewStageOs(List<TowerKitManager> towerKits,List<LineRenderer> routeLines)
    {
        towerKitSetConfig = CreateInstance<TowerKitSetConfig>();
        routeSetConfig = CreateInstance<RouteSetConfig>();
        
        string parentPath = AssetDatabase.GetAssetPath(this);
        string newAssetPathTowerKitSet = Path.GetDirectoryName(parentPath) + "/" + "towerKitSet.asset";
        string newAssetPathRouteSet = Path.GetDirectoryName(parentPath) + "/" + "routeSetConfig.asset";
        AssetDatabase.CreateAsset(towerKitSetConfig, newAssetPathTowerKitSet);
        AssetDatabase.CreateAsset(routeSetConfig, newAssetPathRouteSet);
        SaveToOS(towerKits, routeLines);
    }
    public void LoadFormOs(List<TowerKitManager> towerKits,List<LineRenderer> routeLines)
    {
        // if (routeSetConfig == null && towerKitSetConfig == null)
        // {
        //     CreateNewStageOs(towerKits,routeLines);
        // }
        towerKitSetConfig.LoadTowerKitsPositionFromOs(towerKits);
        routeSetConfig.LoadFromOs(routeLines);
        
        
    }
    public void SaveToOS(List<TowerKitManager> towerKits,List<LineRenderer> routeLines)
    {
        towerKitSetConfig.SaveTowerKitPositionToOs(towerKits);
        routeSetConfig.SaveToOs(routeLines);
    }
    
    
}

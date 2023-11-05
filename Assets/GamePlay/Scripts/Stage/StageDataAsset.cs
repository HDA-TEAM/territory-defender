using AYellowpaper.SerializedCollections;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum StageId
{
    CHAP_1_STAGE_1 = 100,
}

[Serializable]
public struct StageConfig
{
    public TowerKitSetConfig TowerKitSetConfig;
    public RouteSetConfig RouteSetConfig;
    public StageSpawningConfig StageSpawningConfig;
    public InGameInventoryDataAsset InGameInventoryDataAsset;
}

[CreateAssetMenu(fileName = "StageDataAsset", menuName = "ScriptableObject/Database/Stage/StageDataAsset")]
public class StageDataAsset : ScriptableObject
{
    [SerializeField] private StageId _currentStageId;
    [SerializeField] [SerializedDictionary("StageId","StageConfig")]
    private SerializedDictionary<StageId,StageConfig> _stageDict;
    public StageId CurrentStageId() => _currentStageId;
    
    public StageConfig GetStageConfig(StageId stageId = StageId.CHAP_1_STAGE_1)
    {
        _stageDict.TryGetValue(_currentStageId, out StageConfig stageConfig);
        return stageConfig;
    }
}

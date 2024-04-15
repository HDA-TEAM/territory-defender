using AYellowpaper.SerializedCollections;
using System;
using UnityEngine;

public enum StageId
{
    Chap1Stage0 = 100,
    Chap1Stage1 = 101,
    Chap1Stage2 = 102,
}

[Serializable]
public struct StageConfig
{
    public TowerKitSetConfig TowerKitSetConfig;
    public RouteSetConfig RouteSetConfig;
    public InGameInventoryRuntimeData _inGameInventoryRuntimeData;
}

[CreateAssetMenu(fileName = "StageDataAsset", menuName = "ScriptableObject/Database/Stage/StageDataAsset")]
public class StageDataAsset : ScriptableObject
{
    [SerializeField] private StageId _currentStageId;
    [SerializeField] [SerializedDictionary("StageId","StageConfig")]
    private SerializedDictionary<StageId,StageConfig> _stageDict;
    public StageId CurrentStageId() => _currentStageId;

    
    public StageConfig GetStageConfig(StageId stageId = StageId.Chap1Stage1)
    {
        _stageDict.TryGetValue(_currentStageId, out StageConfig stageConfig);
        return stageConfig;
    }
}

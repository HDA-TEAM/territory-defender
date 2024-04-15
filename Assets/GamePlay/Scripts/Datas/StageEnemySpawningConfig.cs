using AYellowpaper.SerializedCollections;
using CustomInspector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

[CreateAssetMenu(fileName = "StageEnemySpawningConfig", menuName = "ScriptableObject/Database/Stage/SpawningConfigData")]
[Serializable, Preserve]
public class StageEnemySpawningConfig : ScriptableObject
{
    // [SerializedDictionary("StageId,SingleStageSpawningConfig")]
    // [SerializeField] private SerializedDictionary<StageId,SingleStageSpawningConfig> _stageEnemySpawningConfigs;

    [SerializeField] private List<SingleStageSpawningConfig> _stageEnemySpawningConfigs;
    public SingleStageSpawningConfig FindSpawningConfig(StageId stageId)
    {
        // if (_stageEnemySpawningConfigs.TryGetValue(stageId, out SingleStageSpawningConfig singleStageSpawningConfig))
        // {
        //     return singleStageSpawningConfig;
        // }
        // Debug.LogError("Not found Stage Spawning config");
        _stageEnemySpawningConfigs.Find(stage => stage.StageId == stageId);
        return null;
    }

    public int GetNumberOfUnitSpawningWithStageId(StageId stageId) => FindSpawningConfig(stageId).GetTotalUnitsSpawning();

#if UNITY_EDITOR
    [Button("ParseToJson")]
    [Button("ReadJsonData")]
    [SerializeField] private string _data;
    public void ParseToJson()
    {
        _data = JsonConvert.SerializeObject(_stageEnemySpawningConfigs);
        Debug.Log("ParseToJson " + _data);
    }
    public void ReadJsonData()
    {
        _stageEnemySpawningConfigs = JsonConvert.DeserializeObject<List<SingleStageSpawningConfig>>(_data);
       _stageEnemySpawningConfigs.Clear();
       foreach (var singleStage in singleStageSpawningConfigs)
       {
           _stageEnemySpawningConfigs.TryAdd(singleStage.StageId, singleStage);
       }
    }
#endif
}
[Serializable]
public class SingleStageSpawningConfig
{
    public StageId StageId;
    public List<WaveSpawning> WavesSpawning;
    
    [Serializable, Preserve]
    public struct WaveSpawning
    {
        public List<GroupSpawning> GroupsSpawning;
    }

    [Serializable, Preserve]
    public struct GroupSpawning
    {
        public float StartSpawning;
        public UnitId.Enemy ObjectSpawn;
        public int RouteId;
        public int NumberSpawning;
    }

    public int GetTotalUnitsSpawning()
    {
        int total = 0;
        foreach (WaveSpawning waveSpawning in WavesSpawning)
        {
            foreach (GroupSpawning groupSpawning in waveSpawning.GroupsSpawning)
            {
                total += groupSpawning.NumberSpawning;
            }
        }
        return total;
    }
}
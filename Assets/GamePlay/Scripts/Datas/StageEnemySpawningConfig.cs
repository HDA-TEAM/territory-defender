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
#if UNITY_EDITOR
    [Button("ParseToJson")]
    [Button("ReadData")]
#endif
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
    
    
#if UNITY_EDITOR
    public List<WaveSpawning> TestWavesSpawning;
    [SerializeField] private string _data;
    public void ParseToJson()
    {
        _data = JsonConvert.SerializeObject(WavesSpawning);
    }
    public void ReadData()
    {
        TestWavesSpawning = JsonConvert.DeserializeObject<List<WaveSpawning>>(_data);
    }
#endif
}

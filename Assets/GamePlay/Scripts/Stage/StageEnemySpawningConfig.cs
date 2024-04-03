using CustomInspector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

[CreateAssetMenu(fileName = "StageEnemySpawningConfig", menuName = "ScriptableObject/Database/Stage/SpawningConfigData")]
[Serializable,Preserve]
public class StageEnemySpawningConfig : ScriptableObject
{
    [Button("ParseToJson")]
    [Button("ReadData")]
    public StageId StageId;
    public List<WaveSpawning> WavesSpawning;
    public List<WaveSpawning> TestWavesSpawning;
    [Serializable,Preserve]
    public struct WaveSpawning
    {
        public List<GroupSpawning> GroupsSpawning;
    }

    [Serializable,Preserve]
    public struct GroupSpawning
    {
        public float StartSpawning;
        public UnitId.Enemy ObjectSpawn;
        public int RouteId;
        public int NumberSpawning;
    }

    
    [SerializeField] private string _data;
    public void ParseToJson()
    {
        _data = JsonConvert.SerializeObject(WavesSpawning);
    }
    public void ReadData()
    {
        TestWavesSpawning = JsonConvert.DeserializeObject<List<WaveSpawning>>(_data);
    }
}

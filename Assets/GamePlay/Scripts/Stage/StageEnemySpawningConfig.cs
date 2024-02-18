using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageEnemySpawningConfig", menuName = "ScriptableObject/Database/Stage/SpawningConfigData")]
[Serializable]
public class StageEnemySpawningConfig : ScriptableObject
{
    public StageId StageId;
    public List<WaveSpawning> WavesSpawning;
    [Serializable]
    public struct WaveSpawning
    {
        public List<GroupSpawning> GroupsSpawning;
    }

    [Serializable]
    public struct GroupSpawning
    {
        public float StartSpawning;
        public UnitId.Enemy ObjectSpawn;
        public int RouteId;
        public int NumberSpawning;
    }
}

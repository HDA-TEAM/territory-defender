using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public static class StageConsts
{
    public const float MinSpawningObjectIntervalInSecond = 1f;
}

[CreateAssetMenu(fileName = "StageSpawningConfig_", menuName = "ScriptableObject/Stage/StageSpawningConfig_")]
[Serializable]
public class StageSpawningInformation : ScriptableObject
{
    [Header("Key config")]
    public ChapterKey ChapterKey;
    public StageIdKey StageIdKey;
    [Header("Detail Rounds config")]
    public List<RoundSpawningInformation> RoundsInfo;
    public async void StartSpawning()
    {
        foreach (var round in RoundsInfo)
        {
            round.StartSpawning();
            Debug.Log("Start round Spawning");
            await UniTask.Delay(TimeSpan.FromSeconds(round.TotalRoundTimeInSecond));
        }
    }
    [Serializable]
    public class RoundSpawningInformation
    {
        public int TotalRoundTimeInSecond;
        [Header("Detail waves config")]
        public List<WaveSpawningInformation> WavesInfo;
        private float totalWaitingTime = 0;
        public async void StartSpawning()
        {
            totalWaitingTime = 0;
            foreach (var wave in WavesInfo)
            {
                float waitToSpawnTime = wave.StartSpawningTimeOfWaveInSecond - totalWaitingTime;
                await UniTask.Delay(TimeSpan.FromSeconds(waitToSpawnTime));
                Debug.Log("Start wave Spawning");
                wave.StartSpawning();
                totalWaitingTime = wave.StartSpawningTimeOfWaveInSecond;
            }
        }
    }

    [Serializable]
    public class WaveSpawningInformation
    {
        public float StartSpawningTimeOfWaveInSecond;
        [Header("Detail groups config")]
        public List<GroupSpawningInformation> GroupsInfo;
        private float totalWaitTime = 0;
        public async void StartSpawning()
        {
            totalWaitTime = 0;
            int i = 0;
            while (i < GroupsInfo.Count)
            {
                if (GroupsInfo[i].StartSpawningTimeOfGroupInSecond <= totalWaitTime)
                {
                    Debug.Log("Start group Spawning");
                    GroupsInfo[i].StartSpawning();
                    i++;
                }
                await UniTask.Delay(TimeSpan.FromSeconds(StageConsts.MinSpawningObjectIntervalInSecond));
                totalWaitTime += StageConsts.MinSpawningObjectIntervalInSecond;
            }
        }
    }

    [Serializable]
    public class GroupSpawningInformation
    {
        [Header("Detail type of spawning objects config")]
        public float StartSpawningTimeOfGroupInSecond;
        public PoolingTypeEnum ObjectSpawn;
        public int NumberSpawning;
        public int RouteId;
        public async void StartSpawning()
        {
            for (int i = 0; i < NumberSpawning; i++)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(StageConsts.MinSpawningObjectIntervalInSecond));
                
                GameObject go = PoolingManager.Instance.SpawnObject(ObjectSpawn);
                go.TryGetComponent<EnemyMovement>(out EnemyMovement component);
                Debug.Log("Spawning " + ObjectSpawn);
                
                var routeSet = GameObject.Find("RouteSet");
                routeSet.TryGetComponent<RouteSetController>(out RouteSetController routeSetController);
                component.RouteToGate = routeSetController.CurrentRouteLineRenderers[RouteId];
                go.transform.position = component.RouteToGate.GetPosition(0);
            }
        }
    }
}
//
// public interface SpawningMachine
// {
//     public async Un StartSpawning()
//     {
//     }
// }



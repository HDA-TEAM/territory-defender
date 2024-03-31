using AYellowpaper.SerializedCollections;
using Cysharp.Threading.Tasks;
using GamePlay.Scripts.Character.StateMachine.EnemyStateMachine;
using System;
using System.Collections.Generic;
using UnityEngine;

public class StageEnemySpawningFactory : MonoBehaviour
{
    [SerializedDictionary("StageId,StageConfig")]
    [SerializeField] private SerializedDictionary<StageId,StageEnemySpawningConfig> _stageEnemySpawningConfigs;
    [SerializeField] private float _spawningEachObjectInterval;
    
    private StageEnemySpawningConfig _stageEnemySpawning;
    
    public StageEnemySpawningConfig FindSpawningConfig(StageId stageId)
    {
        if (_stageEnemySpawningConfigs.TryGetValue(stageId, out StageEnemySpawningConfig stageEnemySpawningConfig))
        {
            return stageEnemySpawningConfig;
        }
        else
        {
            Debug.LogError("Not found Stage Spawning config");
        }
        return null;
    }
    
    public async void StartSpawning(StageEnemySpawningConfig stageConfig)
    {
        if (!stageConfig)
            return;
        Debug.Log("Spawning Starting");
        List<UniTask> spawnTask = new List<UniTask>();
        foreach (var waveSpawning in  stageConfig.WavesSpawning)
        {
            spawnTask.Add(StartSpawningWave(waveSpawning));
        }
        await UniTask.WhenAll(spawnTask);
        Debug.Log("Spawning Ended");
    }
    private async UniTask StartSpawningWave(StageEnemySpawningConfig.WaveSpawning waveSpawning)
    {
        List<UniTask> spawnTask = new List<UniTask>();
        foreach (var groupSpawning in  waveSpawning.GroupsSpawning)
        {
            spawnTask.Add(StartSpawningGroup(groupSpawning));
        }
        await UniTask.WhenAll(spawnTask);
    }
    private async UniTask StartSpawningGroup(StageEnemySpawningConfig.GroupSpawning groupSpawning)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(groupSpawning.StartSpawning));
        for (int i = 0; i < groupSpawning.NumberSpawning; i++)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_spawningEachObjectInterval));

            GameObject go = PoolingController.Instance.SpawnObject(groupSpawning.ObjectSpawn.ToString());

            Debug.Log("Spawning " + groupSpawning.ObjectSpawn);

            SetRoute(go, groupSpawning.RouteId);
            UpdateStats(go);
        }
    }
    
    private void SetRoute(GameObject go, int RouteId)
    {
        go.TryGetComponent(out BaseEnemyStateMachine component);
        component.RouteToGate = RouteSetController.Instance.CurrentRouteLineRenderers[RouteId];
        go.transform.position = component.RouteToGate.GetPosition(0);
    }
    private void UpdateStats(GameObject go)
    {
        go.TryGetComponent(out UnitBase component);
        component.OnUpdateStats?.Invoke();
    }
}

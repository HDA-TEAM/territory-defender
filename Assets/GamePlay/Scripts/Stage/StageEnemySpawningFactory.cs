using Cysharp.Threading.Tasks;
using GamePlay.Scripts.Character.StateMachine.EnemyStateMachine;
using GamePlay.Scripts.Data;
using GamePlay.Scripts.GamePlayController;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class StageEnemySpawningFactory : MonoBehaviour
{
    [SerializeField] private float _spawningEachObjectInterval;
    public StageEnemySpawningConfig SpawningConfig;
    private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
    private SingleStageSpawningConfig _curStageSpawningConfig;

    public void CancelSpawning()
    {
        _cancellationTokenSource.Cancel();
    }
   
    
    public async void StartSpawning(StageId stageId, Action onFinishedSpawning)
    {
        _curStageSpawningConfig = SpawningConfig.FindSpawningConfig(stageId);
        if (_curStageSpawningConfig.WavesSpawning.Count <= 0)
            return;

        Debug.Log("Spawning Starting");
        List<UniTask> spawnTask = new List<UniTask>();
        try
        {
            foreach (var waveSpawning in  _curStageSpawningConfig.WavesSpawning)
                spawnTask.Add(StartSpawningWave(waveSpawning));
            await UniTask.WhenAll(spawnTask);
            onFinishedSpawning?.Invoke();
        }
        catch (Exception e)
        {
            Debug.Log("Cancel spawning" + e);
        }
       
        Debug.Log("Spawning Ended");
    }
    private async UniTask StartSpawningWave(SingleStageSpawningConfig.WaveSpawning waveSpawning)
    {
        List<UniTask> spawnTask = new List<UniTask>();
        foreach (var groupSpawning in  waveSpawning.GroupsSpawning)
        {
            spawnTask.Add(StartSpawningGroup(groupSpawning));
        }
        await UniTask.WhenAll(spawnTask);
    }
    private async UniTask StartSpawningGroup(SingleStageSpawningConfig.GroupSpawning groupSpawning)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(groupSpawning.StartSpawning));
        for (int i = 0; i < groupSpawning.NumberSpawning; i++)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_spawningEachObjectInterval), cancellationToken: _cancellationTokenSource.Token);

            GameObject go = PoolingController.Instance.SpawnObject(groupSpawning.ObjectSpawn.ToString());

            Debug.Log("Spawning " + groupSpawning.ObjectSpawn);

            SetRoute(go, groupSpawning.RouteId);
            UpdateStats(go);
        }
    }
    
    private void SetRoute(GameObject go, int RouteId)
    {
        go.TryGetComponent(out BaseEnemyStateMachine component);
        component.RouteToGate = RouteSetController.Instance.CurrentSingleRouteLineRenderers[RouteId].SingleLineRenderer;
        go.transform.position = component.RouteToGate.GetPosition(0);
    }
    private void UpdateStats(GameObject go)
    {
        go.TryGetComponent(out UnitBase component);
        component.OnUpdateStats?.Invoke();
    }
}

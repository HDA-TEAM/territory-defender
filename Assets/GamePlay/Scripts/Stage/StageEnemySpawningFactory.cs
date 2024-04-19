using Common.Loading.Scripts;
using Cysharp.Threading.Tasks;
using GamePlay.Scripts.Character.StateMachine.EnemyStateMachine;
using GamePlay.Scripts.Data;
using GamePlay.Scripts.Data.StageSpawning;
using GamePlay.Scripts.GamePlayController;
using GamePlay.Scripts.Route;
using SuperMaxim.Messaging;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class StageEnemySpawningFactory : MonoBehaviour
{
    [SerializeField] private float _perWaveInterval = 10f;
    [SerializeField] private float _spawningEachObjectInterval;
    public StageEnemySpawningConfig SpawningConfig;
    private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
    private readonly CancellationTokenSource _cancellationTokenEarlyCallWave = new CancellationTokenSource();
    private SingleStageSpawningConfig _curStageSpawningConfig;
    private int _maxWave;

    public void SetUpNewGame(StartStageComposite startStageComposite)
    {
        Messenger.Default.Publish(new PrepareCallWaveButtonPayload
            {
                DurationEarlyCallWaveAvailable = 0f,
                WaveIndex = 0,
                OnEarlyCallWave = InGameStateController.Instance.StartSpawning,
            });
    }
    
    public void CancelSpawning()
    {
        _cancellationTokenSource.Cancel();
    }


    public async void StartSpawning(StageId stageId, Action onFinishedSpawning)
    {
        _curStageSpawningConfig = SpawningConfig.FindSpawningConfig(stageId);
        _maxWave = _curStageSpawningConfig.WavesSpawning.Count;
        if (_maxWave <= 0)
            return;

        Debug.Log("Spawning Starting");
        try
        {
            for (int waveIndex = 0; waveIndex < _curStageSpawningConfig.WavesSpawning.Count; waveIndex++)
            {
                SingleStageSpawningConfig.WaveSpawning waveSpawning = _curStageSpawningConfig.WavesSpawning[waveIndex];
                await StartSpawningWave(waveSpawning, waveIndex);
            }
            onFinishedSpawning?.Invoke();
        }
        catch (Exception e)
        {
            Debug.Log("Cancel spawning" + e);
        }

        Debug.Log("Spawning Ended");
    }
    private async UniTask StartSpawningWave(SingleStageSpawningConfig.WaveSpawning waveSpawning, int waveIndex)
    {
        Debug.Log("Spawning new Wave " + waveIndex);
        List<UniTask> spawnTask = new List<UniTask>();
        foreach (var groupSpawning in waveSpawning.GroupsSpawning)
        {
            spawnTask.Add(StartSpawningGroup(groupSpawning));
        }
        await UniTask.WhenAll(spawnTask);

        if (waveIndex < _maxWave - 2)
        {
            Debug.Log("Prepare next wave" + waveIndex + 1);
            Messenger.Default.Publish(new PrepareCallWaveButtonPayload
            {
                DurationEarlyCallWaveAvailable = _perWaveInterval,
                WaveIndex = waveIndex + 1,
                OnEarlyCallWave = OnEarlyCallWave,
            });
        }
        
        try
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_perWaveInterval), cancellationToken: _cancellationTokenEarlyCallWave.Token);  
        }
        catch (Exception)
        {
        }

    }
    private void OnEarlyCallWave()
    {
        Debug.Log("OnEarlyCallWave");
        _cancellationTokenEarlyCallWave.Cancel();
    }
    private async UniTask StartSpawningGroup(SingleStageSpawningConfig.GroupSpawning groupSpawning)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(groupSpawning.StartSpawning));
        for (int i = 0; i < groupSpawning.NumberSpawning; i++)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_spawningEachObjectInterval), cancellationToken: _cancellationTokenSource.Token);

            GameObject go = PoolingController.Instance.SpawnObject(groupSpawning.ObjectSpawn.ToString());

            //  Debug.Log("Spawning " + groupSpawning.ObjectSpawn);

            SetRoute(go, groupSpawning.RouteId);
            UpdateStats(go);
        }
    }

    private void SetRoute(GameObject go, int routeId)
    {
        go.TryGetComponent(out BaseEnemyStateMachine component);
        component.RouteToGate = RouteSetController.Instance.CurrentSingleRouteLineRenderers[routeId].SingleLineRenderer;
        go.transform.position = component.RouteToGate.GetPosition(0);
    }
    private void UpdateStats(GameObject go)
    {
        go.TryGetComponent(out UnitBase component);
        component.OnUpdateStats?.Invoke();
    }
}

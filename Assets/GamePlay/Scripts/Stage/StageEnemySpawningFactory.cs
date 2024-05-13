using Common.Loading.Scripts;
using Cysharp.Threading.Tasks;
using GamePlay.Scripts.Character.StateMachine.EnemyStateMachine;
using GamePlay.Scripts.Data.StageSpawning;
using GamePlay.Scripts.GamePlayController;
using GamePlay.Scripts.Route;
using SuperMaxim.Messaging;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace GamePlay.Scripts.Stage
{
    public struct UpdateWavePayload
    {
        public int CurWave;
        public int MaxWave;
    }

    public class StageEnemySpawningFactory : MonoBehaviour
    {
        [SerializeField] private float _perWaveInterval = 10f;
        [SerializeField] private float _spawningEachObjectInterval;
        [SerializeField] private StageEnemySpawningConfig _spawningConfig;
        private CancellationTokenSource _cancellationTokenSource;
        private readonly CancellationTokenSource _cancellationTokenEarlyCallWave = new CancellationTokenSource();
        private SingleStageSpawningConfig _curStageSpawningConfig;
        private int _maxWave;

        public void SetUpNewGame(StartStageComposite startStageComposite)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _curStageSpawningConfig = _spawningConfig.FindSpawningConfig(startStageComposite.StageId);
            _maxWave = _curStageSpawningConfig.WavesSpawning.Count;
            
            Messenger.Default.Publish(new UpdateWavePayload()
            {
                CurWave = 0,
                MaxWave = _maxWave,
            });
            
            Messenger.Default.Publish(new PrepareNextWavePayload
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

        public async void StartSpawning(Action onFinishedSpawning)
        {
            if (_maxWave <= 0)
                return;

            Debug.Log("Spawning Starting");
            try
            {
                for (int waveIndex = 0; waveIndex < _curStageSpawningConfig.WavesSpawning.Count; waveIndex++)
                {
                    Messenger.Default.Publish(new UpdateWavePayload()
                    {
                        CurWave = waveIndex + 1,
                        MaxWave = _maxWave,
                    });
                    
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
            
                Messenger.Default.Publish(new PrepareNextWavePayload
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
                // ignored
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
            component.RouteToGate = RouteSetController.Instance.ActiveSingleRouteLineRenderers[routeId].SingleLineRenderer;
            go.transform.position = component.RouteToGate.GetPosition(0);
        }
        private void UpdateStats(GameObject go)
        {
            go.TryGetComponent(out UnitBase component);
            component.OnUpdateStats?.Invoke();
        }
    }
}

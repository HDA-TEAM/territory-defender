using Common.Scripts;
using DG.Tweening;
using GamePlay.Scripts.Data;
using GamePlay.Scripts.Data.StageSpawning;
using GamePlay.Scripts.Route.PreviewCallWaveTooltip;
using GamePlay.Scripts.Tower.TowerKIT;
using SuperMaxim.Messaging;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Scripts.Route
{
    public struct PrepareNextWavePayload
    {
        public float DurationEarlyCallWaveAvailable;
        public int WaveIndex;
        public Action OnEarlyCallWave;
    }

    public class ListHandleShowCallWaveTooltip
    {
        public List<HandleSingleCallWaveShowTooltip> HandleSingleCallWaveShowTooltips;
        public StageEnemySpawningConfig EnemySpawningConfig;
        private StageId _stageId;
        private int _waveId;
        public ListHandleShowCallWaveTooltip(List<HandleSingleCallWaveShowTooltip> handleSingleCallWaveShowTooltips, StageEnemySpawningConfig enemySpawningConfig)
        {
            HandleSingleCallWaveShowTooltips = handleSingleCallWaveShowTooltips;
            EnemySpawningConfig = enemySpawningConfig;
        }
        public void SetActiveAll(bool isActive)
        {
            foreach (var handleSingleCallWaveShowTooltip in HandleSingleCallWaveShowTooltips)
                handleSingleCallWaveShowTooltip.gameObject.SetActive(isActive);
        }
        public void ShowTooltip(int routeId, StageId stageId, int waveId)
        {
            HandleSingleCallWaveShowTooltips[routeId].ShowTooltip(
                new CallWavePreviewUnitComposite
                {
                    UnitPreviewComposites = EnemySpawningConfig.FindSpawningConfig(stageId).WavesSpawning[waveId].GetPreviewUnits()
                });
        }
    }

    public class CallWaveViewModel : MonoBehaviour
    {
        [SerializeField] private StageEnemySpawningConfig _stageEnemySpawningConfig;

        [Header("Sounds"), Space(12)]
        [SerializeField] private AudioClip _audioClipCallWave;
        [SerializeField] private ConfirmHandle _confirmHandle;
        
        private ListHandleShowCallWaveTooltip _listHandleShowCallWaveTooltip;
        private List<CallWaveBtnView> _callWaveViews;
        private StageId _stageId;
        private Action _onEarlyCallWave;
        private Tween _tweenAutoHidingCallWave;
        private int _curWaveId;
        
        private void Awake()
        {
            Messenger.Default.Subscribe<PrepareNextWavePayload>(PrepareShowCallWave);
        }
        private void OnDestroy()
        {
            if (_tweenAutoHidingCallWave != null)
                _tweenAutoHidingCallWave.Kill();
            Messenger.Default.Unsubscribe<PrepareNextWavePayload>(PrepareShowCallWave);
        }
        public void Setup(List<CallWaveBtnView> callWaveViews, List<HandleSingleCallWaveShowTooltip> handleCallWaveShowTooltips, StageId stageId)
        {
            _listHandleShowCallWaveTooltip = new ListHandleShowCallWaveTooltip(handleCallWaveShowTooltips, _stageEnemySpawningConfig);
            _callWaveViews = callWaveViews;
            _stageId = stageId;
            HidingAllCallWaveButton();
            
            SetupClickCallWaveButton(callWaveViews);
        }
        private void PrepareShowCallWave(PrepareNextWavePayload payload)
        {
            if (_tweenAutoHidingCallWave != null)
                _tweenAutoHidingCallWave.Kill();

            _curWaveId = payload.WaveIndex;
            
            ShowCallWaveButton(payload.WaveIndex);
            _onEarlyCallWave = payload.OnEarlyCallWave;

            if (payload.DurationEarlyCallWaveAvailable > 0)
            {
                _tweenAutoHidingCallWave = DOVirtual.DelayedCall(payload.DurationEarlyCallWaveAvailable, HidingAllCallWaveButton, ignoreTimeScale: false);
            }
        }
        
        private void OnClickCallWave(int routeId)
        {
            Messenger.Default.Publish(new AudioPlayOneShotPayload
            {
                AudioClip = _audioClipCallWave,
            });

            _onEarlyCallWave?.Invoke();

            HidingAllCallWaveButton();
            _listHandleShowCallWaveTooltip.SetActiveAll(false);
            _listHandleShowCallWaveTooltip.ShowTooltip(routeId, _stageId,_curWaveId);
        }
        private void SetupClickCallWaveButton(List<CallWaveBtnView> callWaveViews)
        {
            int routeId = 0;
            foreach (var callWaveView in callWaveViews)
                callWaveView.Setup(OnClickCallWave, routeId++);
        }
        private void HidingAllCallWaveButton()
        {
            foreach (var callWaveView in _callWaveViews)
                callWaveView.gameObject.SetActive(false);
        }
        private void ShowCallWaveButton(int waveIndex)
        {
            List<int> routesHasSpawning = _stageEnemySpawningConfig.FindSpawningConfig(stageId: _stageId).WavesSpawning[waveIndex].GetRoutesHasSpawningInThisWave();
            foreach (var routeId in routesHasSpawning)
                _callWaveViews[routeId].gameObject.SetActive(true);
        }
    }
}

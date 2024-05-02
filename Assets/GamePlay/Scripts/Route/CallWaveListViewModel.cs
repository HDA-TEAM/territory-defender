using Common.Scripts;
using DG.Tweening;
using GamePlay.Scripts.Data;
using GamePlay.Scripts.Data.StageSpawning;
using GamePlay.Scripts.Route.PreviewCallWaveTooltip;
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

    public class CallWaveListViewModel : MonoBehaviour
    {
        [Header("Data"), Space(12)]
        [SerializeField] private StageEnemySpawningConfig _stageEnemySpawningConfig;

        [Header("Sounds"), Space(12)]
        [SerializeField] private AudioClip _audioClipCallWave;

        private ListHandleShowCallWaveTooltip _listHandleShowCallWaveTooltip;
        private ListCallWaveBtnViewModel _listCallWaveBtnViewModel;
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
        public void Setup(List<CallWaveBtnViewModel> callWaveBtnViewModels, List<HandleSingleCallWaveShowTooltip> handleCallWaveShowTooltips, StageId stageId)
        {
            _listHandleShowCallWaveTooltip = new ListHandleShowCallWaveTooltip(handleCallWaveShowTooltips, _stageEnemySpawningConfig);
            _listCallWaveBtnViewModel = new ListCallWaveBtnViewModel(callWaveBtnViewModels, _stageEnemySpawningConfig);

            _stageId = stageId;

            _listCallWaveBtnViewModel.HidingAllCallWaveButton();

            SetupClickCallWaveButton(callWaveBtnViewModels);
        }
        private void PrepareShowCallWave(PrepareNextWavePayload payload)
        {
            if (_tweenAutoHidingCallWave != null)
                _tweenAutoHidingCallWave.Kill();

            _curWaveId = payload.WaveIndex;

            _listCallWaveBtnViewModel.ShowCallWaveButton(_stageId, payload.WaveIndex);
            _onEarlyCallWave = payload.OnEarlyCallWave;

            if (payload.DurationEarlyCallWaveAvailable > 0)
            {
                _tweenAutoHidingCallWave = DOVirtual.DelayedCall(
                    payload.DurationEarlyCallWaveAvailable,
                    _listCallWaveBtnViewModel.HidingAllCallWaveButton,
                    ignoreTimeScale: false);
            }
        }

        private void OnAcceptedCallWave(int routeId)
        {
            Messenger.Default.Publish(new AudioPlayOneShotPayload
            {
                AudioClip = _audioClipCallWave,
            });

            _onEarlyCallWave?.Invoke();

            _listCallWaveBtnViewModel.HidingAllCallWaveButton();
        }
        private void OnPreviewUnitSpawning(int routeId)
        {
            _listHandleShowCallWaveTooltip.ShowTooltip(routeId, _stageId, _curWaveId);
        }
        private void SetupClickCallWaveButton(List<CallWaveBtnViewModel> callWaveViewModels)
        {
            int routeId = 0;
            foreach (var callWaveView in callWaveViewModels)
                callWaveView.Setup(OnAcceptedCallWave, OnPreviewUnitSpawning, routeId++);
        }

    }
}

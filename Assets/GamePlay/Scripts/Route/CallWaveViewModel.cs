using Common.Scripts;
using DG.Tweening;
using GamePlay.Scripts.Data;
using GamePlay.Scripts.Data.StageSpawning;
using SuperMaxim.Messaging;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Scripts.Route
{
    public struct PrepareCallWaveButtonPayload
    {
        public float DurationEarlyCallWaveAvailable;
        public int WaveIndex;
        public Action OnEarlyCallWave;
    }
    public class CallWaveViewModel : MonoBehaviour
    {
        [SerializeField] private StageEnemySpawningConfig _stageEnemySpawningConfig;
        
        [Header("Sounds"), Space(12)]
        [SerializeField] private AudioClip _audioClipCallWave;
        
        private List<CallWaveView> _callWaveViews;
        private StageId _stageId;
        private Action _onEarlyCallWave;
        private Tween _tweenAutoHidingCallWave;
        private void Awake()
        {
            Messenger.Default.Subscribe<PrepareCallWaveButtonPayload>(PrepareShowCallWave);
        }
        private void OnDestroy()
        {
            if (_tweenAutoHidingCallWave != null)
                _tweenAutoHidingCallWave.Kill();
            Messenger.Default.Unsubscribe<PrepareCallWaveButtonPayload>(PrepareShowCallWave);
        }
        public void Setup(List<CallWaveView> callWaveViews, StageId stageId)
        {
            _callWaveViews = callWaveViews;
            _stageId = stageId;
            HidingAllCallWaveButton();
            SetupClickCallWaveButton(callWaveViews);
        }
        private void PrepareShowCallWave(PrepareCallWaveButtonPayload payload)
        {
            if (_tweenAutoHidingCallWave != null)
                _tweenAutoHidingCallWave.Kill();
            
            ShowCallWaveButton(payload.WaveIndex);
            _onEarlyCallWave = payload.OnEarlyCallWave;

            if (payload.DurationEarlyCallWaveAvailable > 0)
            {
                _tweenAutoHidingCallWave = DOVirtual.DelayedCall(payload.DurationEarlyCallWaveAvailable, HidingAllCallWaveButton, ignoreTimeScale: false);
            }
        }
        
        private void OnClickCallWave(int routeId)
        {
            Debug.Log("Route id click " + routeId);
            
            Messenger.Default.Publish(new AudioPlayOneShotPayload
            {
                AudioClip = _audioClipCallWave,
            });
            
            _onEarlyCallWave?.Invoke();
            
            HidingAllCallWaveButton();  
        }
        private void SetupClickCallWaveButton(List<CallWaveView> callWaveViews)
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

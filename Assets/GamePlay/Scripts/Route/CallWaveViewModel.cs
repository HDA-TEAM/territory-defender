using GamePlay.Scripts.Data;
using GamePlay.Scripts.Data.StageSpawning;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Scripts.Route
{
    public class CallWaveViewModel : MonoBehaviour
    {
        private List<CallWaveView> _callWaveViews;
        private StageId _stageId;
        [SerializeField] private StageEnemySpawningConfig _stageEnemySpawningConfig;

        public void Setup(List<CallWaveView> callWaveViews, StageId stageId)
        {
            _callWaveViews = callWaveViews;
            _stageId = stageId;
            HidingAllCallWaveButton();
            SetupClickCallWaveButton(callWaveViews);
            ShowCallWaveButton();
        }
        private void OnClickCallWave()
        {
            
        }
        private void SetupClickCallWaveButton(List<CallWaveView> callWaveViews)
        {
            foreach (var callWaveView in callWaveViews)
                callWaveView.Setup(OnClickCallWave);
        }
        private void HidingAllCallWaveButton()
        {
            foreach (var callWaveView in _callWaveViews)
                callWaveView.gameObject.SetActive(false);
        }
        private void ShowCallWaveButton()
        {
            List<int> routesHasSpawning = _stageEnemySpawningConfig.FindSpawningConfig(stageId: _stageId).WavesSpawning[0].GetRoutesHasSpawningInThisWave();
            foreach (var routeId in routesHasSpawning)
                _callWaveViews[routeId].gameObject.SetActive(true);
        }
    }
}

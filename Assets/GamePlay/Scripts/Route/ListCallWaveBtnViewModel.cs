using GamePlay.Scripts.Data;
using GamePlay.Scripts.Data.StageSpawning;
using System.Collections.Generic;

namespace GamePlay.Scripts.Route
{
    public class ListCallWaveBtnViewModel
    {
        private readonly List<CallWaveBtnViewModel> _callWaveBtnViewModels;
        private readonly StageEnemySpawningConfig _stageEnemySpawningConfig;

        public ListCallWaveBtnViewModel(List<CallWaveBtnViewModel> callWaveBtnViewModels, StageEnemySpawningConfig stageEnemySpawningConfig)
        {
            _callWaveBtnViewModels = callWaveBtnViewModels;
            _stageEnemySpawningConfig = stageEnemySpawningConfig;
        }
        public void HidingAllCallWaveButton()
        {
            foreach (var callWaveView in _callWaveBtnViewModels)
                callWaveView.gameObject.SetActive(false);
        }
        public void ShowCallWaveButton(StageId stageId, int waveIndex)
        {
            List<int> routesHasSpawning = _stageEnemySpawningConfig.FindSpawningConfig(stageId: stageId).WavesSpawning[waveIndex].GetRoutesHasSpawningInThisWave();
            foreach (var routeId in routesHasSpawning)
                _callWaveBtnViewModels[routeId].gameObject.SetActive(true);
        }
    }
}

using GamePlay.Scripts.Data;
using GamePlay.Scripts.Data.StageSpawning;
using System.Collections.Generic;

namespace GamePlay.Scripts.Route.PreviewCallWaveTooltip
{
    public class ListHandleShowCallWaveTooltip
    {
        private readonly List<HandleSingleCallWaveShowTooltip> _handleSingleCallWaveShowTooltips;
        private readonly StageEnemySpawningConfig _enemySpawningConfig;
        private StageId _stageId;
        private int _waveId;
        public ListHandleShowCallWaveTooltip(List<HandleSingleCallWaveShowTooltip> handleSingleCallWaveShowTooltips, StageEnemySpawningConfig enemySpawningConfig)
        {
            _handleSingleCallWaveShowTooltips = handleSingleCallWaveShowTooltips;
            _enemySpawningConfig = enemySpawningConfig;
        }
        private void SetActiveAll(bool isActive)
        {
            foreach (var handleSingleCallWaveShowTooltip in _handleSingleCallWaveShowTooltips)
                handleSingleCallWaveShowTooltip.gameObject.SetActive(isActive);
        }
        public void ShowTooltip(int routeId, StageId stageId, int waveId)
        {
            SetActiveAll(false);
            _handleSingleCallWaveShowTooltips[routeId].gameObject.SetActive(true);
            _handleSingleCallWaveShowTooltips[routeId].ShowTooltip(
                new CallWavePreviewUnitComposite
                {
                    DirectionType = _handleSingleCallWaveShowTooltips[routeId].ECallWaveUnitPreviewDirectionType,
                    UnitPreviewComposites = _enemySpawningConfig.FindSpawningConfig(stageId).WavesSpawning[waveId].GetPreviewUnits(),
                });
        }
    }
}

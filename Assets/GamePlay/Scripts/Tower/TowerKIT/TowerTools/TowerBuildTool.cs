using GamePlay.Scripts.Character.Stats;
using GamePlay.Scripts.Tower.TowerKIT.PreviewTooltip;
using UnityEngine;

namespace GamePlay.Scripts.Tower.TowerKIT.TowerTools
{
    public class TowerBuildTool : TowerToolBase
    {
        [SerializeField] private UnitId.Tower _towerBuildId;
        private UnitBase _towerCanBuild;
        protected void OnEnable()
        {
            _towerToolStatusHandle.SetUp(CheckCurrencyIsEnough() ? TowerTooltatus.Available : TowerTooltatus.UnAvailable);
        }
        private bool CheckCurrencyIsEnough()
        {
            // Checked enough coin to build
            _towerCanBuild = _towerDataConfig.GetUnitConfigById(_towerBuildId).UnitBase;
            var towerStats = _towerCanBuild.UnitStatsHandlerComp();
            return towerStats.GetCurrentStatValue(StatId.CoinNeedToBuild) <= _inGameInventoryRuntimeData.GetCurrencyValue();
        }
        protected override void ApplyTool()
        {
            GameObject tower = Instantiate(_towerCanBuild.gameObject);
            TowerKitSetController.Instance.CurrentSelectedKit.SetTower(tower, _towerBuildId);
        }
        protected override void ShowPreviewChanging()
        {
            TowerKitSetController.Instance.CurrentSelectedKit.ShowPreviewChanging(
                new PreviewBuiltTowerTooltip(_towerDataConfig, _towerBuildId)
            );
        }
    }
}

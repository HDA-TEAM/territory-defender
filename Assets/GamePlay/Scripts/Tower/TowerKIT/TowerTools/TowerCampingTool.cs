using GamePlay.Scripts.Character.Stats;
using GamePlay.Scripts.GamePlayController;

namespace GamePlay.Scripts.Tower.TowerKIT.TowerTools
{
    public class TowerCampingTool : TowerToolBase
    {
        protected void OnEnable()
        {
            _towerToolStatusHandle.SetUp(IsTroopTower() ? TowerTooltatus.Available : TowerTooltatus.Block);
        }
        private bool IsTroopTower()
        {
            var unitBase = _towerKit.GetUnitBase();
            var towerStats = unitBase.UnitStatsHandlerComp();
            return towerStats.GetCurrentStatValue(StatId.CampingRange) > 0;
        }
        protected override void ApplyTool()
        {
            _towerKit.ActiveCampingMode();
        }
    }
}

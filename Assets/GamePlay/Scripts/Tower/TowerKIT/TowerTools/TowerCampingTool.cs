using GamePlay.Scripts.Character.Stats;
using UnityEngine;

public class TowerCampingTool : TowerToolBase
{
    protected void OnEnable()
    {
        _towerToolStatusHandle.SetUp(IsTroopTower() ? TowerTooltatus.Available : TowerTooltatus.Block);
    }
    private bool IsTroopTower()
    {
        if (TowerKitSetController.Instance.CurrentSelectedKit)
        {
            var unitBase = TowerKitSetController.Instance.CurrentSelectedKit.GetUnitBase();
            var towerStats = unitBase.UnitStatsHandlerComp();
            return towerStats.GetCurrentStatValue(StatId.CampingRange) > 0;
        }
        return true;
    }
    protected override void ApplyTool()
    {
        TowerKitSetController.Instance.CurrentSelectedKit.ActiveCampingMode();
    }
}

using GamePlay.Scripts.Character.Stats;
using System;
using UnityEngine;

public class TowerBuildTool : TowerToolBase
{
    [SerializeField] private TowerId _towerBuildId;
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
    protected override void Apply()
    {
        GameObject tower = Instantiate(_towerCanBuild.gameObject);
        TowerKitSetController.Instance.CurrentSelectedKit.SetTower(tower, _towerBuildId);
    }
}

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
        _towerCanBuild = _towerDataAsset.GetTowerType(_towerBuildId);
        var towerStats = _towerCanBuild.UnitStatsHandlerComp();
        return towerStats.GetCurrentStatValue(StatId.CoinNeedToBuild) <= _inGameInventoryRuntimeData.GetCurrencyValue();
    }
    protected override void Apply()
    { 
        // Checked enough coin to build
        _inGameInventoryRuntimeData.TryChangeCurrency(
            - (int)_towerCanBuild.UnitStatsHandlerComp().GetCurrentStatValue(StatId.CoinNeedToBuild));
        
        GameObject tower = Instantiate(_towerCanBuild.gameObject);
        TowerKitSetController.Instance.CurrentSelectedKit.SetTower(tower, _towerBuildId);
    }
}

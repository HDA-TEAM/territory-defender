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
        var towerStats = _towerCanBuild.UnitStatsComp();
        return towerStats.GetStat(StatId.CoinNeedToBuild) <= _inGameInventoryDataAsset.GetCurrencyValue();
    }
    protected override void Apply()
    { 
        // Checked enough coin to build
        _inGameInventoryDataAsset.TryChangeCurrency(
            - (int)_towerCanBuild.UnitStatsComp().GetStat(StatId.CoinNeedToBuild));
        
        GameObject tower = Instantiate(_towerCanBuild.gameObject);
        TowerKitSetController.Instance.CurrentSelectedKit.SetTower(tower, _towerBuildId);
    }
}

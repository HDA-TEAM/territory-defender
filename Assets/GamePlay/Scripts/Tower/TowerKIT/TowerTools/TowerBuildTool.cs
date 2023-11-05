using System;
using UnityEngine;

public class TowerBuildTool : TowerToolBase
{
    [SerializeField] private TowerId _towerBuildId;
    
    protected void OnEnable()
    {
        _towerToolStatusHandle.SetUp(CheckCurrencyIsEnough() ? TowerTooltatus.Available : TowerTooltatus.UnAvailable);
    }
    private bool CheckCurrencyIsEnough()
    {
        UnitBase towerBase = _towerDataAsset.GetTowerType(_towerBuildId);
        var towerStats = towerBase.UnitStatsComp();
        return towerStats.GetStat(StatId.CoinNeedToBuild) <= _inGameInventoryDataAsset.GetCurrencyValue();
    }
    protected override void Apply()
    { 
        UnitBase towerBase = _towerDataAsset.GetTowerType(_towerBuildId);
        Instantiate(towerBase.gameObject);
        TowerKitSetController.Instance.CurrentSelectedKit.SetTower(towerBase);
    }
}

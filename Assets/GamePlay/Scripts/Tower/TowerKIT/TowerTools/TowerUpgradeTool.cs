using GamePlay.Scripts.Tower.TowerKIT.TowerTools;
using UnityEngine;

public class TowerUpgradeTool : TowerToolBase
{
    // [SerializeField] private TowerId _towerBuildId;
    //
    // protected void OnEnable()
    // {
    //     _towerToolStatusHandle.SetUp(CheckCurrencyIsEnough() ? TowerTooltatus.Available : TowerTooltatus.UnAvailable);
    // }
    // private bool CheckCurrencyIsEnough()
    // {
    //     UnitBase towerBase = _towerDataAsset.GetTowerType(_towerBuildId);
    //     var towerStats = towerBase.UnitStatsComp();
    //     return towerStats.GetStat(StatId.CoinNeedToUpgrade) <= _inGameInventoryDataAsset.GetCurrencyValue();
    // }
    // protected override void Apply()
    // {
    //     TowerKitSetController.Instance.CurrentSelectedKit.UpgradeTower(tower);
    // } 
}

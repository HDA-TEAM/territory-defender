using UnityEngine;

public class TowerSellingTool : TowerToolBase
{
    protected override void Apply()
    {
        // _inGameInventoryDataAsset.TryChangeCurrency(
        //     - (int)_towerCanBuild.UnitStatsComp().GetStat(StatId.CoinNeedToBuild));
        TowerKitSetController.Instance.CurrentSelectedKit.SellingTower();
    }
}

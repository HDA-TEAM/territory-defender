using UnityEngine;

public class TowerSellingTool : TowerToolBase
{
    protected override void Apply() => TowerKitSetController.Instance.CurrentSelectedKit.SellingTower();
}

public class TowerSellingTool : TowerToolBase
{
    protected override void ApplyTool()
    {
        TowerKitSetController.Instance.CurrentSelectedKit.SellingTower();
    }
}

using GamePlay.Scripts.Tower.TowerKIT.PreviewTooltip;

namespace GamePlay.Scripts.Tower.TowerKIT.TowerTools
{
    public class TowerSellingTool : TowerToolBase
    {
        protected override void ApplyTool()
        {
            _towerKit.SellingTower();
        }
        protected override void ShowPreviewChanging()
        {
            _towerKit.ShowPreviewChanging(
                new TowerPreviewSoldToolTipComposite(_towerKit.GetSoldTowerCoin().ToString()
                )
            );
        }
    }
}

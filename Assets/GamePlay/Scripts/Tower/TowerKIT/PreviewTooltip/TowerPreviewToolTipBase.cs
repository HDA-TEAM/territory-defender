using Common.Scripts;
using GamePlay.Scripts.Character.Stats;
using GamePlay.Scripts.Data;
using GamePlay.Scripts.Menu.UnitInformationPanel;
using System.Collections.Generic;

namespace GamePlay.Scripts.Tower.TowerKIT.PreviewTooltip
{
    public abstract class TowerPreviewToolTipBase
    {
        protected string _title;
        protected string _desc;
        protected List<ItemStatComposite> _statComposites;
        public PreviewTooltipComposite GetPreviewTooltipComposite()
        {
            return new PreviewTooltipComposite
            {
                Title = _title,
                Desc = _desc,
                StatComposites = _statComposites
            };
        }
    }

    public class TowerPreviewBuiltTowerToolTip : TowerPreviewToolTipBase
    {
        public TowerPreviewBuiltTowerToolTip(TowerDataConfigBase towerDataConfigBase, UnitId.Tower towerId)
        {
            StatsHandlerComponent towerStats = towerDataConfigBase.GetUnitConfigById(towerId).UnitBase.UnitStatsHandlerComp();
            _statComposites = towerStats.GetShowStatsInformation().StatComposites;
            _desc = towerStats.GetBaseStats().GetInformation(InformationId.Description);
            _title = towerStats.GetBaseStats().GetInformation(InformationId.Name);
        }
    }
    public class TowerPreviewSoldToolTipComposite : TowerPreviewToolTipBase
    {
        public TowerPreviewSoldToolTipComposite(string price)
        {
            _title = "SELL TOWER";
            _desc = $"SELL THIS TOWER AND GET A {price} COINS REFUND.";
            _statComposites = null;
        }
    }
    public struct PreviewTooltipComposite 
    {
        public string Title;
        public string Desc;
        public List<ItemStatComposite> StatComposites;
    }
}

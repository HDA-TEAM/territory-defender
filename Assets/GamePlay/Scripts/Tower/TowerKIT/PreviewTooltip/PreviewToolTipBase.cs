using GamePlay.Scripts.Character.Stats;
using GamePlay.Scripts.Data;
using GamePlay.Scripts.Menu.UnitInformationPanel;
using System.Collections.Generic;

namespace GamePlay.Scripts.Tower.TowerKIT.PreviewTooltip
{
    public abstract class PreviewTooltipBase
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

    public class PreviewBuiltTowerTooltip : PreviewTooltipBase
    {
        public PreviewBuiltTowerTooltip(TowerDataConfig towerDataConfig, UnitId.Tower towerId)
        {
            StatsHandlerComponent towerStats = towerDataConfig.GetUnitConfigById(towerId).UnitBase.UnitStatsHandlerComp();
            _statComposites = towerStats.GetShowStatsInformation().StatComposites;
            _desc = towerStats.GetBaseStats().GetInformation(InformationId.Description);
            _title = towerStats.GetBaseStats().GetInformation(InformationId.Name);
        }
    }
    public class PreviewSoldTooltipComposite : PreviewTooltipBase
    {
        public PreviewSoldTooltipComposite(string price)
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

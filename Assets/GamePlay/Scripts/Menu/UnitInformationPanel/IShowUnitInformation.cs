using GamePlay.Scripts.Character.Stats;
using System;
using System.Collections.Generic;

namespace GamePlay.Scripts.Menu.UnitInformationPanel
{
    public interface IShowUnitInformation
    {
        public ShowStatsInformationComposite GetShowStatsInformation(Stats stats);
    }

    public class ShowStatInformationSelector
    {
        public IShowUnitInformation GetShowUnitInformation(UnitId.BaseId unitSideId)
        {
            switch (unitSideId)
            {
                case UnitId.BaseId.Enemy:
                    {
                        return new AllyShowInformation();
                    }
                case UnitId.BaseId.Ally:
                    {
                        return new AllyShowInformation();
                    }
                case UnitId.BaseId.Tower:
                    {
                        return new TowerShowInformation();
                    }
                default: return new AllyShowInformation();
            }
        }
    }
    public class AllyShowInformation : IShowUnitInformation
    {
        public ShowStatsInformationComposite GetShowStatsInformation(Stats stats)
        {
            ShowStatsInformationComposite statsInformationComposite = new ShowStatsInformationComposite();
            List<ItemStatComposite> statComposites = new List<ItemStatComposite>
            {
                new ItemStatComposite
                {
                    StatId = StatId.MaxHeal,
                    StatVal = stats.GetStat(StatId.MaxHeal).ToString(),
                },
                new ItemStatComposite
                {
                    StatId = StatId.AttackDamage,
                    StatVal = stats.GetStat(StatId.AttackDamage).ToString(),
                },
                new ItemStatComposite
                {
                    StatId = StatId.AttackRange,
                    StatVal = stats.GetStat(StatId.AttackRange).ToString(),
                },
            };
            statsInformationComposite.Name = stats.GetInformation(InformationId.Name);
            statsInformationComposite.StatComposites = statComposites;
            return statsInformationComposite;
        }
    }
    public class TowerShowInformation : IShowUnitInformation
    {
        public ShowStatsInformationComposite GetShowStatsInformation(Stats stats)
        {
            ShowStatsInformationComposite statsInformationComposite = new ShowStatsInformationComposite();
            List<ItemStatComposite> statComposites = new List<ItemStatComposite>
            {
                new ItemStatComposite
                {
                    StatId = StatId.AttackDamage,
                    StatVal = stats.GetStat(StatId.AttackDamage).ToString(),
                },
                new ItemStatComposite
                {
                    StatId = StatId.AttackRange,
                    StatVal = stats.GetStat(StatId.AttackRange).ToString(),
                },
            };
            statsInformationComposite.Name = stats.GetInformation(InformationId.Name);
            statsInformationComposite.StatComposites = statComposites;
            return statsInformationComposite;
        }
    }
}

using System;

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
            throw new NotImplementedException();
        }
    }
    public class TowerShowInformation : IShowUnitInformation
    {
        public ShowStatsInformationComposite GetShowStatsInformation(Stats stats)
        {
            throw new NotImplementedException();
        }
    }
}

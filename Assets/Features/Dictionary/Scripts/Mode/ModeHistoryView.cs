using System;
public class ModeHistoryView : ModeBaseView, ISetupHeroViewMode
{
    public void Setup(HeroComposite heroComposite, EHeroViewMode eHeroViewMode, Action<EHeroViewMode> selectAction)
    {
        HeroComposite = heroComposite;
        _onSelectedButton = selectAction;
    }
}


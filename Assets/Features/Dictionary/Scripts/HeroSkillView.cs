using System;

public class HeroSkillView : HeroModeBaseView, ISetupHeroViewMode
{
    public void Setup(HeroComposite heroComposite, EHeroViewMode eHeroViewMode, Action<EHeroViewMode> selectAction)
    {
        HeroComposite = heroComposite;
        _onSelectedButton = selectAction;
    }
    
}

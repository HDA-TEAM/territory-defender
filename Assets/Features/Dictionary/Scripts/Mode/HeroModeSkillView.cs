using System;

public class HeroModeSkillView : HeroModeBaseView, ISetupHeroViewMode
{
    public void Setup(HeroComposite heroComposite, EHeroViewMode eHeroViewMode, Action<EHeroViewMode> selectAction)
    {
        //HeroComposite = heroComposite;
        _onSelectedButton = selectAction;
    }
}

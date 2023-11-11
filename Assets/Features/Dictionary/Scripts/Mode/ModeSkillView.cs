using System;

public class ModeSkillView : ModeBaseView, ISetupHeroViewMode
{
    public void Setup(HeroComposite heroComposite, EHeroViewMode eHeroViewMode, Action<EHeroViewMode> selectAction)
    {
        HeroComposite = heroComposite;
        _onSelectedButton = selectAction;
    }
}

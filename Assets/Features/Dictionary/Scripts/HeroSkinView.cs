using System;

public interface ISetupHeroViewMode
{
    public void Setup(HeroComposite heroComposite, EHeroViewMode eHeroViewMode, Action<EHeroViewMode> selectAction);
    void SetSelectedState(bool isSelected);
}

public class HeroSkinView :  HeroModeBaseView , ISetupHeroViewMode
{
    public void Setup(HeroComposite heroComposite, EHeroViewMode eHeroViewMode, Action<EHeroViewMode> selectAction)
    {
        HeroComposite = heroComposite;
        _onSelectedButton = selectAction;

    }
    
    
}

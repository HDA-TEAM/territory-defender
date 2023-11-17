using System;

public enum EHeroItemView
{
    ItemHeroView,
    ItemSkillView
}

public interface ISetupHeroItemView
{
    public void Setup(HeroComposite heroComposite, Action<EHeroItemView> selectAction);
    //void SetSelectedState(bool isSelected);
}

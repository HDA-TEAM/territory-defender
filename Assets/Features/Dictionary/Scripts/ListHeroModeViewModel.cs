using Features.Dictionary.Scripts;
using UnityEngine;

public enum EHeroViewMode
{
    Skill,
    Skin,
    History,
}
public class ListHeroModeViewModel : MonoBehaviour
{
    [SerializeField] private HeroSkillView _heroSkillView;
    [SerializeField] private HeroSkinView _heroSkinView;
    [SerializeField] private HeroHistoryView _heroHistoryView;

    public void Setup(HeroComposite heroComposite, EHeroViewMode eHeroViewMode)
    {
        _heroSkillView.Setup(heroComposite,eHeroViewMode);
    }
}

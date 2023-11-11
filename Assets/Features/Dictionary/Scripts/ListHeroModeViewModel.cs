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

    private ISetupHeroViewMode _selectedViewMode;
    private EHeroViewMode _currentViewMode;
    private HeroComposite _heroComposite;

    public void Setup(HeroComposite heroComposite, EHeroViewMode eHeroViewMode)
    {
        _heroComposite = heroComposite;

        // Set up the default view (HeroSkillView)
        _heroSkillView.Setup(heroComposite, EHeroViewMode.Skill, OnHeroModeViewSelected);
        _heroSkinView.Setup(heroComposite, EHeroViewMode.Skin, OnHeroModeViewSelected);
        _selectedViewMode = _heroSkillView;
    }

    private void UpdateModeView(EHeroViewMode newViewMode)
    {
        // Only update if the view mode is different
        if (newViewMode != _currentViewMode)
        {
            _currentViewMode = newViewMode;

            // Handle the setup for different view modes
            switch (newViewMode)
            {
                case EHeroViewMode.Skin:
                    SetSelectedViewMode(_heroSkinView);
                    break;

                case EHeroViewMode.Skill:
                    SetSelectedViewMode(_heroSkillView);
                    break;

                case EHeroViewMode.History:
                    //SetSelectedViewMode(_heroHistoryView);
                    break;

                default:
                    // Handle other cases if needed
                    break;
            }
        }
    }

    private void SetSelectedViewMode(ISetupHeroViewMode newHeroViewMode)
    {
        // Deselect the previous view
        if (_selectedViewMode != null)
        {
            _selectedViewMode.SetSelectedState(false);
        }

        // Set up and select the new view
        _selectedViewMode = newHeroViewMode;
        _selectedViewMode.Setup(_heroComposite, _currentViewMode, OnHeroModeViewSelected);
        _selectedViewMode.SetSelectedState(true);
    }

    private void OnHeroModeViewSelected(EHeroViewMode eHeroViewMode)
    {
        // Action to perform when a HeroView is selected
        UpdateModeView(eHeroViewMode);
    }
}


using UnityEngine;
using UnityEngine.Serialization;

public enum EHeroViewMode
{
    Skill,
    Skin,
    History,
}

public class ListModeViewModel : MonoBehaviour
{
    [SerializeField] private HeroModeSkillView _heroModeSkillView;
    [SerializeField] private HeroModeSkinView _heroModeSkinView;
    [SerializeField] private HeroModeHistoryView _heroModeHistoryView;

    private ISetupHeroViewMode _selectedViewMode;
    private EHeroViewMode _currentViewMode;
    private HeroComposite _heroComposite;

    public void Setup(HeroComposite heroComposite, EHeroViewMode eHeroViewMode)
    {
        _heroComposite = heroComposite;

        // Set up the default view (HeroSkillView)
        _heroModeSkillView.Setup(heroComposite, EHeroViewMode.Skill, OnHeroModeViewSelected);
        _heroModeSkinView.Setup(heroComposite, EHeroViewMode.Skin, OnHeroModeViewSelected);
        _heroModeHistoryView.Setup(heroComposite, EHeroViewMode.History, OnHeroModeViewSelected);
        _selectedViewMode = _heroModeSkillView;
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
                    SetSelectedViewMode(_heroModeSkinView);
                    break;

                case EHeroViewMode.Skill:
                    SetSelectedViewMode(_heroModeSkillView);
                    break;

                case EHeroViewMode.History:
                    SetSelectedViewMode(_heroModeHistoryView);
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


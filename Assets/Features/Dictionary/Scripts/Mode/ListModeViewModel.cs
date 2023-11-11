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
    [SerializeField] private ModeSkillView _modeSkillView;
    [SerializeField] private ModeSkinView _modeSkinView;
    [SerializeField] private ModeHistoryView _modeHistoryView;

    private ISetupHeroViewMode _selectedViewMode;
    private EHeroViewMode _currentViewMode;
    private HeroComposite _heroComposite;

    public void Setup(HeroComposite heroComposite, EHeroViewMode eHeroViewMode)
    {
        _heroComposite = heroComposite;

        // Set up the default view (HeroSkillView)
        _modeSkillView.Setup(heroComposite, EHeroViewMode.Skill, OnHeroModeViewSelected);
        _modeSkinView.Setup(heroComposite, EHeroViewMode.Skin, OnHeroModeViewSelected);
        _modeHistoryView.Setup(heroComposite, EHeroViewMode.History, OnHeroModeViewSelected);
        _selectedViewMode = _modeSkillView;
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
                    SetSelectedViewMode(_modeSkinView);
                    break;

                case EHeroViewMode.Skill:
                    SetSelectedViewMode(_modeSkillView);
                    break;

                case EHeroViewMode.History:
                    SetSelectedViewMode(_modeHistoryView);
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


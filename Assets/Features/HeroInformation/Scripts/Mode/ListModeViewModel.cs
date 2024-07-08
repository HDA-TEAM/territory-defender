using Features.HeroInformation.Scripts.Hero;
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

    [SerializeField] private HeroDetailView _heroDetailView;
    [SerializeField] private SkinPageView _skinPageView;
    [SerializeField] private HistoryPageView _historyPageView;
    
    private ISetupHeroViewMode _selectedViewMode;
    private IHeroModePageView _selectedPageView;
    
    private EHeroViewMode _currentViewMode;
    private HeroComposite _heroComposite;

    public void Setup(HeroComposite heroComposite, EHeroViewMode eHeroViewMode)
    {
        _heroComposite = heroComposite;
        _currentViewMode = eHeroViewMode;

        // Set up the default view (HeroSkillView)
        _heroModeSkillView.Setup(heroComposite, EHeroViewMode.Skill, OnHeroModeViewSelected);
        _heroModeSkinView.Setup(heroComposite, EHeroViewMode.Skin, OnHeroModeViewSelected);
        _heroModeHistoryView.Setup(heroComposite, EHeroViewMode.History, OnHeroModeViewSelected);
        _selectedViewMode = _heroModeSkillView;
        _selectedPageView = _heroDetailView;
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
                case EHeroViewMode.Skill:
                    SetSelectedViewMode(_heroModeSkillView, _heroDetailView);
                    
                    break;
                
                case EHeroViewMode.Skin:
                    SetSelectedViewMode(_heroModeSkinView, _skinPageView);
                    
                    break;
                
                case EHeroViewMode.History:
                    SetSelectedViewMode(_heroModeHistoryView, _historyPageView);
                    break;

                default:
                    // Handle other cases if needed
                    break;
            }
        }
    }

    private void SetSelectedViewMode(ISetupHeroViewMode newHeroViewMode, IHeroModePageView newHeroModePageView)
    {
        // Deselect the previous view
        if (_selectedViewMode != null)
        {
            _selectedViewMode.SetSelectedState(false);
            _selectedPageView.PageSelected(false);
        }

        // Set up and select the new view
        _selectedViewMode = newHeroViewMode;
        _selectedViewMode.Setup(_heroComposite, _currentViewMode, OnHeroModeViewSelected);
        _selectedViewMode.SetSelectedState(true);
        
        _selectedPageView = newHeroModePageView;
        _selectedPageView.PageSelected(true);
        
    }
    
    public void ResetToSkillView(EHeroViewMode resetViewMode)
    {
        // Perform the setup for the skill view
        SetSelectedViewMode(_heroModeSkillView, _heroDetailView);

        // Update the current view mode
        _currentViewMode = resetViewMode;
    }


    private void OnHeroModeViewSelected(EHeroViewMode eHeroViewMode)
    {
        // Action to perform when a HeroView is selected
        UpdateModeView(eHeroViewMode);
    }
}


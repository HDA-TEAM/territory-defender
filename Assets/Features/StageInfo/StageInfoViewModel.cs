using Common.Loading.Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageInfoViewModel : MonoBehaviour
{
    [Header("UI")] 
    [SerializeField] private ItemPlayView _itemPlayView;
    [SerializeField] private ItemMasteryView _itemMasteryView;
    [SerializeField] private List<ItemStageStarView> _itemStageStarViews;
    [SerializeField] private StageInfoDetailView _stageInfoDetailView;
    
    [Header("Data")]
    [SerializeField] private GameModeViewModel _gameModeViewModel;
    [SerializeField] private ListHeroChooseViewModel _heroChooseView;
    // Internal
    private StageComposite _currentStage;
    private UIManagerStateMachine _stateMachine;

    private void Awake()
    {
        _stateMachine = new UIManagerStateMachine();
        _itemPlayView.Setup(OnSelectedItemPlay);
        _itemMasteryView.Setup(OnSelectedItemMastery);
        
        GameEvents.OnCompositeSelected += HandleCompositeSelection;
        UpdateData();
    }
    private void OnDestroy()
    {
        // Unsubscribe from the stage selection event
        GameEvents.OnCompositeSelected -= HandleCompositeSelection;
    }
    private void HandleCompositeSelection(IComposite composite)
    {
        if (composite is StageComposite stage)
        {
            _currentStage = stage;
            UpdateData();
        }
    }
    private void UpdateData()
    {
        UpdateView();
    }

    private void UpdateView()
    {
        // Fill the star result for that stage
        for (int i = 0; i < _itemStageStarViews.Count; i++)
        {
            if (i < _currentStage.StageStar)
                _itemStageStarViews[i].SetupYellowStar();
            
            else
                _itemStageStarViews[i].SetupGrownStar();
        }
        _stageInfoDetailView.Setup(_currentStage);
    }

    private void OnSelectedItemMastery(ItemMasteryView itemMasteryView)
    {
        _stateMachine.ChangeState<MasteryPageState>();
    }

    private void OnSelectedItemPlay(ItemPlayView itemPlayView)
    {
        GameMode currentGameMode = _gameModeViewModel.GetMode();
        HeroComposite heroBeChosen = _heroChooseView.GetHeroChoose();
        
        //TODO: load the map is suitable with the (Stage, Mode, Hero Chosen)
        LoadSceneBasedOnStageAndMode(_currentStage, currentGameMode, heroBeChosen);
    }
    
    private void LoadSceneBasedOnStageAndMode(StageComposite stage, GameMode gameMode, HeroComposite hero)
    {
        Debug.Log($"StageId: {stage.StageId}, GameMode: {gameMode}, Hero : {hero.Name}");
        LoadingSceneController.Instance.LoadingHomeToGame();
    }
}


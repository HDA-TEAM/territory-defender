using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageInfoViewModel : MonoBehaviour
{
    [Header("UI")] 
    [SerializeField] private ItemPlayView _itemPlayView;
    [SerializeField] private ItemMasteryView _itemMasteryView;
    [SerializeField] private List<ItemStageStarView> _itemStageStarViews;
    [SerializeField] private StageInfoDetailView _stageInfoDetailView;
    
    [SerializeField] private GameModeViewModel _gameModeViewModel;
    
    // Internal
    private StageComposite _currentStage;
    private UIManagerStateMachine _stateMachine;
    private void Awake()
    {
        GameEvents.OnCompositeSelected += HandleCompositeSelection;
        
        _stateMachine = new UIManagerStateMachine();
        _itemPlayView.Setup(OnSelectedItemPlay);
        _itemMasteryView.Setup(OnSelectedItemMastery);
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
        Debug.Log($"Stage: {_currentStage.StageId}, Type: {_currentStage.StageType}, Mode: {currentGameMode}");
        
        //TODO: load the map is suitable with the (Stage, Mode)
        LoadSceneBasedOnStageAndMode(_currentStage, currentGameMode);
    }
    
    private void LoadSceneBasedOnStageAndMode(StageComposite stage, GameMode gameMode)
    {
        int sceneIndex = DetermineSceneIndex(stage, gameMode);
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogError($"Scene index out of range: {sceneIndex}. Ensure you have the correct scene setup in Build Settings.");
        }
    }

    // Example logic to determine which scene to load based on the stage and game mode
    private int DetermineSceneIndex(StageComposite stage, GameMode gameMode)
    {
        // Implement your logic here to determine the scene index
        // This is a placeholder return value. Replace it with your actual logic.
        return 2; // Example scene index. Adjust based on your project's scenes setup.
    }
}


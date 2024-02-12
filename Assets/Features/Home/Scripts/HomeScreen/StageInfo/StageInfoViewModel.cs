using UnityEngine;
using UnityEngine.SceneManagement;

public class StageInfoViewModel : MonoBehaviour
{
    [Header("UI")] 
    [SerializeField] private ItemPlayView _itemPlayView;

    //[SerializeField] private ListStageViewModel _listStageViewModel;
    [SerializeField] private GameModeViewModel _gameModeViewModel;
    // Internal
    private StageComposite _currentStage;
    private void Awake()
    {
        GameEvents.OnStageSelected += HandleStageSelection;
        _itemPlayView.Setup(OnSelectedItemPlay);
        UpdateData();
    }
    private void OnDestroy()
    {
        // Unsubscribe from the stage selection event
        GameEvents.OnStageSelected -= HandleStageSelection;
    }
    
    private void HandleStageSelection(StageComposite stage)
    {
        _currentStage = stage; // Store the selected stage
    }

    private void UpdateData()
    {
        UpdateView();
    }

    private void UpdateView()
    {
        
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


using System.Collections.Generic;
using Common.Loading.Scripts;
using Common.Scripts;
using GamePlay.Scripts.Data;
using UnityEngine;

public class StageInfoViewModel : MonoBehaviour
{
    [Header("UI")] [SerializeField] private ItemPlayView _itemPlayView;
    [SerializeField] private ItemMasteryView _itemMasteryView;
    [SerializeField] private List<ItemStageStarView> _itemStageStarViews;
    [SerializeField] private StageInfoDetailView _stageInfoDetailView;

    [Header("Data")] [SerializeField] private GameModeViewModel _gameModeViewModel;

    [SerializeField] private ListHeroChooseViewModel _heroChooseView;

    // Internal
    private static StageComposite _currentStage;

    private void Awake()
    {
        _itemPlayView.Setup(OnSelectedItemPlay);
        _itemMasteryView.Setup(OnSelectedItemMastery);

        UpdateData();
    }

    private void UpdateData()
    {
        _currentStage = StageDataManager.Instance.CurrentStage;
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
        var stateMachine = UIManagerStateMachine.Instance;
        stateMachine.ChangeModalState<MasteryPagePuState>();
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
        LoadingSceneController.Instance.LoadingHomeToGame(new StartStageComposite
        {
            // Demo
            StageId = StageId.Chap1Stage0,
            StageDiff = StageDiff.Normal,
            HeroId = hero.HeroId,
        });
    }
}

using Common.Loading.Scripts;
using GamePlay.Scripts.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Features.StageInfo.Scripts.StageInfoView
{
    public class StageInfoViewModel : MonoBehaviour
    {
        [Header("UI")] [SerializeField] private ItemPlayView _itemPlayView;
        [SerializeField] private ItemMasteryView _itemMasteryView;
        [SerializeField] private List<ItemStageStarView> _itemStageStarViews;
        [SerializeField] private StageInfoDetailView _stageInfoDetailView;

        [Header("Data")] [SerializeField] private StageModeViewModel _stageModeViewModel;
        
        [SerializeField] private ListHeroChooseViewModel _heroChooseView;
        [SerializeField] private StageDataAsset _stageDataAsset;
        [SerializeField] private MapDataConfig _mapDataConfig;
        [SerializeField] private StageDataConfig _stageDataConfig;
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
        private void UpdateStarView()
        {
            StagePassed stagePassed = _stageDataAsset.ListStagePassed.Find((stage) => stage.StageId == _currentStage.StageId);
            // Fill the star result for that stage
            for (int i = 0; i < _itemStageStarViews.Count; i++)
            {
                if (i < stagePassed.TotalStar)
                    _itemStageStarViews[i].SetupYellowStar();

                else
                    _itemStageStarViews[i].SetupGrownStar();
            }
        }
        private void UpdateView()
        {
            UpdateStarView();
            _currentStage.StageImage = _mapDataConfig.GetConfigByKey(_currentStage.StageId).MapSprite;
            _currentStage.StageName = _stageDataConfig.GetConfigByKey(_currentStage.StageId).Name;
            _stageInfoDetailView.Setup(_currentStage);
        }

        private void OnSelectedItemMastery(ItemMasteryView itemMasteryView)
        {
            var stateMachine = UIManagerStateMachine.Instance;
            stateMachine.ChangeModalState<MasteryPagePuState>();
        }

        private void OnSelectedItemPlay(ItemPlayView itemPlayView)
        {
            GameMode currentGameMode = _stageModeViewModel.GetMode();
            HeroComposite heroBeChosen = _heroChooseView.GetHeroChoose();

            //TODO: load the map is suitable with the (Stage, Mode, Hero Chosen)
            LoadSceneBasedOnStageAndMode(_currentStage, currentGameMode, heroBeChosen);
        }

        private void LoadSceneBasedOnStageAndMode(StageComposite stage, GameMode gameMode, HeroComposite hero)
        {
            Debug.Log($"StageId: {stage.StageId}, GameMode: {gameMode}, Hero : {hero.Name}");
            LoadingSceneController.Instance.LoadingHomeToGame(new StartStageComposite
            {
                StageId = stage.StageId,
                StageDiff = StageDiff.Normal,
                HeroId = hero.HeroId,
            });
        }
    }
}

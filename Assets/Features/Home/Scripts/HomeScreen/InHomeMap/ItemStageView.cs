using System;
using GamePlay.Scripts.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Home.Scripts.HomeScreen.InHomeMap
{
    public class ItemStageView : MonoBehaviour
    {
        [SerializeField] private Button _btn;
        [SerializeField] private GameObject _imgFlag;

        // Internal
        private ItemStageView _preStageSelected;
        private Action<ItemStageView> _onSelected;
        public StageComposite StageComposite;

        public void Setup(StageComposite stageComposite, Action<ItemStageView> onAction, ItemStageView preItem)
        {
            StageComposite = stageComposite;
            _onSelected = onAction;
            _preStageSelected = preItem;

            _imgFlag.SetActive(stageComposite is { StageStar: > 0 });

            StageLoad(stageComposite.StageId);
            _btn.onClick.AddListener(OnSelectedStage);
        }

        private void OnSelectedStage()
        {
            var stateMachine = UIManagerStateMachine.Instance;
            _onSelected?.Invoke(this);

            stateMachine.ChangeModalState<StageInfoPuState>();
        }

        private void StageLoad(StageId stageID)
        {
            //TODO: Load Name for each Stage
        }

        public void ExistLightCol() //Auto exist light col for next stage
        {
            HomeMapViewModel.Instance?.MoveLightColTo(this.transform.position);
        }
    }
}




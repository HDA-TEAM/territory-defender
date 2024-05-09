using System;
using Features.Home.Scripts.HomeScreen.InHomeMap;
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
        private Action<ItemStageView> _onSelected;
        public StageComposite StageComposite;

        public void Setup(StageComposite stageComposite, Action<ItemStageView> onAction,
            UIManagerStateMachine stateMachine)
        {
            StageComposite = stageComposite;
            _onSelected = onAction;

            if (stageComposite.StageState)
                _imgFlag.SetActive(true);

            StageLoad(stageComposite.StageId);
            _btn.onClick.AddListener(OnSelectedStage);
        }

        private void OnSelectedStage()
        {
            var stateMachine = UIManagerStateMachine.Instance;
            _onSelected?.Invoke(this);

            HomeMapViewModel.Instance?.MoveLightColTo(this.transform.position);
            stateMachine.ChangeModalState<StageInfoPuState>();
        }

        private void StageLoad(StageId stageID)
        {
            //TODO: Load Name for each Stage
            //Debug.Log("Stage " + stageID + " is setting");
        }
    }
}




using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UIInHomeScreen
{
    public class ItemStageView : MonoBehaviour
    {
        [SerializeField] private Button _btn;

        // Internal
        private Action<ItemStageView> _onSelected;
        private UIManagerStateMachine _stateMachine;
        
        public StageComposite StageComposite;

        public void Setup(StageComposite stageComposite, Action<ItemStageView> onAction, UIManagerStateMachine stateMachine)
        {
            StageComposite = stageComposite;
            _onSelected = onAction;
            _stateMachine = stateMachine; // Assign the state machine instance

            StageLoad(stageComposite.StageType);
            _btn.onClick.RemoveAllListeners();
            _btn.onClick.AddListener(OnSelectedHero);
        }

        private void OnSelectedHero()
        {
            Debug.Log("Stage " + StageComposite.StageType + " is opened");

            _onSelected?.Invoke(this);
            _stateMachine.ChangeState<StageInfoState>();
        }

        private void StageLoad(string name)
        { 
            Debug.Log("Stage " + name + " is setting");
        }
    }
}



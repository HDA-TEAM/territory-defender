using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemStageView : MonoBehaviour
{
    [SerializeField] private Button _btn;

    // Internal
    private Action<ItemStageView> _onSelected;
    private static UIManagerStateMachine _stateMachine;
    
    public StageComposite StageComposite;

    public void Setup(StageComposite stageComposite, Action<ItemStageView> onAction, UIManagerStateMachine stateMachine)
    {
        StageComposite = stageComposite;
        _onSelected = onAction;
        _stateMachine = stateMachine; // Assign the state machine instance

        StageLoad(stageComposite.StageId);
        //_btn.onClick.RemoveAllListeners();
        _btn.onClick.AddListener(OnSelectedHero);
    }

    private void OnSelectedHero()
    {
        _onSelected?.Invoke(this);
        _stateMachine.ChangeState<StageInfoState>();
    }

    private void StageLoad(int stageID)
    { 
        //TODO: Load Name for each Stage
        //Debug.Log("Stage " + stageID + " is setting");
    }
}




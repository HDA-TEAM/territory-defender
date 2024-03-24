using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemStageView : MonoBehaviour
{
    [SerializeField] private Button _btn;
    // Internal
    private Action<ItemStageView> _onSelected;

    public StageComposite StageComposite;

    public void Setup(StageComposite stageComposite, Action<ItemStageView> onAction, UIManagerStateMachine stateMachine)
    {
        StageComposite = stageComposite;
        _onSelected = onAction;

        StageLoad(stageComposite.StageId);
        _btn.onClick.AddListener(OnSelectedHero);
    }

    private void OnSelectedHero()
    {
        var stateMachine = UIManagerStateMachine.Instance;   
        _onSelected?.Invoke(this);
        stateMachine.ChangeModalState<StageInfoPuState>();
    }

    private void StageLoad(int stageID)
    { 
        //TODO: Load Name for each Stage
        //Debug.Log("Stage " + stageID + " is setting");
    }
}




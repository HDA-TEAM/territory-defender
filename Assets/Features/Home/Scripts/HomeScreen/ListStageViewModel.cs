using System.Collections.Generic;
using UnityEngine;

public class ListStageViewModel : MonoBehaviour
{
    [Header("UI")] 
    [SerializeField] private List<ItemStageView> _itemStageViews;

    // Internal
    private List<StageComposite> _stageComposites;
    private ItemStageView _preSelectedStageView;
    //private UIManagerStateMachine _stateMachine;
    private void Awake()
    {
        
        //_stateMachine = new UIManagerStateMachine();
        _stageComposites = new List<StageComposite>();
    }

    private void Start()
    {
        UpdateData();
    }

    private void UpdateData()
    {
        // TODO: Run with data for testing, and it would be updated soon
        var stageDataManager = StageDataManager.Instance;
        if (stageDataManager == null) 
            return;
        if (stageDataManager.StageComposites == null) 
            return;
        
        Debug.Log(stageDataManager.StageComposites[0].StageId);
        // Update data for list StageComposite
        _stageComposites = stageDataManager.StageComposites;
        
        UpdateView();
    }

    private void UpdateView()
    {
        var stateMachine = UIManagerStateMachine.Instance;  
        for (int i = 0; i < _itemStageViews.Count; i++)
        {
            _itemStageViews[i].Setup(_stageComposites[i], OnStageSelected, stateMachine);
        }
    }

    private void OnStageSelected(ItemStageView itemStageView)
    {
        _preSelectedStageView = itemStageView;

        StageDataManager.Instance.CurrentStage = _preSelectedStageView.StageComposite;
        GameEvents.SelectComposite(StageDataManager.Instance.CurrentStage);
    }
}

public struct StageComposite : IComposite
{
    public int StageId;
    public int StageStar;
    public string StageName;
    public Sprite StageImage;
}


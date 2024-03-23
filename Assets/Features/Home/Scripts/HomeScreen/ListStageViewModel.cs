using System.Collections.Generic;
using UnityEngine;

public class ListStageViewModel : MonoBehaviour
{
    [Header("UI")] 
    [SerializeField] private List<ItemStageView> _itemStageViews;

    // Internal
    private List<StageComposite> _stageComposites;
    private ItemStageView _preSelectedStageView;
    private void Awake()
    {
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
    }
}

public interface IComposite
{
    // Common properties or methods for composites
}

public struct StageComposite : IComposite
{
    public int StageId;
    public int StageStar;
    public string StageName;
    public Sprite StageImage;
}


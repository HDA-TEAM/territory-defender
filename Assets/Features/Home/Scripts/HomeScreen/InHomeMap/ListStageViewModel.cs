using Features.Home.Scripts.HomeScreen.InHomeMap;
using System.Collections.Generic;
using GamePlay.Scripts.Data;
using UnityEngine;

public class ListStageViewModel : MonoBehaviour
{
    [Header("UI")] 
    [SerializeField] private List<ItemStageView> _itemStageViews;

    // Internal
    private List<StageComposite> _stageComposites;
    private ItemStageView _preSelectedStageView;
    private StageComposite _nextStage;
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

        foreach (var stage in _stageComposites)
        {
            if (!stage.StageState)
                _nextStage = stage;
        }
        UpdateView();
    }
    private void UpdateView()
    {
        for (int i = 0; i < _itemStageViews.Count; i++)
        {
            _itemStageViews[i].Setup(_stageComposites[i], OnStageSelected, _preSelectedStageView);
        }

        _itemStageViews.Find(stage => stage.StageComposite.StageId == _nextStage.StageId).ExistLightCol();
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
    public StageId StageId;
    public int StageStar;
    public string StageName;
    public bool StageState;
    public Sprite StageImage;
}


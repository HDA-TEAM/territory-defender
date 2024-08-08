using Features.Home.Scripts.HomeScreen.Common;
using Features.Home.Scripts.HomeScreen.InHomeMap;
using System.Collections.Generic;
using System.Linq;
using GamePlay.Scripts.Data;
using UnityEngine;

public class ListStageViewModel : MonoBehaviour
{
    [Header("UI"), Space(12)] 
    [SerializeField] private List<ItemStageView> _itemStageViews;
    [SerializeField] private StageDataAsset _stageDataAsset;
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
        // Update data for list StageComposite
        _stageComposites = new StageDataAdapter().GetStageComposites(stageDataAsset: _stageDataAsset);

        // Determined index of next expended stage 
        _nextStage = _stageComposites.FirstOrDefault(stage => stage.StageStar <= 0);
        
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

        StageDataController.Instance.CurrentStage = _preSelectedStageView.StageComposite;
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
    public Sprite StageImage;
}


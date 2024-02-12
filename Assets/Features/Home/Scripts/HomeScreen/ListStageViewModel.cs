
using System;
using System.Collections.Generic;
using UI.UIInHomeScreen;
using UnityEngine;
using UnityEngine.UI;

public class ListStageViewModel : MonoBehaviour
{
    [Header("UI")] 
    [SerializeField] private List<ItemStageView> _itemStageViews;

    private List<StageComposite> _stageComposites;
    private ItemStageView _preSelectedStageView;
    private static StageComposite _preStageComposite;
    
    private UIManagerStateMachine _stateMachine;
    private void Awake()
    {
        _stateMachine = new UIManagerStateMachine();
        _stageComposites = new List<StageComposite>();
        UpdateData();
    }

    private void UpdateData()
    {
        _stageComposites.Add(
            new StageComposite
            {
                StageId = 1,
                StageStar = 2,
                StageType = "normal",
                StageName = "VUNG DAT DO"
            }
        );
        
        _stageComposites.Add(
            new StageComposite
            {
                StageId = 2,
                StageStar = 0,
                StageType = "boss",
                StageName = "VUNG DAT DO BOSS"
            }
        );
        UpdateView();
    }

    private void UpdateView()
    {
        for (int i = 0; i < _itemStageViews.Count; i++)
        {
            _itemStageViews[i].Setup(_stageComposites[i], OnStageSelected, _stateMachine);
        }
    }

    private void OnStageSelected(ItemStageView itemStageView)
    {
        _preStageComposite = itemStageView.StageComposite;
        Debug.Log("Stage: " + _preStageComposite.StageId 
                            + "StageType: " + _preStageComposite.StageType);

        _preSelectedStageView = itemStageView;
        
        GameEvents.SelectStage(_preStageComposite);
    }

    public StageComposite GetStage() => _preStageComposite;
}

public struct StageComposite
{
    public int StageId;
    public int StageStar;
    public string StageType;
    public string StageName;
    public Sprite StageImage;
}


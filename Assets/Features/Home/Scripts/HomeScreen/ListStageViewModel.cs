
using System;
using System.Collections.Generic;
using UI.UIInHomeScreen;
using UnityEngine;

public class ListStageViewModel : MonoBehaviour
{
    [Header("UI")] 
    [SerializeField] private List<ItemStageView> _itemStageViews;

    private List<StageComposite> _stageComposites;
    private ItemStageView _preSelectedStageView;
    private void Awake()
    {
        _stageComposites = new List<StageComposite>();
        UpdateData();
    }

    private void UpdateData()
    {
        _stageComposites.Add(
            new StageComposite
            {
                StageId = 1,
                Mode = "normal"
            }
        );
        
        _stageComposites.Add(
            new StageComposite
            {
                StageId = 2,
                Mode = "boss"
            }
        );
        UpdateView();
    }

    private void UpdateView()
    {
        for (int i = 0; i < _itemStageViews.Count; i++)
        {
            _itemStageViews[i].Setup(_stageComposites[i], OnStageSelected);
        }
    }

    private void OnStageSelected(ItemStageView itemStageView)
    {
        Debug.Log("Stage " + itemStageView.StageComposite.Mode + " is opened");

        _preSelectedStageView = itemStageView;
    }
}

public struct StageComposite
{
    public int StageId;
    public string Mode;
}


using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ListTowerViewModel : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private List<ItemTowerView> _itemTowerViews;
    [SerializeField] private ListRuneViewModel _listRuneViewModel;
    
    [FormerlySerializedAs("_commonTowerDataAsset")]
    [Header("Data"), Space(12)]
    [SerializeField] private CommonTowerConfig _commonTowerConfig;
    
    public Action<TowerId> _onUpdateViewAction;
    
    private List<TowerComposite> _towerComposites;
    private TowerComposite _towerComposite;
    
    private ItemTowerView _preSelectedItem;
    private void Start()
    {
        _towerComposites = new List<TowerComposite>();
        UpdateData();
        OnSelectedItem(_itemTowerViews[0]);
    }

    private void OnDisable()
    {
        ResetView();
    }

    private void UpdateData()
    {
        List<CommonTowerSO> listTowerData = _commonTowerConfig.GetAllTowerData();
        foreach (var towerDataSo in listTowerData)
        {
            _towerComposites.Add(
                new TowerComposite
                {
                    TowerId = towerDataSo.GetTowerId(),
                    RuneLevels = towerDataSo.GetAllRuneDatLevels(),
                }
            );
        }
        UpdateView();
    }

    private void UpdateView()
    {
        for (int i = 0; i < _itemTowerViews.Count; i++)
        {
            if (i < _towerComposites.Count)
            {
                // Setup hero property
                _itemTowerViews[i].Setup(_towerComposites[i],OnSelectedItem);  
                _itemTowerViews[i].gameObject.SetActive(true);
            } else {
                _itemTowerViews[i].gameObject.SetActive(false); 
            }
        }
    }

    private void OnSelectedItem(ItemTowerView itemTowerView)
    {
        //Prevent multiple clicks
        if (_preSelectedItem == itemTowerView) return;
        
        if (_preSelectedItem != null)
        {
            _preSelectedItem.RemoveSelected();
        }

        _preSelectedItem = itemTowerView;
        
        Debug.Log($"Invoking actions for tower ID: {itemTowerView.TowerComposite.TowerId}");
        
        // Reset view of rune detail
        GlobalUtility.ResetRuneDetailView(_listRuneViewModel);
        _onUpdateViewAction?.Invoke(_preSelectedItem.TowerComposite.TowerId);
    }
    
    public void ResetView()
    {
        // Reset the selection state
        if (_preSelectedItem != null)
        {
            _preSelectedItem.RemoveSelected();
            _preSelectedItem = null;
        }

        // Reset the view to its initial state, such as selecting the first tower again
        if (_itemTowerViews != null)
        {
            _itemTowerViews[0].OnSelectedTower();
            _onUpdateViewAction?.Invoke(_towerComposites[0].TowerId);
        }
    }
}

public struct TowerComposite
{
    public TowerId TowerId;
    public List<RuneLevel> RuneLevels;
}

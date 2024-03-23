using System;
using System.Collections.Generic;
using Features.Home.Scripts.HomeScreen.Common;
using Features.MasteryPage.Scripts.Rune;
using UnityEngine;

public class ListRuneViewModel : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private List<ItemRuneView> _itemRuneViews;
    [SerializeField] private RuneDetailView _runeDetailView;
    [SerializeField] private ItemUpgradeRuneView _itemUpgradeRuneView;
    [SerializeField] private ItemStarView _itemStarView;
    [SerializeField] private ListTowerViewModel _listTowerViewModel;
    
    [Header("Data"), Space(12)]
    [SerializeField] private InGameInventoryRuntimeData _inventoryRuntimeData;
    //[SerializeField] private CommonTowerConfig _commonTowerConfig;
    //[SerializeField] private RuneDataAsset _runeDataAsset;

    // Internal
    private List<RuneLevel> _runeLevels;
    //private List<TowerComposite> _towerComposites;
    private List<TowerRuneComposite> _towerRuneComposites;
    
    private InventoryComposite _inventoryComposite;
    private TowerRuneComposite _preTowerComposite;
    private List<RuneSO> _runeSos;
    private RuneSO _preRuneSo;
    private ItemRuneView _preSelectedItem;
    private ItemUpgradeRuneView _preSelectedUpgradeRuneView;
    
    //Action
    private Action _onTowerDataUpdatedAction;
    
    private void Start()
    {
        _inventoryComposite = new InventoryComposite();
        UpdateData();
        
        if (_listTowerViewModel != null)
            _listTowerViewModel._onUpdateViewAction += UpdateView;
        
        if (_itemStarView != null)
            _itemStarView._onDataUpdated += UpdateData;

        if (_itemUpgradeRuneView != null)
            _onTowerDataUpdatedAction += UpdateData;
    }
    
    private void UpdateData()
    {
        //_towerRuneComposites.Clear();
        //_runeComposites.Clear();
        var towerRuneDataManager = RuneDataManager.Instance;
        
        if (towerRuneDataManager == null) return;
        if (towerRuneDataManager.TowerRuneComposites == null) return;

        _towerRuneComposites = towerRuneDataManager.TowerRuneComposites;
        Debug.Log(_towerRuneComposites[0].RuneComposite[1].Level + " ??????????????????????");
        
        // Load Star data
        _inventoryComposite.Currency = _inventoryRuntimeData.GetCurrencyValue();
        _inventoryComposite.Life = _inventoryRuntimeData.GetLifeValue();
        _inventoryComposite.StarNumber = _inventoryRuntimeData.GetStarValue();

        // Default setting
        if (_preTowerComposite.RuneComposite == null)
            _preTowerComposite = _towerRuneComposites[0];
        
        UpdateView(_preTowerComposite.TowerId);
    }
    private void UpdateView(TowerId towerId)
    {
        var result = FindByTowerId(_towerRuneComposites, towerId);
        if (result.Equals(default(TowerRuneComposite)))
        {
            Debug.LogError("UpdateView: result is default, no update performed");
            return;
        }
        
        _preTowerComposite = result;
        
        for (int runeIndex = 0; runeIndex < _itemRuneViews.Count; runeIndex++)
        {
            // Setup rune view
            _itemRuneViews[runeIndex].SetRuneLevel(result.RuneComposite[runeIndex]);
        
            // Setup star view
            _itemStarView.Setup(_inventoryComposite);
        
            // Rune avatar logic
            _itemRuneViews[runeIndex].SetAvatarRune(result.RuneComposite[runeIndex].Level > 0 ? result.RuneComposite[runeIndex].AvatarSelected : result.RuneComposite[runeIndex].AvatarStarted);
        
            // Selected rune setup
            _itemRuneViews[runeIndex].Setup(result.RuneComposite[runeIndex], OnSelectedRuneItem);
        }

        // Update rune data when updated the level of that rune
        if (_runeDetailView != null && _preSelectedItem != null && _itemUpgradeRuneView != null)
        {
            foreach (var runeComposite in _preTowerComposite.RuneComposite)
            {
                if (runeComposite.RuneId == _preSelectedItem.RuneComposite.RuneId)
                {
                    _runeDetailView.UpdateCurrentRuneData(runeComposite);
                }
            }
        }
    }

    private void OnSelectedRuneItem(ItemRuneView itemRuneView)
    {
        SetupRuneDetailView(true);
        if (_preSelectedItem == null)
        {
            _itemUpgradeRuneView.gameObject.SetActive(true);
            _runeDetailView.gameObject.SetActive(true);
        }
        _preSelectedItem = itemRuneView;
        
        // Setup rune view
        _runeDetailView.Setup(_preSelectedItem.RuneComposite);
        _itemUpgradeRuneView.Setup(_preSelectedItem.RuneComposite, OnSelectedUpgradeRuneItem);
    }
    
    private void OnSelectedUpgradeRuneItem(ItemUpgradeRuneView itemUpgradeRuneView)
    {
        var runeDataAsset = RuneDataManager.Instance.RuneDataAsset;
        if (itemUpgradeRuneView == null || _preSelectedItem == null || runeDataAsset == null || _itemStarView == null)
        {
            Debug.LogError("One or more required objects are null.");
            return;
        }
        _preSelectedUpgradeRuneView = itemUpgradeRuneView;
       
        //Conditions to upgrade any skill
        if (_preSelectedItem.RuneComposite.Level <= _preSelectedItem.RuneComposite.MaxLevel  && _inventoryRuntimeData.GetStarValue() > 0)
        {
            _preRuneSo = runeDataAsset.GetRune(_preSelectedUpgradeRuneView.RuneComposite.RuneId);
            if (_preRuneSo != null)
            {
                var commonTowerConfig = RuneDataManager.Instance.CommonTowerConfig;
                commonTowerConfig.UpdateTowerData(_preTowerComposite.TowerId, _preSelectedUpgradeRuneView.RuneComposite);
            
                // Subtract star number
                _inventoryRuntimeData.TryChangeStar(1);
        
                
                Debug.Log("Upgrade rune successful....");
                
                _onTowerDataUpdatedAction?.Invoke();
            }
        } else {
            CommonLog.LogError("Upgrade rune fail");
        }
    }
    
    private TowerRuneComposite FindByTowerId(List<TowerRuneComposite> list, TowerId towerId)
    {
        foreach (var item in list)
        {
            if (item.TowerId == towerId)
            {
                Debug.Log($"FindByTowerId: Found match for TowerId {towerId}");
                return item;
            }
        }
        Debug.Log($"FindByTowerId: No match found for TowerId {towerId}, returning default");
        return default(TowerRuneComposite); 
    }

    public void SetupRuneDetailView(bool flag)
    {
        _runeDetailView.gameObject.SetActive(flag);
        _itemUpgradeRuneView.gameObject.SetActive(flag);
    }
}
public struct RuneComposite
{
    public RuneId RuneId;
    public string Name;
    public int Level;
    public int MaxLevel;
    public Sprite AvatarSelected;
    public Sprite AvatarStarted;
    public List<EffectId> Effects;
}

public struct InventoryComposite
{
    public float Currency;
    public float Life;
    public float StarNumber;
}

public struct TowerRuneComposite
{
    public TowerId TowerId;
    public List<RuneComposite> RuneComposite;
}


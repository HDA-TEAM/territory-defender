using Common.Scripts;
using GamePlay.Scripts.Data;
using System;
using System.Collections.Generic;
using Features.Home.Scripts.HomeScreen.Common;
using Features.MasteryPage.Scripts.Rune;
using Features.MasteryPage.Scripts.RuneDetailView;
using UnityEngine;

public class ListRuneViewModel : MonoBehaviour
{
    [Header("UI")]
    // List item
    [SerializeField] private ListTowerViewModel _listTowerViewModel;
    [SerializeField] private List<ItemRuneView> _itemRuneViews;
    
    // Single Item
    [SerializeField] private ItemUpgradeRuneView _itemUpgradeRuneView;
    [SerializeField] private ItemResetRuneView _itemResetRuneView;
    [SerializeField] private ItemStarView _itemStarView;
    [SerializeField] private RuneDetailView _runeDetailView;
    
    [Header("Data"), Space(12)]
    [SerializeField] private InGameInventoryRuntimeData _inventoryRuntimeData;

    // INTERNAL
    private List<RuneLevel> _runeLevels;
    
    // Composite
    private List<TowerHasRuneComposite> _towerRuneComposites;
    private InventoryComposite _inventoryComposite;
    private TowerHasRuneComposite _preTowerHasComposite;
    
    // Config
    private List<RuneDataConfig> _runeSos;
    private RuneDataConfig _preRuneDataConfig;
    
    // Item object
    private ItemRuneView _preSelectedRuneItem;
    private ItemUpgradeRuneView _preSelectedUpgradeRuneItem;
    private ItemResetRuneView _preSelectedResetRuneItem;
    
    //Action
    private Action _onTowerDataUpdatedAction;
    private Action _onTowerRuneResetAction;

    private void Start()
    {
        _inventoryComposite = new InventoryComposite();
        UpdateData();
        
        SetupRuneDetailView(true);
        
        // Handle Tower changed
        if (_listTowerViewModel != null)
        {
            _listTowerViewModel._onUpdateViewAction += UpdateView;
        }
        
        // Handle upgrade rune clicking
        if (_itemUpgradeRuneView != null)
        {
            _onTowerDataUpdatedAction += RuneDataManager.Instance.GetTowerRuneData;
            //RuneDataManager.Instance.GetTowerRuneData();
            
            _onTowerDataUpdatedAction += UpdateData;
        }
        
        // Handle reset rune clicking
        if (_itemResetRuneView != null)
        {
            _onTowerRuneResetAction += RuneDataManager.Instance.GetTowerRuneData;
            //RuneDataManager.Instance.GetTowerRuneData();
            
            _onTowerRuneResetAction += UpdateData;
        }
    }

    private void SetDefaultState()
    {
        _preSelectedResetRuneItem = null;
        _preSelectedUpgradeRuneItem = null;
        
        OnSelectedRuneItem(_itemRuneViews[0]);
        
        _itemUpgradeRuneView.Setup(_preSelectedRuneItem.RuneComposite, OnSelectedUpgradeRuneItem);
        _itemResetRuneView.Setup(_preSelectedRuneItem.RuneComposite, OnSelectedResetRuneItem);
    }

    private void UpdateData()
    {
        var towerRuneDataManager = RuneDataManager.Instance;
        
        if (towerRuneDataManager == null) return;
        if (towerRuneDataManager.TowerRuneComposites == null) return;

        _towerRuneComposites = towerRuneDataManager.TowerRuneComposites;

        // Load Star data
        _inventoryComposite.Currency = _inventoryRuntimeData.GetCurrencyValue();
        _inventoryComposite.Life = _inventoryRuntimeData.GetLifeValue();
        _inventoryComposite.StarNumber = _inventoryRuntimeData.GetStarValue();

        // Default setting
        if (_preTowerHasComposite.RuneComposite == null)
            _preTowerHasComposite = _towerRuneComposites[0];
        
        UpdateView(_preTowerHasComposite.TowerId);
    }
    private void UpdateView(UnitId.Tower towerId)
    {
        var result = FindByTowerId(_towerRuneComposites, towerId);
        if (result.Equals(default(TowerHasRuneComposite)))
        {
            Debug.LogError("UpdateView: result is default, no update performed");
            return;
        }
        
        _preTowerHasComposite = result;
        
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
        if (_runeDetailView != null && _preSelectedRuneItem != null && _preSelectedUpgradeRuneItem != null)
        {
            foreach (var runeComposite in _preTowerHasComposite.RuneComposite)
            {
                if (runeComposite.RuneId == _preSelectedRuneItem.RuneComposite.RuneId)
                {
                    _runeDetailView.UpdateCurrentRuneData(runeComposite);
                }
            }
        } else {
            // Handle to set rune status to default when clicking other tower
            SetDefaultState();
        }
    }

    private void OnSelectedRuneItem(ItemRuneView itemRuneView)
    {
        if (_preSelectedRuneItem == null)
        {
            // Do something
        }
        _preSelectedRuneItem = itemRuneView;
        
        // Setup rune view
        _runeDetailView.Setup(_preSelectedRuneItem.RuneComposite);
        _itemUpgradeRuneView.Setup(_preSelectedRuneItem.RuneComposite, OnSelectedUpgradeRuneItem);
        _itemResetRuneView.Setup(_preSelectedRuneItem.RuneComposite, OnSelectedResetRuneItem);
    }

    private void OnSelectedUpgradeRuneItem(ItemUpgradeRuneView itemUpgradeRuneView)
    {
        var runeDataAsset = RuneDataManager.Instance.RuneDataAsset;
        
        if (itemUpgradeRuneView == null || _preSelectedRuneItem == null || runeDataAsset == null || _itemStarView == null)
        {
            Debug.LogError("One or more required objects are null.");
            return;
        }
        _preSelectedUpgradeRuneItem = itemUpgradeRuneView;

        //Conditions to upgrade any skill
        if (_preSelectedRuneItem.RuneComposite.Level < _preSelectedRuneItem.RuneComposite.MaxLevel  && _inventoryRuntimeData.GetStarValue() > 0)
        {
            _preRuneDataConfig = runeDataAsset.GetRune(_preSelectedUpgradeRuneItem.RuneComposite.RuneId);
            if (_preRuneDataConfig != null)
            {
                var towerRuneDataConfig = RuneDataManager.Instance.TowerRuneDataConfig;
                towerRuneDataConfig.UpdateTowerData(_preTowerHasComposite.TowerId, _preSelectedUpgradeRuneItem.RuneComposite);
            
                // Subtract star number
                _inventoryRuntimeData.TryChangeStar(1);
                
                Debug.Log("Upgrade rune successful....");
                
                _onTowerDataUpdatedAction?.Invoke();
            }
        } else {
            
            Debug.Log("Upgrade rune fail");
        }
    }

    private void OnSelectedResetRuneItem(ItemResetRuneView itemResetRuneView)
    {
        var runeDataAsset = RuneDataManager.Instance.RuneDataAsset;
        if (itemResetRuneView == null || _preSelectedRuneItem == null)
        {
            Debug.LogError("One or more required objects are null.");
            return;
        }

        _preSelectedResetRuneItem = itemResetRuneView;
        if (_preSelectedRuneItem.RuneComposite.Level > 0)
        {
            _preRuneDataConfig = runeDataAsset.GetRune(_preSelectedResetRuneItem.RuneComposite.RuneId);
            if (_preRuneDataConfig != null)
            {
                var towerRuneDataConfig = RuneDataManager.Instance.TowerRuneDataConfig;
                towerRuneDataConfig.ResetRuneLevel(_preTowerHasComposite.TowerId, _preSelectedResetRuneItem.RuneComposite);
            
                // Return star number after reset
                _inventoryRuntimeData.TryRefundStar(towerRuneDataConfig._returnStar);
                
                Debug.Log("Upgrade rune successful....");
                
                _onTowerRuneResetAction?.Invoke();
            }
        }
    }
    private TowerHasRuneComposite FindByTowerId(List<TowerHasRuneComposite> list, UnitId.Tower towerId)
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
        return default(TowerHasRuneComposite); 
    }
    public void SetupRuneDetailView(bool flag)
    {
        _runeDetailView.gameObject.SetActive(flag);
        _itemUpgradeRuneView.gameObject.SetActive(flag);
        _itemResetRuneView.gameObject.SetActive(flag);
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

public struct TowerHasRuneComposite
{
    public UnitId.Tower TowerId;
    public List<RuneComposite> RuneComposite;
}


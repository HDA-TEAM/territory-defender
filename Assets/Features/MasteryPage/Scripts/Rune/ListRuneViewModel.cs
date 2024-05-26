using Common.Scripts;
using Common.Scripts.Data.DataAsset;
using Common.Scripts.Data.DataConfig;
using GamePlay.Scripts.Data;
using System;
using System.Collections.Generic;
using Features.Home.Scripts.HomeScreen.Common;
using Features.MasteryPage.Scripts.Rune;
using Features.MasteryPage.Scripts.RuneDetailView;
using UnityEngine;
using UnityEngine.Serialization;

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
    [SerializeField] private InventoryDataAsset _inventoryDataAsset;

    // INTERNAL
    private List<RuneLevel> _runeLevels;
    
    // Data Asset
    private InventoryData _inventoryData;
    
    // Composite
    private List<TowerHasRuneComposite> _towerRuneComposites;
    private TowerHasRuneComposite _preTowerHasComposite;
    private InventoryComposite _starInventory;
    
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

    private void SubscribeEvents()
    {
        // Handle Tower changed
        if (_listTowerViewModel != null)
        {
            _listTowerViewModel._onUpdateViewAction += UpdateView;
        }
        
        // Handle upgrade rune clicking
        if (_itemUpgradeRuneView != null)
        {
            _onTowerDataUpdatedAction += RuneDataManager.Instance.GetTowerRuneData;
            _onTowerDataUpdatedAction += UpdateData;
        }
        
        // Handle reset rune clicking
        if (_itemResetRuneView != null)
        {
            _onTowerRuneResetAction += RuneDataManager.Instance.GetTowerRuneData;
            _onTowerRuneResetAction += UpdateData;
        }
    }

    private void UnsubscribeEvents()
    {
        if (_listTowerViewModel != null)
        {
            _listTowerViewModel._onUpdateViewAction -= UpdateView;
        }
        if (_itemUpgradeRuneView != null)
        {
            _onTowerDataUpdatedAction -= RuneDataManager.Instance.GetTowerRuneData;
            _onTowerDataUpdatedAction -= UpdateData;
        }
        if (_itemResetRuneView != null)
        {
            _onTowerRuneResetAction -= RuneDataManager.Instance.GetTowerRuneData;
            _onTowerRuneResetAction -= UpdateData;
        }
    }
    private void Start()
    {
        _starInventory = new InventoryComposite();
        UpdateData();
        SetupRuneDetailView(true);
    
        UnsubscribeEvents(); // Ensure there are no duplicates
        SubscribeEvents();
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
        
        // Retrieve the inventory data for 'Star' type
        _inventoryData = _inventoryDataAsset.GetInventoryDataByType(InventoryType.TotalStar);

        // Update the _starInventory
        _starInventory = new InventoryComposite
        {
            Type = _inventoryData.InventoryType,
            Amount = _inventoryData.Amount,
        };
        
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
            _itemStarView.Setup(_starInventory);
        
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
                    _itemUpgradeRuneView.Setup(runeComposite, OnSelectedUpgradeRuneItem);
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
        if (_preSelectedRuneItem.RuneComposite.Level < _preSelectedRuneItem.RuneComposite.MaxLevel
            && _starInventory.Amount > 0)
        {
            _preRuneDataConfig = runeDataAsset.GetRune(_preSelectedUpgradeRuneItem.RuneComposite.RuneId);
            if (_preRuneDataConfig != null)
            {
                var towerRuneDataConfig = RuneDataManager.Instance.TowerRuneDataConfig;
                towerRuneDataConfig.UpdateTowerData(_preTowerHasComposite.TowerId, _preSelectedUpgradeRuneItem.RuneComposite);
            
                // Get data from inventory data & Subtract star number
                _inventoryDataAsset.TryChangeInventoryData(_starInventory.Type, -1);
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
                
                // Get data from inventory data & Return star number after reset
                _inventoryDataAsset.TryChangeInventoryData(_starInventory.Type, towerRuneDataConfig._returnStar);

                Debug.Log("Upgrade rune successful".ToUpper());
                
                _onTowerRuneResetAction?.Invoke();
            }
        }
        else Debug.Log("CANNOT RESET THIS RUNE");
    }
    private TowerHasRuneComposite FindByTowerId(List<TowerHasRuneComposite> list, UnitId.Tower towerId)
    {
        TowerHasRuneComposite foundItem = list.Find(item => item.TowerId == towerId);
        if (foundItem.RuneComposite != null)
        {
            Debug.Log($"FindByTowerId: Found match for TowerId {towerId}");
            return foundItem;
        }
        else
        {
            Debug.Log($"FindByTowerId: No match found for TowerId {towerId}, returning default");
            return default(TowerHasRuneComposite);
        }
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
    public InventoryType Type;
    public int Amount;
}

public struct TowerHasRuneComposite
{
    public UnitId.Tower TowerId;
    public List<RuneComposite> RuneComposite;
}


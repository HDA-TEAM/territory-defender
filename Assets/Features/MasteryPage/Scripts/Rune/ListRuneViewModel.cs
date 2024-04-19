using Common.Scripts;
using GamePlay.Scripts.Data;
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

    // Internal
    private List<RuneLevel> _runeLevels;
    private List<TowerHasRuneComposite> _towerRuneComposites;
    
    private InventoryComposite _inventoryComposite;
    private TowerHasRuneComposite _preTowerHasComposite;
    private List<RuneDataConfig> _runeSos;
    private RuneDataConfig _preRuneDataConfig;
    private ItemRuneView _preSelectedItem;
    private ItemUpgradeRuneView _preSelectedUpgradeRuneView;
    
    //Action
    private Action _onTowerDataUpdatedAction;
    
    private void Start()
    {
        _inventoryComposite = new InventoryComposite();
        UpdateData();
        
        SetDefaultState();

        if (_listTowerViewModel != null)
        {
            _listTowerViewModel._onUpdateViewAction += UpdateView;
        }
        
        if (_itemUpgradeRuneView != null)
        {
            _onTowerDataUpdatedAction += RuneDataManager.Instance.GetTowerRuneData;
            RuneDataManager.Instance.GetTowerRuneData();
            
            _onTowerDataUpdatedAction += UpdateData;
        }
    }

    private void SetDefaultState()
    {
        SetupRuneDetailView(true);
        
        _runeDetailView.UpdateCurrentRuneData(_itemRuneViews[0].RuneComposite);
        OnSelectedRuneItem(_itemRuneViews[0]);
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
        if (_runeDetailView != null && _preSelectedItem != null && _itemUpgradeRuneView != null)
        {
            foreach (var runeComposite in _preTowerHasComposite.RuneComposite)
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
        if (_preSelectedItem == null)
        {
            // Do something
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
        if (_preSelectedItem.RuneComposite.Level < _preSelectedItem.RuneComposite.MaxLevel  && _inventoryRuntimeData.GetStarValue() > 0)
        {
            _preRuneDataConfig = runeDataAsset.GetRune(_preSelectedUpgradeRuneView.RuneComposite.RuneId);
            if (_preRuneDataConfig != null)
            {
                var commonTowerConfig = RuneDataManager.Instance.TowerRuneDataConfig;
                commonTowerConfig.UpdateTowerData(_preTowerHasComposite.TowerId, _preSelectedUpgradeRuneView.RuneComposite);
            
                // Subtract star number
                _inventoryRuntimeData.TryChangeStar(1);
                
                Debug.Log("Upgrade rune successful....");
                
                _onTowerDataUpdatedAction?.Invoke();
            }
        } else {
            
            Debug.Log("Upgrade rune fail");
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


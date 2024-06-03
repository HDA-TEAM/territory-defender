using Common.Scripts;
using Common.Scripts.Data.DataAsset;
using Common.Scripts.Data.DataConfig;
using Features.Home.Scripts.HomeScreen.Common;
using Features.MasteryPage.Scripts.RuneDetailView;
using Features.MasteryPage.Scripts.Tower;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Features.MasteryPage.Scripts.Rune
{
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
        [SerializeField] private global::RuneDetailView _runeDetailView;
    
        [Header("Data"), Space(12)]
        [SerializeField] private InventoryDataAsset _inventoryDataAsset;
        public TowerRuneDataController _towerRuneDataController;

        // INTERNAL
        private List<RuneLevelData> _runeLevels;
        private int _totalTalentPoint;
    
        // Data Asset
        private InventoryData _inventoryData;
    
        // Composite
        private List<TowerRuneComposite> _towerRuneComposites;
        private TowerRuneComposite _preTowerComposite;
        private InventoryComposite _talentPointInventory;
    
        // Config
        private List<RuneDataSo> _runeSos;
        private RuneDataSo _preRuneDataSo;
    
        // Item object
        private ItemRuneView _preSelectedRuneItem;
        private ItemUpgradeRuneView _preSelectedUpgradeRuneItem;
        private ItemResetRuneView _preSelectedResetRuneItem;
    
    
        //Action
        private Action _onTowerDataUpdatedAction;
        private Action _onTowerRuneResetAction;
    
        private ITowerRune _currentTowerRune;
        private void SubscribeEvents()
        {
            // Handle Tower changed
            if (_listTowerViewModel != null)
            {
                _listTowerViewModel._onUpdateViewAction += UpdateView;
            }
        
            //TODO
            // Handle upgrade rune clicking
            if (_itemUpgradeRuneView != null)
            {
                _onTowerDataUpdatedAction += UpdateData;
            }
        
            //Handle reset rune clicking
            if (_itemResetRuneView != null)
            {
                _onTowerRuneResetAction += UpdateData;
            }
        }
        private void UnsubscribeEvents()
        {
            if (_listTowerViewModel != null)
            {
                _listTowerViewModel._onUpdateViewAction -= UpdateView;
            }
        
            //TODO
            if (_itemUpgradeRuneView != null)
            {
                _onTowerDataUpdatedAction -= UpdateData;
            }
            if (_itemResetRuneView != null)
            {
                _onTowerRuneResetAction -= UpdateData;
            }
        }
        private void Start()
        {
            if (_towerRuneDataController == null)
            {
                Debug.LogError("TowerRuneDataController is not set");
                return;
            }

            // Update talent point amount before get that data
            _talentPointInventory = new InventoryComposite();
        
            UpdateData();
            
            _listTowerViewModel.SetupTower();
            SetupRuneDetailView(true);
    
            UnsubscribeEvents(); // Ensure there are no duplicates
            SubscribeEvents();
        }
        private void SetDefault()
        {
            _preSelectedResetRuneItem = null;
            _preSelectedUpgradeRuneItem = null;
        
            OnSelectedRuneItem(_itemRuneViews[0]);
        
            _itemUpgradeRuneView.Setup(_preSelectedRuneItem.RuneComposite, OnSelectedUpgradeRuneItem);
            _itemResetRuneView.Setup(_preSelectedRuneItem.RuneComposite, OnSelectedResetRuneItem);
        }

        private void UpdateData()
        {
            // Set strategy based on game state or other conditions
            _towerRuneDataController.SetStrategy(new InitTowerRuneStrategy());
            _towerRuneDataController.ExecuteStrategy();
        
            var towerRuneComposites = _towerRuneDataController.TowerRuneComposites;

            if (towerRuneComposites == null)
            {
                Debug.LogError("towerRuneComposites null");
                return;
            }

            //TODO
            _towerRuneComposites = towerRuneComposites;

            // Retrieve the inventory data for 'Talent Point' type
            _inventoryData = _inventoryDataAsset.GetInventoryDataByType(InventoryType.TalentPoint);

            // Update the _starInventory
            _talentPointInventory = new InventoryComposite
            {
                Type =  _inventoryData.InventoryType,
                Amount = _inventoryData.Amount,
            };
        
            // Default setting
            if (_preTowerComposite.RuneComposite == null)
                _preTowerComposite = _towerRuneComposites[0];
        
            UpdateView(_preTowerComposite.TowerId);
        }
        private void UpdateView(UnitId.Tower towerId)
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
        
                // Setup Talent Point view
                _itemStarView.Setup(_talentPointInventory);
        
                // Rune avatar logic
                _itemRuneViews[runeIndex].SetAvatarRune(result.RuneComposite[runeIndex].Level > 0 ? result.RuneComposite[runeIndex].AvatarSelected : result.RuneComposite[runeIndex].AvatarStarted);
        
                // Selected rune setup
                _itemRuneViews[runeIndex].Setup(result.RuneComposite[runeIndex], OnSelectedRuneItem);
            }

            // Update rune data when updated the level of that rune
            if (_runeDetailView != null && _preSelectedRuneItem != null && _preSelectedUpgradeRuneItem != null)
            {
                foreach (var runeComposite in _preTowerComposite.RuneComposite)
                {
                    if (runeComposite.RuneId == _preSelectedRuneItem.RuneComposite.RuneId)
                    {
                        _itemUpgradeRuneView.Setup(runeComposite, OnSelectedUpgradeRuneItem);
                        _runeDetailView.UpdateCurrentRuneData(runeComposite);
                    }
                }
            } else {
                // Handle to set rune status to default when clicking other tower
                SetDefault();
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
            var runeDataAsset = _towerRuneDataController._runeDataAsset;
        
            //TODO
            if (itemUpgradeRuneView == null || _preSelectedRuneItem == null || runeDataAsset == null || _itemStarView == null)
            {
                Debug.LogError("One or more required objects are null.");
                return;
            }
            _preSelectedUpgradeRuneItem = itemUpgradeRuneView;
        
            //Conditions to upgrade any skill
            if (_preSelectedRuneItem.RuneComposite.Level < _preSelectedRuneItem.RuneComposite.MaxLevel
                && _talentPointInventory.Amount > 0)
            {
                _preRuneDataSo = runeDataAsset.GetRune(_preSelectedUpgradeRuneItem.RuneComposite.RuneId);
                if (_preRuneDataSo != null)
                {
                    // To update rune data
                    _towerRuneDataController.SetStrategy(new UpdateTowerRuneStrategy(_preTowerComposite.TowerId, _preSelectedUpgradeRuneItem.RuneComposite));
                    _towerRuneDataController.ExecuteStrategy();

                    // Get data from inventory data & Subtract Talent Point number
                    _inventoryDataAsset.TryChangeInventoryData(_talentPointInventory.Type, -1);
                    Debug.Log("Upgrade rune successful....");
                 
                    _onTowerDataUpdatedAction?.Invoke();
                }
            } else {
             
                Debug.Log("Upgrade rune fail");
            }
        }

        private void OnSelectedResetRuneItem(ItemResetRuneView itemResetRuneView)
        {
            //TODO
            if (itemResetRuneView == null || _preSelectedRuneItem == null)
            {
                Debug.LogError("One or more required objects are null.");
                return;
            }
        
            _preSelectedResetRuneItem = itemResetRuneView;
            if (_preSelectedRuneItem.RuneComposite.Level > 0)
            {
                _preRuneDataSo =_towerRuneDataController. _runeDataAsset.GetRune(_preSelectedResetRuneItem.RuneComposite.RuneId);
                if (_preRuneDataSo != null)
                {
                    // To reset rune data
                    _towerRuneDataController.SetStrategy(new ResetTowerRuneStrategy(_preTowerComposite.TowerId));
                    _towerRuneDataController.ExecuteStrategy();
                
                    // Get data from inventory data & Return Talent Point number after reset
                    _inventoryDataAsset.TryChangeInventoryData(_talentPointInventory.Type, _towerRuneDataController.GetReturnStar());
        
                    Debug.Log("Reset rune successful".ToUpper());
                    _onTowerRuneResetAction?.Invoke();
                }
            }
            else Debug.Log("CANNOT RESET THIS RUNE");
        }
        private TowerRuneComposite FindByTowerId(List<TowerRuneComposite> list, UnitId.Tower towerId)
        {
            TowerRuneComposite foundItem = list.Find(item => item.TowerId == towerId);
            if (foundItem.RuneComposite != null)
            {
                Debug.Log($"FindByTowerId: Found match for TowerId {towerId}");
                return foundItem;
            }
            else
            {
                Debug.Log($"FindByTowerId: No match found for TowerId {towerId}, returning default");
                return default(TowerRuneComposite);
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

    public struct TowerRuneComposite
    {
        public UnitId.Tower TowerId;
        public List<RuneComposite> RuneComposite;
    }
}
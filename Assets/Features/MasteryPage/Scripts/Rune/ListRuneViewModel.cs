using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class ListRuneViewModel : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private List<ItemRuneView> _itemRuneViews;
    [SerializeField] private RuneDetailView _runeDetailView;
    [SerializeField] private ItemUpgradeRuneView _itemUpgradeRuneView;
    [SerializeField] private StarView _starView;
    [SerializeField] private ListTowerViewModel _listTowerViewModel;
    
    [Header("Data"), Space(12)]
    [SerializeField] private InGameInventoryDataAsset _inventoryDataAsset;
    [SerializeField] private CommonTowerDataAsset _commonTowerDataAsset;
    [SerializeField] private RuneDataAsset _runeDataAsset;
    // Internal
    private List<RuneLevel> _runeLevels;
    private List<TowerComposite> _towerComposites;
    private List<RuneComposite> _runeComposites;
    private List<TowerDataAssetComposite> _towerDataAssetComposites;
    
    private InventoryComposite _inventoryComposite;
    private TowerDataAssetComposite _preTowerDataAssetComposite;

    private List<RuneSO> _runeSos;
    private RuneSO _preRuneSo;

    private ItemRuneView _preSelectedItem;
    private ItemUpgradeRuneView _preSelectedUpgradeRuneView;
    
    public Action _onTowerDataUpdatedAction;
    private void Awake()
    {
        _runeComposites = new List<RuneComposite>();
        _towerDataAssetComposites = new List<TowerDataAssetComposite>();
        _inventoryComposite = new InventoryComposite();
        
        UpdateData();
    }
    private void Start()
    {
        if (_listTowerViewModel != null)
            _listTowerViewModel._onUpdateViewAction += UpdateView;
        
        if (_starView != null)
            _starView._onDataUpdated += UpdateData;

        // if(_commonTowerDataAsset != null)
        //     _commonTowerDataAsset._onTowerDataUpdatedAction += UpdateView;

        _onTowerDataUpdatedAction += UpdateData;

        // if (_runeDetailView != null)
        //     _runeDetailView._onDataUpdated += UpdateData;
    }
    
    private void UpdateData()
    {
        _towerDataAssetComposites.Clear();
        _runeComposites.Clear();
        
        // Load Rune data
        List<RuneSO> listRuneSos = _runeDataAsset.GetAllRuneData();
        List<CommonTowerSO> listTowerDataAsset = _commonTowerDataAsset.GetAllTowerData();
        TowerDataAssetList loadedTowerData = DataAssetLoading.LoadTowerDataAssetList();
        
        foreach (var towerSo in listTowerDataAsset)
        {
            _runeComposites = new List<RuneComposite>();

            foreach (var runeSo in listRuneSos)
            {
                _runeComposites.Add(new RuneComposite
                {
                    RuneId = runeSo.GetRuneId(),
                    Name = runeSo._name,
                    Level = 0,
                    MaxLevel = runeSo._maxLevel,
                    AvatarSelected = runeSo._avatarSelected,
                    AvatarStarted = runeSo._avatarStarted
                });
            }

            if (loadedTowerData._towerList != null) // Check if json is new created or null
            {
                // Find the corresponding TowerSoSaver in loadedTowerData
                int towerSoSaverIndex = loadedTowerData._towerList.FindIndex(t => t._towerId == towerSo.GetTowerId());
                if (towerSoSaverIndex != -1)
                {
                    TowerSoSaver towerSoSaver = loadedTowerData._towerList[towerSoSaverIndex];
                    foreach (var runeLevel in towerSoSaver._runeLevels)
                    {
                        int runeCompositeIndex = _runeComposites.FindIndex(rc => rc.RuneId == runeLevel._runeId);
                        if (runeCompositeIndex != -1)
                        {
                            RuneComposite temp = _runeComposites[runeCompositeIndex];
                            temp.Level = runeLevel._level;
                            _runeComposites[runeCompositeIndex] = temp;
                        }
                    }
                }
            }
            
            _towerDataAssetComposites.Add(new TowerDataAssetComposite
            {
                TowerId = towerSo.GetTowerId(),
                RuneComposite = _runeComposites
            });
        }
        // Load Star data
        _inventoryComposite.Currency = _inventoryDataAsset.GetCurrencyValue();
        _inventoryComposite.Life = _inventoryDataAsset.GetLifeValue();
        _inventoryComposite.StarNumber = _inventoryDataAsset.GetStarValue();

        // Default setting
        if (_preTowerDataAssetComposite.RuneComposite == null)
            _preTowerDataAssetComposite = _towerDataAssetComposites[0];
        
        UpdateView(_preTowerDataAssetComposite.TowerId);
    }
    private void UpdateView(TowerId towerId)
    {
        var result = FindByTowerId(_towerDataAssetComposites, towerId);
        if (result.Equals(default(TowerDataAssetComposite)))
        {
            Debug.LogError("UpdateView: result is default, no update performed");
            return;
        }
        
        _preTowerDataAssetComposite = result;
        
        for (int runeIndex = 0; runeIndex < _itemRuneViews.Count; runeIndex++)
        {
            // Setup rune view
            _itemRuneViews[runeIndex].SetRuneStacks(result.RuneComposite[runeIndex]);
        
            // Setup star view
            _starView.Setup(_inventoryComposite);
        
            // Rune avatar logic
            _itemRuneViews[runeIndex].SetAvatarRune(result.RuneComposite[runeIndex].Level > 0 ? result.RuneComposite[runeIndex].AvatarSelected : result.RuneComposite[runeIndex].AvatarStarted);
        
            // Selected rune setup
            _itemRuneViews[runeIndex].Setup(result.RuneComposite[runeIndex], OnSelectedRuneItem);
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
        if (itemUpgradeRuneView == null || _preSelectedItem == null || _runeDataAsset == null || _starView == null)
        {
            Debug.LogError("One or more required objects are null.");
            return;
        }
        _preSelectedUpgradeRuneView = itemUpgradeRuneView;
        
        //Conditions to upgrade any skill
        if (_preSelectedItem.RuneComposite.Level <= _preSelectedItem.RuneComposite.MaxLevel  && _inventoryDataAsset.GetStarValue() > 0)
        {
            _preRuneSo = _runeDataAsset.GetRune(_preSelectedUpgradeRuneView.RuneComposite.RuneId);
            
            if (_preRuneSo != null)
            {
                _commonTowerDataAsset.UpdateTowerData(_preTowerDataAssetComposite.TowerId, _preSelectedUpgradeRuneView.RuneComposite);
            
                // Subtract star number
                _inventoryDataAsset.TryChangeStar(1);
        
                // Update rune data
                //_runeDetailView.UpdateCurrentStackView(_preRuneSo);
                Debug.Log("Upgrade rune successful....");
                
                _onTowerDataUpdatedAction?.Invoke();
            }
        } else {
            CommonLog.LogError("Upgrade rune fail");
        }
    }
    
    private TowerDataAssetComposite FindByTowerId(List<TowerDataAssetComposite> list, TowerId towerId)
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
        return default(TowerDataAssetComposite); 
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
}

public struct InventoryComposite
{
    public float Currency;
    public float Life;
    public float StarNumber;
}

public struct TowerDataAssetComposite
{
    public TowerId TowerId;
    public List<RuneComposite> RuneComposite;
}


using System;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private CommonTowerMasteryPageDataAsset _commonTowerMasteryPageDataAsset;
    
    // Internal
    private List<RuneDataSO> _listRuneDataSo;
    private List<TowerComposite> _towerComposites;
    private List<RuneComposite> _runeComposites;
    private List<TowerMasteryPageComposite> _towerMasteryPageComposites;
    
    private InventoryComposite _inventoryComposite;
    private TowerMasteryPageComposite _preTowerMasteryPageComposite;
    
    private RuneDataSO _preRuneDataSo;
    private MasteryPageDataAsset _masteryPageDataAsset;
    
    private ItemRuneView _preSelectedItem;
    private ItemUpgradeRuneView _preSelectedUpgradeRuneView;
    private void Awake()
    {
        _runeComposites = new List<RuneComposite>();
        _towerMasteryPageComposites = new List<TowerMasteryPageComposite>();
        _inventoryComposite = new InventoryComposite();
        
        UpdateData();
    }
    private void Start()
    {
        if (_listTowerViewModel != null)
        {
            _listTowerViewModel._onUpdateViewAction += UpdateView;
        }
        
        if (_commonTowerMasteryPageDataAsset != null)
            _commonTowerMasteryPageDataAsset._onDataUpdatedAction += UpdateData;

        if (_starView != null)
            _starView._onDataUpdated += UpdateData;
    }
    
    private void UpdateData()
    {
        _towerMasteryPageComposites.Clear();
        _runeComposites.Clear();
        
        // Load Rune data
        List<MasteryPageDataAsset> listMpDataSo = _commonTowerMasteryPageDataAsset.GetAllRuneData();

        foreach (var mpDataSo in listMpDataSo)
        {
            _runeComposites = new List<RuneComposite>();
            _listRuneDataSo = mpDataSo.GetAllRuneData();
            foreach (var runeDataSo in _listRuneDataSo)
            {
                _runeComposites.Add(new RuneComposite
                {
                    RuneId = runeDataSo.GetRuneId(),
                    TypeName = runeDataSo._name,
                    AdditionalValue =  runeDataSo._additionalValue,
                    Operate = runeDataSo._operate,
                    CurrentStacks = runeDataSo._currentStacks,
                    Stacks = runeDataSo._stacks,
                    StarNeedToUpgrade = runeDataSo._starNeedToUpgrade,
                    AvatarSelected = runeDataSo._avatarSelected,
                    AvatarStarted = runeDataSo._avatarStarted
                });
            }
            _towerMasteryPageComposites.Add(new TowerMasteryPageComposite
            {
                TowerId = _commonTowerMasteryPageDataAsset.GetTowerId(mpDataSo),
                RuneComposite = _runeComposites
            });
        }
        // Load Star data
        _inventoryComposite.Currency = _inventoryDataAsset.GetCurrencyValue();
        _inventoryComposite.Life = _inventoryDataAsset.GetLifeValue();
        _inventoryComposite.StarNumber = _inventoryDataAsset.GetStarValue();

        if (_preTowerMasteryPageComposite.RuneComposite == null)
        {
            _preTowerMasteryPageComposite = _towerMasteryPageComposites[0];
            _masteryPageDataAsset = _commonTowerMasteryPageDataAsset.GetMasteryPageDataAsset(_preTowerMasteryPageComposite.TowerId);
        }
     
        UpdateView(_preTowerMasteryPageComposite.TowerId);
    }
    private void UpdateView(TowerId towerId)
    {
        var result = FindByTowerId(_towerMasteryPageComposites, towerId);
        if (result.Equals(default(TowerMasteryPageComposite)))
        {
            Debug.LogError("UpdateView: result is default, no update performed");
            return;
        }
        
        _preTowerMasteryPageComposite = result;
        _masteryPageDataAsset =
            _commonTowerMasteryPageDataAsset.GetMasteryPageDataAsset(_preTowerMasteryPageComposite.TowerId);
        
        for (int runeIndex = 0; runeIndex < _itemRuneViews.Count; runeIndex++)
        {
            // Setup rune view
            _itemRuneViews[runeIndex].SetRuneStacks(result.RuneComposite[runeIndex]);
        
            // Setup star view
            _starView.Setup(_inventoryComposite);
        
            // Rune avatar logic
            _itemRuneViews[runeIndex].SetAvatarRune(result.RuneComposite[runeIndex].CurrentStacks > 0 ? result.RuneComposite[runeIndex].AvatarSelected : result.RuneComposite[runeIndex].AvatarStarted);
        
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
        if (itemUpgradeRuneView == null || _preSelectedItem == null || _masteryPageDataAsset == null || _starView == null)
        {
            Debug.LogError("One or more required objects are null.");
            return;
        }
        _preSelectedUpgradeRuneView = itemUpgradeRuneView;
        
        // Conditions to upgrade any skill
        if (_preSelectedItem.RuneComposite.CurrentStacks < _preSelectedItem.RuneComposite.Stacks && _inventoryDataAsset.GetStarValue() > 0)
        {
            _preRuneDataSo = _masteryPageDataAsset.GetRune(_preSelectedUpgradeRuneView.RuneComposite.RuneId);
            if (_preRuneDataSo != null)
            {
                _preRuneDataSo._currentStacks++;
            
                // Subtract star number
                _inventoryDataAsset.TryChangeStar(_preRuneDataSo._starNeedToUpgrade);

                // Update rune data
                _commonTowerMasteryPageDataAsset.UpdateMasteryPage(_masteryPageDataAsset, _preRuneDataSo);
                _runeDetailView.UpdateCurrentStackView(_preRuneDataSo);
                Debug.Log("Upgrade rune successful....");
            }
        } else {
            Debug.Log("Upgrade rune fail");
        }
    }
    
    private TowerMasteryPageComposite FindByTowerId(List<TowerMasteryPageComposite> list, TowerId towerId)
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
        return default(TowerMasteryPageComposite); 
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
    public string TypeName;
    public string AdditionalValue;
    public string Operate;
    public float CurrentStacks;
    public float Stacks;
    public int StarNeedToUpgrade;
    public Sprite AvatarSelected;
    public Sprite AvatarStarted;
}

public struct InventoryComposite
{
    public float Currency;
    public float Life;
    public float StarNumber;
}

public struct TowerMasteryPageComposite
{
    public TowerId TowerId;
    public List<RuneComposite> RuneComposite;
}


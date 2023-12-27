using System;
using System.Collections.Generic;
using UnityEngine;

public class ListTowerViewModel : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private List<ItemTowerView> _itemTowerViews;
    [SerializeField] private ListRuneViewModel _listRuneViewModel;
    
    [Header("Data"), Space(12)]
    [SerializeField] private CommonTowerDataAsset _commonTowerDataAsset;
    
    private List<TowerComposite> _towerComposites;
    private TowerComposite _towerComposite;
    
    private ItemTowerView _preSelectedItem;
    
    public Action<TowerId> _onUpdateViewAction;
    private void Start()
    {
        _itemTowerViews[0].OnSelectedTower();
    }

    private void Awake()
    {
        _towerComposites = new List<TowerComposite>();
        
        UpdateData();
    }

    private void UpdateData()
    {
        List<Stats> listTowerData = _commonTowerDataAsset.GetAllTowerData();
        
        foreach (var towerDataSo in listTowerData)
        {
            _towerComposites.Add(
                new TowerComposite
                {
                    TowerId = _commonTowerDataAsset.GetTowerId(towerDataSo),
                    Name = towerDataSo.GetInformation(InformationId.Name),
                    MaxHeal = towerDataSo.GetStat(StatId.MaxHeal).ToString(""),
                    AttackDamage = towerDataSo.GetStat(StatId.AttackDamage).ToString(""),
                    AttackSpeed = towerDataSo.GetStat(StatId.AttackSpeed).ToString("F2"),
                    DetectRange = towerDataSo.GetStat(StatId.DetectRange).ToString(""),
                    CoinNeedToBuild = towerDataSo.GetStat(StatId.CoinNeedToBuild).ToString(""),
                    CoinNeedToUpgrade = towerDataSo.GetStat(StatId.CoinNeedToUpgrade).ToString("")
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
    public string Name;
    public string MaxHeal;
    public string AttackDamage;
    public string AttackSpeed;
    public string DetectRange;
    public string CoinNeedToBuild;
    public string CoinNeedToUpgrade;
}

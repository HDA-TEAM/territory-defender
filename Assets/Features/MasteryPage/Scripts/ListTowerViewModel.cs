using System.Collections.Generic;
using UnityEngine;

public class ListTowerViewModel : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private List<ItemTowerView> _itemTowerViews;
    
    [Header("Data"), Space(12)]
    [SerializeField] private CommonTowerDataAsset _commonTowerDataAsset;
    
    private List<TowerComposite> _towerComposites;
    private ItemTowerView _preSelectedItem;

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

        UpdataView();
    }

    private void UpdataView()
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
    }
}

public struct TowerComposite
{
    public string Name;
    public string MaxHeal;
    public string AttackDamage;
    public string AttackSpeed;
    public string DetectRange;
    public string CoinNeedToBuild;
    public string CoinNeedToUpgrade;
}

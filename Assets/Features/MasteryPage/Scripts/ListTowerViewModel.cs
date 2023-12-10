
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public struct TowerComposite
{
    public string Name;
    public string MaxHeal;
    public string AttackDamage;
    public string AttackSpeed;
    public string DetectRange;
    public string CoinValue;
}
public class ListTowerViewModel : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private List<ItemTowerView> _itemTowerViews;
    
    [Header("Data"), Space(12)] 
    [SerializeField] private TowerDataAsset _towerDataAsset;
    
    private List<TowerComposite> _towerComposites;
    private ItemTowerView _preSelectedItem;
    private void Awake()
    {
        _towerComposites = new List<TowerComposite>();

        UpdateData();
    }

    private void UpdateData()
    {
        //List<UnitBase> listTowerData = _towerDataAsset.GetAllTowerData();

        _towerComposites.Add(
            new TowerComposite
            {
                Name = "Archer Tower",
                MaxHeal = "999",
                AttackDamage = "99",
                AttackSpeed = "9.9",
                DetectRange = "3",
                CoinValue = "60"
            }
        );
            
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
        if (_preSelectedItem != null)
        {
            _preSelectedItem.RemoveSelected();
        }

        _preSelectedItem = itemTowerView;
    }
}

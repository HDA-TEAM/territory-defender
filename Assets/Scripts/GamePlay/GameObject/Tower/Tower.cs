using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Tower : MonoBehaviour
{
    [Header("Tower attribute")]
    [SerializeField] protected int _towerId;
    [SerializeField] protected string _name;
    [SerializeField] protected float _attackSpeed;
    [SerializeField] protected float _attackDame;
    [SerializeField] protected float _attackRange;
    [SerializeField] protected float _health;
    [SerializeField] protected int _price;
    [SerializeField] protected int _sellPrice;
    [SerializeField] protected int _level;


    [SerializeField] protected Button _controlButton;
    [SerializeField] protected Image _updateAndSelllContainer;

    
    public int Level{
        get{
            return _level;
        }
        set{
            _level = value;
        }
    }
    public int Price{
        get{
            return _price;
        }
        set{
            _price = value;
        }
    }
    public int SellPrice{
        get{
            return _sellPrice;
        }
        set{
            _sellPrice = value;
        }
    }
    
    protected void Start()
    {
        _level = 0;
        _controlButton.onClick.AddListener(OpenUpdateAndSell);
        MapTowerData(_towerId,_level);
    }
    void OpenUpdateAndSell(){
        _controlButton.gameObject.SetActive(false);
        _updateAndSelllContainer.gameObject.SetActive(true);
        InformationBoardControl.ShowTowerInformation(_attackDame,_attackSpeed,_attackRange);
    }

    public bool IsAbleUpdateTower(){
        if(_level < 2){
            return true;
        }
        return false;
    }
    public void UpdateTower(){
        MapTowerData(_towerId,++_level);
    }

    protected bool MapTowerData(int id, int level){
        TowerData towerData = XMLControler._towerDataList.FindTowerData(id,level);
        if(towerData != null){
            _name = towerData.name;
            _attackDame = towerData.attackDame;
            _attackSpeed = towerData.attackSpeed;
            _attackRange = towerData.attackRange;
            _health = towerData.health;
            _price = towerData.price;
            _sellPrice +=(int)(_price * 0.3f);
            return true;
        }
        return false;
    }
    protected void Update(){
 
    }

}

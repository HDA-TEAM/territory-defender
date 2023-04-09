using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpdateAndSell : MonoBehaviour
{
    /// <summary>
    /// 1) Control Update and sell of a tower
    /// 2) Control AssemblePoint of tower, if that tower is a TroopTower
    /// </summary>
    
    [SerializeField] private GameObject _updateContainer;
    [SerializeField] private Button _controlButton;
    [SerializeField] private Button _updateButton;
    [SerializeField] private Button _sellButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _assembleButton;
    [SerializeField] private Tower _currentTower;
    [SerializeField] private Text _sellPrice;
    [SerializeField] private Text _updatePrice;
    [SerializeField] private AssemblePoint _assemblePoint;

    void Start()
    {
        _updateButton.onClick.AddListener(UpdateTower);
        _sellButton.onClick.AddListener(SellTower);
        _exitButton.onClick.AddListener(Exit);
        if(_assembleButton != null){
            _assembleButton.onClick.AddListener(AssemblePoint);
        }
    }

    void UpdateTower(){
        //update tower
        int updatePrice = _currentTower.Price;
        if(_currentTower.IsAbleUpdateTower() && CoinIsEnough(updatePrice)){
            _currentTower.UpdateTower();
            GameControl.DecreaseCoin(updatePrice);
        }
        SetActive(false);
    }
    void DisableUpdate(){
        _updateContainer.SetActive(false);
    }
    private void Update() {
        CoinIsEnough(_currentTower.Price);
        if(_currentTower.IsAbleUpdateTower() == false){
            DisableUpdate();
        }
        _sellPrice.text = _currentTower.SellPrice.ToString();
        _updatePrice.text = _currentTower.Price.ToString();
    }
    
    public void SellTower(){
       
        BuidingPlaceController.ActiveInstance(transform);
        
        GameControl.IncreaseCoin(_currentTower.SellPrice);

        Destroy(_currentTower.transform.parent.gameObject);

        SetActive(false);
    }
    void Exit(){
        
        SetActive(false);
    }

    void AssemblePoint(){
        _assemblePoint._settingAssemblePoint = true;
        SetActive(false);
    }
    void SetActive(bool isActive){
        _controlButton.gameObject.SetActive(!isActive);
        this.gameObject.SetActive(isActive);
    }
    bool CoinIsEnough(int coint){
        if (GameControl.CoinIsEnough(coint) == true){
            ChangedPriceTextColor(Color.yellow);
            return true;
        }
        else{
            ChangedPriceTextColor(Color.red);
            return false;
        }
    }
    void ChangedPriceTextColor(Color color){
        _updatePrice.color = color;
    }
}



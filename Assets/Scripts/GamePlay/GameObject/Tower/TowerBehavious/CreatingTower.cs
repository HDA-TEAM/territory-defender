using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatingTower : MonoBehaviour
{
    /// <summary>
    /// 1) Control create a specific tower type
    /// 2) Determine condition to decide the tower enough money to build or not
    /// </summary>

    [SerializeField]
    private GameObject _prefab;

    [SerializeField]
    private Button _buildButton;

    [SerializeField]
    private GameObject _buildingPlaceContainer;

    [SerializeField]
    private Image _buildingContainer;

    [SerializeField]
    private Text _priceText;

    private int _towerPrice;
    void Start()
    {
        _buildButton.onClick.AddListener(Create);
        SetupTowerPrice();
    }
    void Create(){
        if(CoinIsEnough() == true){
            Vector2 initPosition = new Vector2(_buildingPlaceContainer.transform.position.x ,_buildingPlaceContainer.transform.position.y + _prefab.transform.position.y);
            Instantiate(_prefab,initPosition,Quaternion.identity);
            resetState();
            GameControl.DecreaseCoin(_towerPrice);
        }
        else{
            Debug.Log("Dont's enough money");
        }
    }
    
    void resetState(){
        
        BuidingPlaceController.returnToBool(_buildingPlaceContainer);
        _buildingContainer.gameObject.SetActive(false);
    }

    bool CoinIsEnough(){
        if (GameControl.CoinIsEnough(_towerPrice) == true){
            ChangedPriceTextColor(Color.yellow);
            return true;
        }
        else{
            ChangedPriceTextColor(Color.red);
            return false;
        }
    }
    void ChangedPriceTextColor(Color color){
        _priceText.color = color;
    }
    void SetupTowerPrice(){
        GameObject instance = Instantiate(_prefab);

        if(instance.GetComponentInChildren<Bow>() != null){
            _towerPrice = (int) XMLControler._towerDataList.FindTowerData(0,0).price;
        }
        else if(instance.GetComponentInChildren<Magic>() != null){
            _towerPrice = (int) XMLControler._towerDataList.FindTowerData(1,0).price;
        }
        else if(instance.GetComponentInChildren<Catapult>() != null){
            _towerPrice = (int) XMLControler._towerDataList.FindTowerData(2,0).price;
        }
        else if(instance.GetComponentInChildren<TowerTroop>() != null){
            _towerPrice = (int) XMLControler._towerDataList.FindTowerData(3,0).price;
        }
        _priceText.text = _towerPrice.ToString();
        Destroy(instance);
    }

    // Update is called once per frame
    void Update()
    {
        CoinIsEnough();
    }
}

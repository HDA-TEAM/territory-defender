using SuperMaxim.Messaging;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum TowerKitState
{
    Default = 0,
    Building = 1,
    TowerExisted = 2,
    Hiding = 3,
}

public class TowerKit : MonoBehaviour
{
    [Header("Event"), Space(12)]
    [SerializeField] private Button _btn;

    [Header("UI"), Space(12)]
    [SerializeField] private CanvasGroup _canvasGroupBtn;
    [SerializeField] private GameObject _towerBuildTool;
    [SerializeField] private GameObject _towerUsingTool;
    [SerializeField] private GameObject _spawnTowerHolder;

    [Header("Data"), Space(12)]
    [SerializeField] private TowerDataAsset _towerDataAsset;

    // Internal
    private TowerKitState _towerKitState;
    private TowerKitState TowerKitState
    {
        get
        {
            return _towerKitState;
        }
        set
        {
            _towerKitState = value;
            SetMenuState();
        }
    }
    private GameObject _towerEntity;

    // call back
    private Action<TowerKit> _onSelected;

    private void Start()
    {
        TowerKitState = TowerKitState.Default;
        _btn.onClick.AddListener(OnSelected);
    }
    private void OnSelected()
    {
        if (TowerKitSetController.Instance.CurrentSelectedKit == this 
            && (TowerKitState == TowerKitState.TowerExisted || TowerKitState == TowerKitState.Building))
        {
            // reset state
            OnCancelMenu();
            // Turn off canvas block raycast
            Messenger.Default.Publish(new HandleCancelRaycastPayload
            {
                IsOn = false,
                callback = null,
            });
            return;
        }
        
        _onSelected?.Invoke(this);
        TowerKitState = _towerEntity ? TowerKitState.TowerExisted : TowerKitState.Building;

        Debug.Log("Onclick");
        // Handle user want to cancel selection menu by canvas block raycast
        Messenger.Default.Publish(new HandleCancelRaycastPayload
        {
            IsOn = true,
            callback = OnCancelMenu,
        });
    }
    public void OnCancelMenu()
    {
        TowerKitState = _towerEntity ? TowerKitState.Hiding : TowerKitState.Default;
    }
    private void SetMenuState()
    {
        _canvasGroupBtn.alpha = 0f;
        _towerBuildTool.SetActive(false);
        _towerUsingTool.SetActive(false);
        switch (_towerKitState)
        {
            case TowerKitState.Default:
                {
                    _canvasGroupBtn.alpha = 1f;
                    return;
                }
            case TowerKitState.Building:
                {
                    _canvasGroupBtn.alpha = 1f;
                    _towerBuildTool.SetActive(true);
                    return;
                }
            case TowerKitState.TowerExisted:
                {
                    _towerUsingTool.SetActive(true);
                    return;
                }
            case TowerKitState.Hiding:
                {
                    return;
                }
            default: throw new ArgumentOutOfRangeException();
        }
    }
    public void Setup(Action<TowerKit> onSelected)
    {
        _onSelected = onSelected;
    }
    public void SetTower(GameObject tower)
    {
        _towerEntity = tower;
        _towerEntity.transform.SetParent(_spawnTowerHolder.transform);
        _towerEntity.transform.position = _spawnTowerHolder.transform.position;
        TowerKitState = TowerKitState.Hiding;
    }
    public void UpgradeTower()
    {
        // _towerEntity = tp;
        // _towerEntity.transform.SetParent(_spawnTowerHolder.transform);
        // _towerEntity.transform.position = _spawnTowerHolder.transform.position;
        // TowerKitState = TowerKitState.Hiding;
    }
    public void SellingTower()
    {
        //todo 
        // Logic get 30% coin used 
        // UnitBase towerBase = _towerEntity.GetComponent<UnitBase>();
        // towerBase.UnitStatsComp().GetStat(StatId.Armour
        
        Destroy(_towerEntity);
        TowerKitState = TowerKitState.Default;
    }
}

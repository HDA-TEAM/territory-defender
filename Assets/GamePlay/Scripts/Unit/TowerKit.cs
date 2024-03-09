using SuperMaxim.Messaging;
using System;
using UnityEngine;
using UnityEngine.UI;

public enum TowerKitState
{
    // Just show flag on map
    Default = 0,
    // Show flag and build tower tool
    Building = 1,
    // Show tool of current tower entity
    ShowToolOfTowerExisted = 2,
    // Hide all tool and show curren tower
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
    [SerializeField] private Button _btnRange;

    [Header("Data"), Space(12)]
    [SerializeField] private TowerDataAsset _towerDataAsset;
    [SerializeField] private TowerId _towerId;
    [SerializeField] private UnitBase _unitBase;
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

    #region Access
    public TowerId GetTowerId() => _towerId;

    public UnitBase GetUnitBase() => _unitBase;
    
    #endregion
    
    private void Start()
    {
        TowerKitState = TowerKitState.Default;
        _btn.onClick.AddListener(OnSelected);
    }
    private void OnSelected()
    {
        if (TowerKitSetController.Instance.CurrentSelectedKit == this 
            && (TowerKitState == TowerKitState.ShowToolOfTowerExisted || TowerKitState == TowerKitState.Building))
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
        TowerKitState = _towerEntity ? TowerKitState.ShowToolOfTowerExisted : TowerKitState.Building;

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
            case TowerKitState.ShowToolOfTowerExisted:
                {
                    _towerUsingTool.SetActive(true);
                    
                    _unitBase.UnitShowingInformationComp().ShowUnitInformation();

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
    public void SetTower(GameObject tower, TowerId towerId)
    {
        _towerId = towerId;
        _towerEntity = tower;
        _unitBase = _towerEntity.GetComponent<UnitBase>();

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

        _unitBase = null;
        Destroy(_towerEntity);
        TowerKitState = TowerKitState.Default;
    }
    public void ActiveCampingMode()
    {
        
        var rangeVal= _unitBase.UnitStatsHandlerComp().GetCurrentStatValue(StatId.CampingRange);
        
        SetRangeOfTower(rangeVal);

        TowerKitState = TowerKitState.Hiding;
        _btnRange.gameObject.SetActive(true);

        _btnRange.onClick.AddListener(() =>
        {
            // Set camping position
            var troopTowerBehaviour = _unitBase.GetComponent<TroopTowerBehaviour>();
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.LogError("Camping position : " + mousePos);
            troopTowerBehaviour.SetCampingPlace(new Vector3(mousePos.x, mousePos.y,0));
            
            // Hiding select camping position
            _btnRange.gameObject.SetActive(false);
            TowerKitState = TowerKitState.Hiding;
            _btnRange.onClick.RemoveAllListeners();
        });
    }
    private void SetRangeOfTower(float range)
    {
        _btnRange.image.rectTransform.sizeDelta = new Vector2(range * 2, range * 2);
    }
}

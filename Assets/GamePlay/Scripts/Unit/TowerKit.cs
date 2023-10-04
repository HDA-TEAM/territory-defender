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
    [Header("Event"),Space(12)]
    [SerializeField] private Button _btn;
    
    [Header("UI"),Space(12)]
    [SerializeField] private CanvasGroup _canvasGroupBtn;
    [SerializeField] private GameObject _towerBuildTool;
    [SerializeField] private GameObject _towerUsingTool;
    [SerializeField] private GameObject _spawnTowerHolder;
    
    [Header("Data"),Space(12)]
    [SerializeField] private TowerDataAsset _towerDataAsset;
    
    // Internal
    private TowerKitState _towerKitState;
    private TowerKitState TowerKitState
    {
        get {
            return _towerKitState;
        }
        set {
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
        Debug.LogError("Kit Selected " + this );
        _onSelected?.Invoke(this);
        TowerKitState = _towerEntity ? TowerKitState.TowerExisted : TowerKitState.Building;
        
        Messenger.Default.Publish(new HandleCancelRaycastPayload
        {
            IsOn = true,
            callback = OnCancelMenu,
        });
    }
    private void OnCancelMenu()
    {
        TowerKitState = _towerEntity ? TowerKitState.Hiding : TowerKitState.Default;
    }
    private void SetMenuState()
    {
        _canvasGroupBtn.alpha = 0f;
        _canvasGroupBtn.interactable = false;
        _towerBuildTool.SetActive(false);
        _towerUsingTool.SetActive(false);
        switch (_towerKitState)
        {
            case TowerKitState.Default :
            {
                _canvasGroupBtn.alpha = 1f;
                _canvasGroupBtn.interactable = true;
                return;
            }
            case TowerKitState.Building :
            {
                _canvasGroupBtn.alpha = 1f;
                _towerBuildTool.SetActive(true);
                return;
            }
            case TowerKitState.TowerExisted :
            {
                _towerUsingTool.SetActive(true);
                return;
            }
            case TowerKitState.Hiding :
            {
                _canvasGroupBtn.interactable = true;
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
}

using Common.Scripts;
using CustomInspector;
using GamePlay.Scripts.Character.Stats;
using GamePlay.Scripts.Data;
using GamePlay.Scripts.GamePlayController;
using GamePlay.Scripts.Tower.TowerKIT.PreviewTooltip;
using GamePlay.Scripts.Tower.TowerKIT.TowerTools;
using SuperMaxim.Messaging;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Scripts.Tower.TowerKIT
{
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
        [SerializeField] private SpriteRenderer _spiteFlag;

        [Header("Preview Tooltip"), Space(12)]
        [SerializeField] private TowerCampingSelection _towerCampingSelection;
        
        [Header("Preview Tooltip"), Space(12)]
        [SerializeField] private HandleTowerShowTooltip _towerShowTooltip;
        
        [Header("Data"), Space(12)]
        [SerializeField] private InGameInventoryRuntimeData _inventoryRuntime;
        [SerializeField] private UnitId.Tower _towerId;
        [SerializeField] private UnitBase _unitBase;
        
        [Header("Sounds"), Space(12)]
        [SerializeField] private AudioClip _audioClipOpenKit;
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
        [ReadOnly][SerializeField] private int _totalUsedCoin;
    
        // call back
        private Action<TowerKit> _onSelected;

        #region Access
        public UnitId.Tower GetTowerId() => _towerId;

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

            // Handle user want to cancel selection menu by canvas block raycast
            Messenger.Default.Publish(new HandleCancelRaycastPayload
            {
                UnitSelectionShowType = _towerEntity ? EUnitSelectionShowType.ShowInformationPanelAndBlockRaycast : EUnitSelectionShowType.OnlyBlockRaycast,
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
            _spiteFlag.gameObject.SetActive(false);
            _towerBuildTool.SetActive(false);
            _towerUsingTool.SetActive(false);
            _towerShowTooltip.HideAll();
            switch (_towerKitState)
            {
                case TowerKitState.Default:
                    {
                        _spiteFlag.gameObject.SetActive(true);
                        _canvasGroupBtn.alpha = 1f;
                        return;
                    }
                case TowerKitState.Building:
                    {
                        _canvasGroupBtn.alpha = 1f;
                        _spiteFlag.gameObject.SetActive(true);
                        _towerBuildTool.SetActive(true);
                        
                        Messenger.Default.Publish(new AudioPlayOneShotPayload
                        {
                            AudioClip = _audioClipOpenKit,
                        });
                        
                        return;
                    }
                case TowerKitState.ShowToolOfTowerExisted:
                    {
                        _towerUsingTool.SetActive(true);
                    
                        _unitBase.UnitShowingInformationComp().ShowUnitInformation();

                        Messenger.Default.Publish(new AudioPlayOneShotPayload
                        {
                            AudioClip = _audioClipOpenKit,
                        });
                        
                        return;
                    }
                case TowerKitState.Hiding:
                    {
                        Messenger.Default.Publish(new HandleCancelRaycastPayload
                        {
                            UnitSelectionShowType = EUnitSelectionShowType.HidingAll,
                            IsOn = false,
                            callback = null,
                        });
                        return;
                    }
                default: throw new ArgumentOutOfRangeException();
            }
        }
        public void Setup(Action<TowerKit> onSelected)
        {
            _onSelected = onSelected;
        }
        public void SetTower(GameObject tower, UnitId.Tower towerId)
        {
            _towerId = towerId;
            _towerEntity = tower;
            _unitBase = _towerEntity.GetComponent<UnitBase>();

            // Reduce coin in inventory
            var coinNeedToBuild = (int)_unitBase.UnitStatsHandlerComp().GetCurrentStatValue(StatId.CoinNeedToBuild);
            _inventoryRuntime.TryChangeCurrency(
                - coinNeedToBuild);
            _totalUsedCoin += coinNeedToBuild;
        
            _towerEntity.transform.SetParent(_spawnTowerHolder.transform);
            _towerEntity.transform.position = _spawnTowerHolder.transform.position;
            TowerKitState = TowerKitState.Hiding;
        }
        public void ShowPreviewChanging(TowerPreviewToolTipBase towerPreviewToolTipBase)
        {
            _towerShowTooltip.ShowTooltip(towerPreviewToolTipBase);
        }
        public void UpgradeTower()
        {
            // _towerEntity = tp;
            // _towerEntity.transform.SetParent(_spawnTowerHolder.transform);
            // _towerEntity.transform.position = _spawnTowerHolder.transform.position;
            // TowerKitState = TowerKitState.Hiding;
        }
        public int GetSoldTowerCoin()
        {
            // Logic get 30% coin used 
            return _totalUsedCoin * 30 / 100;
        }
        public void SellingTower()
        {
            _inventoryRuntime.TryChangeCurrency(GetSoldTowerCoin());
            // reset Coin
            _totalUsedCoin = 0;

            _unitBase = null;
            Destroy(_towerEntity);
            TowerKitState = TowerKitState.Default;
        }
        private void SetFlagActive(bool isActive)
        {
            _spiteFlag.gameObject.SetActive(isActive);
        }
        public void ActiveCampingMode()
        {
        
            var rangeVal= _unitBase.UnitStatsHandlerComp().GetCurrentStatValue(StatId.CampingRange);

            TowerKitState = TowerKitState.Hiding;
            SetFlagActive(true);
            
            _towerCampingSelection.gameObject.SetActive(true);
            _towerCampingSelection.SetUp(rangeVal, OnSelectCampingPlace);
        }
        private void OnSelectCampingPlace()
        {
            var troopTowerBehaviour = _unitBase.GetComponent<TroopTowerBehaviour>();
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            var campingPos = new Vector3(mousePos.x, mousePos.y,0);

            troopTowerBehaviour.SetCampingPlace(campingPos);
            _towerCampingSelection.SetFlagCampingPos(campingPos);
            
            // Hiding select camping position
            SetFlagActive(false);
            TowerKitState = TowerKitState.Hiding;
        }
    }
}
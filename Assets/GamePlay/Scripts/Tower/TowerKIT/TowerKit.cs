using Common.Scripts;
using CustomInspector;
using GamePlay.Scripts.Character.Stats;
using GamePlay.Scripts.Character.TowerBehaviour;
using GamePlay.Scripts.Data;
using GamePlay.Scripts.GamePlayController;
using GamePlay.Scripts.Menu.InGameStageScreen.UnitInformationPanel;
using GamePlay.Scripts.Tower.TowerKIT.PreviewTooltip;
using GamePlay.Scripts.Tower.TowerKIT.TowerTools;
using SuperMaxim.Messaging;
using System;
using UnityEngine;
using UnityEngine.Serialization;
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

        [Header("Tower Camping Selection"), Space(12)]
        [SerializeField] private TowerRangingHandler _towerRangingHandler;
        
        [Header("Tower Camping Selection"), Space(12)]
        [SerializeField] private TowerCampingSelection _towerCampingSelection;
        
        [Header("Preview Tooltip"), Space(12)]
        [SerializeField] private HandleTowerShowTooltip _towerShowTooltip;
        
        [FormerlySerializedAs("_inventoryRuntime")]
        [Header("Data"), Space(12)]
        [SerializeField] private InGameResourceRuntimeData _resourceRuntime;
        [SerializeField] private UnitId.Tower _towerId;
        [SerializeField] private UnitBase _unitBaseOfCurrentTower;
        
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
        private GameObject _previewTowerEntity;
        [ReadOnly][SerializeField] private int _totalUsedCoin;
    
        // call back
        private Action<TowerKit> _onSelected;

        #region Access
        public UnitId.Tower GetTowerId() => _towerId;
        public TowerRangingHandler TowerRangingHandler() => _towerRangingHandler;
        public UnitBase GetUnitBase() => _unitBaseOfCurrentTower;
        public bool IsExistTower() => _unitBaseOfCurrentTower;
        #endregion

        private TowerKitSetController _towerKitSetController;
        private void Start()
        {
            TowerKitState = TowerKitState.Default;
            _btn.onClick.AddListener(OnSelected);
        }
        private void OnSelected()
        {
            if (_towerKitSetController.IsCurrentSelectedKit(this)
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
            _towerRangingHandler.SetShowRanging(false);
            _towerBuildTool.SetActive(false);
            _towerUsingTool.SetActive(false);
            _towerShowTooltip.HideAll();
            switch (_towerKitState)
            {
                case TowerKitState.Default:
                    {
                        _spiteFlag.gameObject.SetActive(true);
                        _canvasGroupBtn.alpha = 1f;
                        CheckAndRemoveExistTower(_previewTowerEntity);
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
                        
                        _unitBaseOfCurrentTower.TowerBehaviourBase().ShowTool();
                        _unitBaseOfCurrentTower.UnitShowingInformationComp().ShowUnitInformation();

                        Messenger.Default.Publish(new AudioPlayOneShotPayload
                        {
                            AudioClip = _audioClipOpenKit,
                        });
                        
                        return;
                    }
                case TowerKitState.Hiding:
                    {
                        _towerRangingHandler.SetShowRanging(false);
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
        public void Setup(Action<TowerKit> onSelected, TowerKitSetController towerKitSetController)
        {
            _onSelected = onSelected;
            _towerKitSetController = towerKitSetController;
        }
        private void CheckAndRemoveExistTower(GameObject tower)
        {
            if (!tower)
                return;
            
            Destroy(tower);
        }
        public void SetTower(GameObject tower, UnitId.Tower towerId)
        {
            CheckAndRemoveExistTower(_towerEntity);
            CheckAndRemoveExistTower(_previewTowerEntity);
            _towerId = towerId;
            _towerEntity = tower;
            _unitBaseOfCurrentTower = _towerEntity.GetComponent<UnitBase>();
            _unitBaseOfCurrentTower.TowerBehaviourBase().Setup(this);
            
            // Reduce coin in inventory
            var coinNeedToBuild = (int)_unitBaseOfCurrentTower.UnitStatsHandlerComp().GetCurrentStatValue(StatId.CoinNeedToBuild);
            _resourceRuntime.TryChangeCurrency(
                - coinNeedToBuild);
            _totalUsedCoin += coinNeedToBuild;
        
            _towerEntity.transform.SetParent(_spawnTowerHolder.transform);
            _towerEntity.transform.position = _spawnTowerHolder.transform.position;
            TowerKitState = TowerKitState.Hiding;
        }
        public void SetPreviewTower(GameObject tower, UnitId.Tower towerId)
        {
            CheckAndRemoveExistTower(_previewTowerEntity);
            
            _towerId = towerId;
            _previewTowerEntity = tower;

            _previewTowerEntity.transform.SetParent(_spawnTowerHolder.transform);
            _previewTowerEntity.transform.position = _spawnTowerHolder.transform.position;
            
            SetFlagActive(false);
        }
        public void ShowPreviewChanging(TowerPreviewToolTipBase towerPreviewToolTipBase)
        {
            _towerShowTooltip.ShowTooltip(towerPreviewToolTipBase);
        }
        public int GetSoldTowerCoin()
        {
            // Logic get 30% coin used 
            return _totalUsedCoin * 30 / 100;
        }
        public void SellingTower()
        {
            _resourceRuntime.TryChangeCurrency(GetSoldTowerCoin());
            // reset Coin
            _totalUsedCoin = 0;

            _unitBaseOfCurrentTower = null;
            Destroy(_towerEntity);

            // Hiding tower info
            Messenger.Default.Publish(new HideUnitInformationPayload());
            
            TowerKitState = TowerKitState.Default;
        }
        private void SetFlagActive(bool isActive)
        {
            _spiteFlag.gameObject.SetActive(isActive);
        }
        public void ActiveCampingMode()
        {
            TowerKitState = TowerKitState.Hiding;
            SetFlagActive(true);
            
            _towerCampingSelection.gameObject.SetActive(true);
            // Hiding select camping position
            SetFlagActive(false);
            
            _towerCampingSelection.SetUp(OnSelectCampingPlace);
        }
        private void OnSelectCampingPlace()
        {
            var troopTowerBehaviour = _unitBaseOfCurrentTower.GetComponent<TroopTowerBehaviour>();
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            var campingPos = new Vector3(mousePos.x, mousePos.y,0);

            troopTowerBehaviour.SetCampingPlace(campingPos);
            _towerCampingSelection.SetFlagCampingPos(campingPos);
            
            TowerKitState = TowerKitState.Hiding;
        }
    }
}
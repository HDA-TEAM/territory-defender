using Common.Scripts;
using Common.Scripts.Data.DataAsset;
using Features.Home.Scripts.HomeScreen.Common;
using Features.MasteryPage.Scripts.Rune;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Features.MasteryPage.Scripts.Tower
{
    public class ListTowerViewModel : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private List<ItemTowerView> _itemTowerViews;
        [SerializeField] private ListRuneViewModel _listRuneViewModel;

        public Action<UnitId.Tower> _onUpdateViewAction;

        // Internal
        private List<TowerComposite> _towerComposites;
        private ItemTowerView _preSelectedItem;
        private void Start()
        {
            UpdateData();
        
            //Setup default state
            OnSelectedItem(_itemTowerViews[0]); 
        }
        private void OnDisable()
        {
            ResetView();
        }

        private void UpdateData()
        {
            var towerDataManager = TowerDataManager.Instance;

            if (towerDataManager == null) return;
            if (towerDataManager.TowerComposites == null) return;

            _towerComposites = towerDataManager.TowerComposites;
            UpdateView();
        }

        private void UpdateView()
        {
            for (int i = 0; i < _itemTowerViews.Count; i++)
            {
                if (i < _towerComposites.Count)
                {
                    // Setup hero property
                    _itemTowerViews[i].Setup(_towerComposites[i], OnSelectedItem);
                    _itemTowerViews[i].gameObject.SetActive(true);
                }
                else
                {
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
            _preSelectedItem.OnSelectedTower();
        
            Debug.Log($"Invoking actions for tower ID: {itemTowerView.TowerComposite.TowerId}");

            // Reset view of rune detail
            //GlobalUtility.ResetRuneDetailView(_listRuneViewModel);
            _onUpdateViewAction?.Invoke(_preSelectedItem.TowerComposite.TowerId);
        }

        public void ResetView()
        {
            // Reset the selection state
            if (_preSelectedItem != null)
            {
                _preSelectedItem.RemoveSelected();
                _preSelectedItem = null;
            }

            // Reset the view to its initial state, such as selecting the first tower again
            if (_itemTowerViews != null)
            {
                _itemTowerViews[0].OnSelectedTower();
                _onUpdateViewAction?.Invoke(_towerComposites[0].TowerId);
            }
        }
    }

    public struct TowerComposite
    {
        public UnitId.Tower TowerId;
        public List<RuneLevel> RuneLevels;
    }
}
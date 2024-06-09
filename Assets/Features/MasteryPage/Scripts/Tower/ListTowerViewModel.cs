using Common.Scripts;
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
        //[SerializeField] private TowerRuneDataController _towerRuneDataController;
        
        public Action<UnitId.Tower> _onUpdateViewAction;

        // Internal
        private List<TowerRuneComposite> _towerRuneComposites;
        private ItemTowerView _preSelectedItem;
        public void SetupTower()
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
            //var towerDataManager = TowerDataManager.Instance;

            if (_listRuneViewModel._towerRuneDataController == null) return;
            if (_listRuneViewModel._towerRuneDataController.TowerRuneComposites == null) return;

            _towerRuneComposites = _listRuneViewModel._towerRuneDataController.TowerRuneComposites;
            UpdateView();
        }

        private void UpdateView()
        {
            for (int i = 0; i < _itemTowerViews.Count; i++)
            {
                if (i < _towerRuneComposites.Count)
                {
                    // Setup hero property
                    _itemTowerViews[i].Setup(_towerRuneComposites[i], OnSelectedItem);
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
        
            Debug.Log($"Invoking actions for tower ID: {itemTowerView.TowerRuneComposite.TowerId}");

            // Reset view of rune detail
            _onUpdateViewAction?.Invoke(_preSelectedItem.TowerRuneComposite.TowerId);
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
                // _onUpdateViewAction?.Invoke(_towerRuneComposites[0].TowerId);
            }
        }
    }
}
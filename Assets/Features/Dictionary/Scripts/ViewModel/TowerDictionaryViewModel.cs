using Common.Scripts;
using Features.Dictionary.Scripts.View;
using GamePlay.Scripts.Character.Stats;
using GamePlay.Scripts.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Features.Dictionary.Scripts.ViewModel
{
    public class TowerDictionaryViewModel : DictionaryViewModelBase
    {
        [SerializeField] private List<UnitId.Tower> _baseTowerIds;
        [SerializeField] private TowerDataConfigBase _towerDataConfigBase;
        [SerializeField] private ListButtonDictionaryViewModel _listButtonDictionaryViewModel;
        [SerializeField] private UnitDictionaryDetailViewModel _unitDictionaryDetailViewModel;

        [SerializeField] private TowerButtonPreviewViewModel _towerButtonPreviewView;

        private NextUpgradeTreeHandle _nextUpgradeTreeHandle;
        private UnitDataComposite _unitDataComposite;
        
        public override void SetUp()
        {
            base.SetUp();
            PrepareListButtonData();
            if (_baseTowerIds.Count > 0)
            {
                _nextUpgradeTreeHandle = new NextUpgradeTreeHandle(_baseTowerIds[0], _towerDataConfigBase);
                SetupButtonPreview();
            }
        }
        private void SetupButtonPreview()
        {
            _towerButtonPreviewView.Setup(
                _nextUpgradeTreeHandle.IsExistLeftId() ? ShowLeftUnit : null,
                _nextUpgradeTreeHandle.IsExistRightId() ? ShowRightUnit : null);
        }
        private void PrepareListButtonData()
        {
            List<ButtonDictionaryComposite> listButtonDictionaryComposites = new List<ButtonDictionaryComposite>();

            foreach (UnitDataComposite unitDataComposite in _towerDataConfigBase.GetConfigsByKeys(_baseTowerIds))
            {
                listButtonDictionaryComposites.Add(
                    new ButtonDictionaryComposite
                    {
                        UnitId = unitDataComposite.UnitBase.UnitId,
                        Name = unitDataComposite.UnitBase.UnitStatsHandlerComp().GetBaseStats().GetInformation(InformationId.Name),
                    });
            }
            _listButtonDictionaryViewModel.SetUpButton(listButtonDictionaryComposites, OnShowUnitInformation);

        }
        private void ShowLeftUnit()
        {
            OnShowUnitInformation(_nextUpgradeTreeHandle.GetLeftId());
            SetupButtonPreview();
        }
        private void ShowRightUnit()
        {
            OnShowUnitInformation(_nextUpgradeTreeHandle.GetRightId());
            SetupButtonPreview();
        }
        private void OnShowUnitInformation(UnitId.Tower unitId)
        {
            _unitDataComposite = _towerDataConfigBase.GetConfigByKey(unitId);
            _unitDictionaryDetailViewModel.SetUp(_unitDataComposite);
        }
        private void OnShowUnitInformation(string unitId)
        {
            if (!Enum.TryParse(unitId, out UnitId.Tower towerKey))
                return;
            _unitDataComposite = _towerDataConfigBase.GetConfigByKey(towerKey);
            _unitDictionaryDetailViewModel.SetUp(_unitDataComposite);
        }
    }

    public class NextUpgradeTreeHandle
    {
        private readonly List<UnitId.Tower> _curTree;
        private UnitId.Tower _curNode;

        public NextUpgradeTreeHandle(UnitId.Tower curNode, TowerDataConfigBase towerDataConfigBase)
        {
            _curTree = towerDataConfigBase.NextAvailableUpgradeTowers.GetAllNextAvailableUpgradeTowers(curNode);
        }
        public bool IsExistLeftId()
        {
            return _curTree.FindIndex((tower) => tower == _curNode) - 1 >= 0;
        }
        public bool IsExistRightId()
        {
            return _curTree.FindIndex((tower) => tower == _curNode) + 1 < _curTree.Count;
        }
        public UnitId.Tower GetLeftId()
        {
            _curNode = _curTree[_curTree.FindIndex((tower) => tower == _curNode) - 1];
            return _curNode;
        }
        public UnitId.Tower GetRightId()
        {
            _curNode = _curTree[_curTree.FindIndex((tower) => tower == _curNode) + 1];
            return _curNode;
        }
    }
}

using Common.Scripts;
using Features.Dictionary.Scripts.View;
using GamePlay.Scripts.Character.Stats;
using GamePlay.Scripts.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Features.Dictionary.Scripts.ViewModel
{
    public class EnemyDictionaryViewModel : DictionaryViewModelBase
    {
        [SerializeField] private EnemyDataConfigBase _enemyDataConfigBase;
        [SerializeField] private ListButtonDictionaryViewModel _listButtonDictionaryViewModel;
        [SerializeField] private UnitDictionaryDetailViewModel _unitDictionaryDetailViewModel;
        public override void SetUp()
        {
            base.SetUp();
            PrepareListButtonData();
        }
        private void PrepareListButtonData()
        {
            List<ButtonDictionaryComposite> listButtonDictionaryComposites = new List<ButtonDictionaryComposite>();

            foreach (UnitDataComposite unitDataComposite in _enemyDataConfigBase.GetListItem())
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
        private void OnShowUnitInformation(string unitId)
        {
            if (Enum.TryParse(unitId, out UnitId.Enemy key))
            {
                _unitDictionaryDetailViewModel.SetUp(_enemyDataConfigBase.GetConfigByKey(key).UnitBase);
            }
        }
    }
}

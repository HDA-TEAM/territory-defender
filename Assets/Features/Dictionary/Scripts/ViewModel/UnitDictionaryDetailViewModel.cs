using Features.Dictionary.Scripts.View;
using GamePlay.Scripts.Character.Stats;
using System.Collections.Generic;
using UnityEngine;

namespace Features.Dictionary.Scripts.ViewModel
{
    public class UnitDictionaryDetailViewModel : MonoBehaviour
    {
        [SerializeField] private List<StatId> _listStatCanBeShow;
        [SerializeField] private UnitDictionaryDetailView _dictionaryDetailView;

        public void SetUp(UnitBase unitBase)
        {
            PrepareInformation(unitBase);
        }
        private void PrepareInformation(UnitBase unitBase)
        {
            List<UnitDictionaryStatComposite> unitDictionaryStatComposites = new List<UnitDictionaryStatComposite>();
            Stats stats = unitBase.UnitStatsHandlerComp().GetBaseStats();
            
            foreach (var statsComposite in stats.GetListStat())
            {
                if (_listStatCanBeShow.Contains(statsComposite.StatId))
                {
                    unitDictionaryStatComposites.Add(new UnitDictionaryStatComposite
                    {
                        StatName = statsComposite.StatId.ToString(),
                        StatVal = statsComposite.StatVal.ToString(),
                    });
                }
            }
            UnitDictionaryDetailComposite unitDictionaryDetailComposite = new UnitDictionaryDetailComposite
            {
                UnitDictionaryStatComposites = unitDictionaryStatComposites,
                Name = stats.GetInformation(InformationId.Name),
            };
            _dictionaryDetailView.SetUp(unitDictionaryDetailComposite);
        }
    }
}

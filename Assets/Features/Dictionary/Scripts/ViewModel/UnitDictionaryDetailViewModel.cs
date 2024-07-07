using Features.Dictionary.Scripts.View;
using GamePlay.Scripts.Character.Stats;
using GamePlay.Scripts.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Features.Dictionary.Scripts.ViewModel
{
    public class UnitDictionaryDetailViewModel : MonoBehaviour
    {
        [SerializeField] private List<StatId> _listStatCanBeShow;
        [SerializeField] private UnitDictionaryDetailView _dictionaryDetailView;

        public void SetUp(UnitDataComposite unitDataComposite)
        {
            PrepareInformation(unitDataComposite);
        }
        private void PrepareInformation(UnitDataComposite unitDataComposite)
        {
            List<UnitDictionaryStatComposite> unitDictionaryStatComposites = new List<UnitDictionaryStatComposite>();
            Stats stats = unitDataComposite.UnitBase.UnitStatsHandlerComp().GetBaseStats();
            
            foreach (StatsComposite statsComposite in stats.GetListStat())
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
                Avatar = unitDataComposite.UnitSprites.AvatarFull,
                Intro = stats.GetInformation(InformationId.Description),
            };
            
            _dictionaryDetailView.SetUp(unitDictionaryDetailComposite);
        }
    }
}

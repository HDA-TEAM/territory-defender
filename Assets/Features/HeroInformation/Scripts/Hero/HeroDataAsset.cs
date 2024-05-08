using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Common.Scripts;
using UnityEngine;

namespace Features.HeroInformation
{
    [Serializable]
    public struct HeroDataModel : IDefaultDataModel
    {
        public List<HeroDataSO> ListHero;
        public bool IsEmpty()
        {
            return false;
        }
        public void SetDefault()
        {
            ListHero = new List<HeroDataSO>();
        }
    }
    
    [CreateAssetMenu(fileName = "HeroDataAsset", menuName = "ScriptableObject/DataAsset/HeroDataAsset")]
    public class HeroDataAsset : BaseDataAsset<HeroDataModel>
    {
        [SerializedDictionary("HeroId", "HeroDataSO")]
        [SerializeField] private SerializedDictionary<UnitId.Hero, HeroDataSO> _heroDataDict = new SerializedDictionary<UnitId.Hero, HeroDataSO>();
        public HeroDataSO GetHeroDataWithId(UnitId.Hero heroId)
        {
            _heroDataDict.TryGetValue(heroId, out HeroDataSO heroDataSo);
            return heroDataSo;
        }
        public List<HeroDataSO> GetAllHeroData()
        {
            return _heroDataDict.Values.ToList();
        }
        
        // TODO: something be needed
    }
}

using AYellowpaper.SerializedCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common.Scripts.Data.DataAsset
{
    [Serializable]
    public struct HeroLevel
    {
        public int Level;
        public UnitId.Hero HeroId;
    }
    [Serializable]
    public struct HeroDataModel : IDefaultDataModel
    {
        public List<HeroDataSO> ListHero;
        public List<HeroLevel> HeroLevels;
        public bool IsEmpty()
        {
            return HeroLevels == null || HeroLevels.Count <= 0;
        }
        public void SetDefault()
        {
            ListHero = new List<HeroDataSO>();
            HeroLevels = new List<HeroLevel>
            {
                new HeroLevel
                {
                    HeroId = UnitId.Hero.TrungNhi,
                    Level = 1,
                },
                new HeroLevel
                {
                    HeroId = UnitId.Hero.TrungTrac,
                    Level = 1,
                },
            };
        }
    }
    
    [CreateAssetMenu(fileName = "HeroDataAsset", menuName = "ScriptableObject/DataAsset/HeroDataAsset")]
    public class HeroDataAsset : LocalDataAsset<HeroDataModel>
    {
        [SerializedDictionary("HeroId", "HeroDataSO")]
        [SerializeField] private SerializedDictionary<UnitId.Hero, HeroDataSO> _heroDataDict = new SerializedDictionary<UnitId.Hero, HeroDataSO>();

        public List<HeroLevel> HeroLevels
        {
            get
            {
                if (_model.HeroLevels == null || _model.HeroLevels.Count <= 0)
                {
                    _model.HeroLevels = new List<HeroLevel>();
                    _model.HeroLevels.Add(new HeroLevel
                    {
                        HeroId = UnitId.Hero.TrungNhi,
                        Level = 1,
                    });
                    _model.HeroLevels.Add(new HeroLevel
                    {
                        HeroId = UnitId.Hero.TrungTrac,
                        Level = 1,
                    });
                }
                
                return _model.HeroLevels;
            }
        }
        public HeroDataSO GetHeroDataWithId(UnitId.Hero heroId)
        {
            _heroDataDict.TryGetValue(heroId, out HeroDataSO heroDataSo);
            return heroDataSo;
        }
        public List<HeroDataSO> GetAllHeroData()
        {
            return _heroDataDict.Values.ToList();
        }
        public int GetHeroLevel(UnitId.Hero heroId)
        {
            return HeroLevels.Find(heroLevel => heroLevel.HeroId == heroId).Level;
        }
        public void UpgradeHeroLevel(UnitId.Hero heroId)
        {
            HeroLevel curHeroLevel = HeroLevels.Find(heroLevel => heroLevel.HeroId == heroId);
            _model.HeroLevels.Remove(curHeroLevel);
            _model.HeroLevels.Add(new HeroLevel
            {
                HeroId = heroId,
                Level = curHeroLevel.Level + 1,
            });
            SaveData();
        }
    }
}

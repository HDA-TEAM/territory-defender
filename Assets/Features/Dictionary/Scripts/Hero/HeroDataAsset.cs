using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum EHeroId
{
    TrungTrac = 1,
    TrungNhi = 2,
    ToDinh = 3,
    NgoQuyen = 4,
    QuangTrung = 5,
    MaVien = 6,
    TrungTrac2 = 7
}

[CreateAssetMenu(fileName = "HeroDataAsset", menuName = "ScriptableObject/DataAsset/HeroDataAsset")]
public class HeroDataAsset : ScriptableObject
{
    [SerializedDictionary("HeroId", "HeroDataSO")]
    [SerializeField] private SerializedDictionary<EHeroId, HeroDataSO> _heroDataDict = new SerializedDictionary<EHeroId, HeroDataSO>();
    public HeroDataSO GetHeroDataWithId(EHeroId heroId)
    {
        _heroDataDict.TryGetValue(heroId, out HeroDataSO heroDataSo);
        return heroDataSo;
    }
    public List<HeroDataSO> GetAllHeroData()
    {
        return _heroDataDict.Values.ToList();
    }
}

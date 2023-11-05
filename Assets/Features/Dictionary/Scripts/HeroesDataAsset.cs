using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum EHeroId
{
    TrungTrac = 1,
    TrungNhi = 2,
}

[CreateAssetMenu(fileName = "HeroesDataAsset", menuName = "ScriptableObject/DataAsset/HeroesDataAsset")]
public class HeroesDataAsset : ScriptableObject
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

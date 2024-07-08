using System;
using AYellowpaper.SerializedCollections;
using Common.Scripts.Data.DataConfig;
using System.Collections.Generic;
using System.Linq;
using Common.Scripts.Data.DataAsset;
using UnityEngine;

namespace Common.Scripts.Data.DataAsset
{
    public enum RuneId
    {
        ArmorPiercingRune = 1,
        AttackRune = 2,
        CriticalRune = 3,
        StunRune = 4,
    
        AttackSpeedRune = 10,
        AttackRangeRune = 11,
        UnitProductionRune = 12,
        ChainStrikeRune = 13,
    
        HealthRune = 20,
        ArmorRune = 21,
        GoldRune = 22,
        EvasionRune = 23,
    }

    [CreateAssetMenu(fileName = "RuneDataAsset", menuName = "ScriptableObject/DataAsset/RuneDataAsset")]
    public class RuneDataAsset : LocalDataAsset<RuneDataModel>
    {
        [SerializedDictionary("RuneId", "RuneDataSO")] 
        public SerializedDictionary<RuneId, RuneDataSo> _masteryPageDataDict = new SerializedDictionary<RuneId, RuneDataSo>();

        public RuneDataSo GetRune(RuneId runeId)
        {
            if (_masteryPageDataDict.TryGetValue(runeId, out RuneDataSo runeDataSo))
                return runeDataSo;
            
            Debug.LogError($"No rune value found for key {runeId} on ");
            return null;
        }
        public List<RuneDataSo> GetAllRuneData()
        {
            return _masteryPageDataDict.Values.ToList();
        }
        
        public List<RuneData> GetAllRuneDataAsRuneData()
        {
            return _masteryPageDataDict.Values.Select(runeDataSo => new RuneData
            {
                RuneId = runeDataSo.GetRuneId(),
                Level = 0 // Assuming you want to set default level to 0
            }).ToList();
        }
    }
}

[Serializable]
public struct RuneDataModel : IDefaultDataModel
{
    public List<RuneData> ListRuneDatas;

    public bool IsEmpty()
    {
        return (ListRuneDatas == null || ListRuneDatas.Count == 0);
    }

    public void SetDefault()
    {
        
    }
}

[Serializable]
public struct RuneData
{
    public RuneId RuneId;
    public int Level;

    public RuneData(RuneId runeId, int level)
    {
        RuneId = runeId;
        Level = level;
    }
}

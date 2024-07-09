using System;
using Common.Scripts.Data.DataAsset;
using UnityEngine;

namespace Common.Scripts.Data.DataAsset
{
    public enum RuneId
    {
        None = 0,
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
    }
}

[Serializable]
public struct RuneDataModel : IDefaultDataModel
{

    public bool IsEmpty()
    {
        return false;
    }

    public void SetDefault()
    {
        
    }
}

[Serializable]
public struct RuneData : IDefaultDataModel
{
    public RuneId RuneId;
    public int Level;

    public RuneData(RuneId runeId, int level)
    {
        RuneId = runeId;
        Level = level;
    }
    public bool IsEmpty()
    {
        return RuneId == RuneId.None;
    }
    public void SetDefault()
    {
        RuneId = RuneId.None;
        Level = -1;
    }
}

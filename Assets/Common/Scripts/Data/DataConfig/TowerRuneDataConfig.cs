using System;
using System.Collections.Generic;
using Common.Scripts.Data.DataAsset;
using Features.MasteryPage.Scripts.Rune;
using Newtonsoft.Json;
using UnityEngine;

namespace Common.Scripts.Data.DataConfig
{
    [CreateAssetMenu(fileName = "TowerRuneDataConfig", menuName = "ScriptableObject/Configs/TowerRuneDataConfig")]
    public class TowerRuneDataConfig : DataConfigBase<UnitId.Tower, List<RuneId>>
    {
        // [SerializeField] private AvailableRunesUsage _availableRunesUsage;
        // public AvailableRunesUsage AvailableRunesUsage
        // {
        //     get
        //     {
        //         return _availableRunesUsage;
        //     }
        // }
    }
    
    // [Serializable]
    // public struct RuneDataComposite
    // {
    //     [SerializeField]
    //     private RuneId _runeId;
    //
    //     public string _name;
    //     public int _maxLevel;
    //     [JsonIgnore]
    //     public Sprite _avatarSelected;
    //     [JsonIgnore]
    //     public Sprite _avatarStarted;
    //
    //     public List<EffectId> _effects;
    //     public RuneId GetRuneId()
    //     {
    //         return _runeId;
    //     }
    // }
}

using System.Collections.Generic;
using Common.Scripts.Data.DataAsset;
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
}

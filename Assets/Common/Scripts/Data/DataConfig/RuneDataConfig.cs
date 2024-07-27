using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using Common.Scripts.Data.DataAsset;
using UnityEngine;

namespace Common.Scripts.Data.DataConfig
{
    [CreateAssetMenu(fileName = "RuneDataConfig", menuName = "ScriptableObject/Configs/RuneDataConfig")]
    public class RuneDataConfig : DataConfigBase<RuneId, RuneDataSo>
    {
        [SerializeField] private List<int> _necessaryToUpgradeStars;
        public RuneDataSo GetRune(RuneId runeId)
        {
            if (_data.TryGetValue(runeId, out RuneDataSo runeDataSo))
                return runeDataSo;
            
            Debug.LogError($"No rune value found for key {runeId} on ");
            return null;
        }
        public int GetUpgradeStar(int level)
        {
            if (_necessaryToUpgradeStars != null && level <= _necessaryToUpgradeStars.Count && level > 0)
            {
                return _necessaryToUpgradeStars[level - 1];
            }
            return -1;
        }
        public int GetReturnStar(int level)
        {
            int totalStar = 0;
            if (_necessaryToUpgradeStars == null)
                return 0;
            while (level <= _necessaryToUpgradeStars.Count && level > 0)
            {
                totalStar += _necessaryToUpgradeStars[level - 1];
                level--;
            }
            return totalStar;
        }
    }
}

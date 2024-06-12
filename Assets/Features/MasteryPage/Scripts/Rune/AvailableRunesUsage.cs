using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Common.Scripts.Data.DataAsset;
using UnityEngine;

namespace Features.MasteryPage.Scripts.Rune
{
    public class AvailableRunesUsage
    {
        [SerializeField] [SerializedDictionary("RuneID", "AvailableRunesUsage")]
        private SerializedDictionary<RuneId, List<RuneId>> _availableRunesUsage;

        public List<RuneId> GetRunesAvailableUsage(RuneId runeId)
        {
            _availableRunesUsage.TryGetValue(runeId, out List<RuneId> availableRunesUsage);
            return availableRunesUsage;
        }
    }
}

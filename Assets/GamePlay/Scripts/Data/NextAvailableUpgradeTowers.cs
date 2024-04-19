using AYellowpaper.SerializedCollections;
using Common.Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Scripts.Data
{
    [Serializable]
    public class NextAvailableUpgradeTowers
    {
        [SerializeField] [SerializedDictionary("UnitId.Tower","AvailableUpgradeTowers")]
        private SerializedDictionary<UnitId.Tower,List<UnitId.Tower>> _availableUpgradeTowers;
        public List<UnitId.Tower> GetNextAvailableUpgradeTowers(UnitId.Tower towerId)
        {
            _availableUpgradeTowers.TryGetValue(towerId, out List<UnitId.Tower> availableUpgradeNextTowers);
            return availableUpgradeNextTowers;
        }
    }
}

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
        public List<UnitId.Tower> GetSingleNextAvailableUpgradeTowers(UnitId.Tower towerId)
        {
            _availableUpgradeTowers.TryGetValue(towerId, out List<UnitId.Tower> availableUpgradeNextTowers);
            return availableUpgradeNextTowers;
        }
        public List<UnitId.Tower> GetAllNextAvailableUpgradeTowers(UnitId.Tower towerId)
        {
            List<UnitId.Tower> res = new List<UnitId.Tower>();
            while (_availableUpgradeTowers.TryGetValue(towerId, out List<UnitId.Tower> availableUpgradeNextTowers))
            {
                foreach (var tower in availableUpgradeNextTowers)
                {
                    if (!res.Contains(tower))
                    {
                        res.Add(tower);
                    }
                }
            }
            return res;
        }
    }
}

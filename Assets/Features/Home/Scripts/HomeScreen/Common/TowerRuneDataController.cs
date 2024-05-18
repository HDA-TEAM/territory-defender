using System;
using System.Collections.Generic;
using System.Linq;
using Common.Scripts;
using Features.MasteryPage.Scripts.Rune;
using Features.MasteryPage.Scripts.Tower;
using UnityEngine;

namespace Features.Home.Scripts.HomeScreen.Common
{
    public class TowerRuneDataController : MonoBehaviour
    {
        [Header("Data"), Space(12)]
        [SerializeField] private TowerRuneDataAsset _towerRuneDataAsset;
        
        public RuneDataAsset _runeDataAsset;
        public List<TowerRuneComposite> TowerRuneComposites { get; private set; }
        private List<RuneComposite> _runeComposites;
        
        private ITowerRune _currentStrategy;

        public void SetStrategy(ITowerRune strategy)
        {
            _currentStrategy = strategy;
        }

        public void ExecuteStrategy()
        {
            _currentStrategy?.Execute(this);
        }
        public int GetReturnStar()
        {
            return _towerRuneDataAsset._returnStar;
        }
        public void InitializeTowerRuneData()
        {
            //Check and setup for Towers which had the rune
            if (TowerRuneComposites == null)
                TowerRuneComposites = new List<TowerRuneComposite>();
            else TowerRuneComposites.Clear();
            
            //Check and setup Runes
            if (_runeComposites == null)
                _runeComposites = new List<RuneComposite>();
            else _runeComposites.Clear();
            
            // Load Rune data into each Tower
            List<RuneDataConfig> listRuneSos = _runeDataAsset.GetAllRuneData();
            List<TowerDataConfig> towerDataConfigs = _towerRuneDataAsset.GetAllTowerDataConfig();
            List<TowerDataSaver> loadedTowerData = _towerRuneDataAsset._towerDataAsset.GetTowers();
            
            foreach (var towerSo in towerDataConfigs)
            {
                _runeComposites = new List<RuneComposite>();
                // if (towerSo._runeLevels == null)
                // {
                    // Include default data for each rune
                    foreach (var runeSo in listRuneSos)
                    {
                        _runeComposites.Add(new RuneComposite
                        {
                            RuneId = runeSo.GetRuneId(),
                            Name = runeSo._name,
                            Level = 0,
                            MaxLevel = runeSo._maxLevel,
                            AvatarSelected = runeSo._avatarSelected,
                            AvatarStarted = runeSo._avatarStarted,
                            Effects = runeSo._effects,
                        });
                    }
                //}
                
                var newTowerData = loadedTowerData.Find(obj => towerSo.GetTowerId() == obj.TowerId);
                if (newTowerData.RuneLevels != null) // Check if json is new created or null
                {
                    // Find the corresponding TowerSoSaver in loadedTowerData
                    int towerSoSaverIndex = loadedTowerData.FindIndex(t => t.TowerId == towerSo.GetTowerId());
                    if (towerSoSaverIndex != -1)
                    {
                        TowerDataSaver towerDataSaver = loadedTowerData[towerSoSaverIndex];
                        foreach (var runeLevel in towerDataSaver.RuneLevels)
                        {
                            int runeCompositeIndex = _runeComposites.FindIndex(rc => rc.RuneId == runeLevel.RuneId);
                            if (runeCompositeIndex != -1)
                            {
                                RuneComposite temp = _runeComposites[runeCompositeIndex];
                                temp.Level = runeLevel.Level;
                                _runeComposites[runeCompositeIndex] = temp;
                            }
                        }
                    }
                }
                TowerRuneComposites.Add(new TowerRuneComposite
                {
                    TowerId = towerSo.GetTowerId(),
                    RuneComposite = _runeComposites
                });
            }
        }
        public void UpgradeTowerRuneData(UnitId.Tower towerId, RuneComposite runeComposite)
        {
            _towerRuneDataAsset._towerTypeDict.TryGetValue(towerId, out TowerDataConfig curTower);
            if (!curTower)
            {
                Debug.LogError("Tower type not exist in dictionary");
            }
            else
            {
                RuneLevel runeLevel = new RuneLevel(runeComposite.RuneId, runeComposite.Level);
                int index = curTower._runeLevels.FindIndex(r => r.RuneId == runeComposite.RuneId);
                if (index != -1)
                {
                    // RuneId exists, update the rune
                    UpgradeRune(curTower, index);
                }
                else
                {
                    // RuneId does not exist, add a new rune
                    AddRune(curTower, runeLevel);
                }

                _towerRuneDataAsset._towerDataAsset.SaveTowers(_towerRuneDataAsset._towerTypeDict);
            }
        }
        private void AddRune(TowerDataConfig towerDataConfig, RuneLevel runeLevel)
        {
            if (towerDataConfig._runeLevels == null)
            {
                Debug.Log("First time......"); // No join
                towerDataConfig._runeLevels = new List<RuneLevel>();
            }

            // Set the level of the rune to 1 regardless of its current level
            RuneLevel newRune = new RuneLevel
            {
                RuneId = runeLevel.RuneId,
                Level = 1 // Set level to 1 for the new rune
            };
            towerDataConfig._runeLevels.Add(newRune);

            // foreach (var item in towerDataConfig._runeLevels)
            // {
            //     Debug.Log("ID: "+ item.RuneId + "..." + item.Level);
            // }
            // Optionally, sort the RuneLevels list by RuneId
            towerDataConfig._runeLevels.Sort((a, b) => a.RuneId.CompareTo(b.RuneId));
        }

        private void UpgradeRune(TowerDataConfig towerDataConfig, int index)
        {
            if (towerDataConfig._runeLevels == null || index < 0 || index >= towerDataConfig._runeLevels.Count)
            {
                Debug.LogError("Invalid index or RuneLevels is not initialized.");
                return;
            }

            // Increment the level of the existing rune
            RuneLevel currentRuneLevel = towerDataConfig._runeLevels[index];
            currentRuneLevel.Level++;  // Increment the level by 1
            towerDataConfig._runeLevels[index] = currentRuneLevel;
        }

        public void ResetTowerRuneData(UnitId.Tower towerId)
        {
            // Attempt to get the tower configuration
            if (!_towerRuneDataAsset._towerTypeDict.TryGetValue(towerId, out TowerDataConfig towerDataConfig))
            {
                Debug.LogError("Tower type not exist in dictionary for reset");
                return;
            }

            _towerRuneDataAsset._returnStar = 0;
        
            for (int i = 0; i < towerDataConfig._runeLevels.Count; i++)
            {
                RuneLevel rune = towerDataConfig._runeLevels[i];
                if (towerDataConfig._runeLevels[i].Level > 0)
                {
                    _towerRuneDataAsset._returnStar += rune.Level;
                    rune.Level = 0;
                    towerDataConfig._runeLevels[i] = rune;
                }
            }
        
            // Remove all runes with level
            towerDataConfig._runeLevels.RemoveAll(rune => rune.Level == 0);
        
            // Save changes to disk or server
            _towerRuneDataAsset._towerDataAsset.SaveTowers(_towerRuneDataAsset._towerTypeDict);
        }
    }
}

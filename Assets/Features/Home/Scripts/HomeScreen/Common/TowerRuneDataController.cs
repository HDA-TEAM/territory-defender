using System;
using System.Collections.Generic;
using System.Linq;
using Common.Scripts;
using Common.Scripts.Data.DataAsset;
using Common.Scripts.Data.DataConfig;
using Features.MasteryPage.Scripts.Rune;
using Features.MasteryPage.Scripts.Tower;
using UnityEngine;
using UnityEngine.Serialization;

namespace Features.Home.Scripts.HomeScreen.Common
{
    public class TowerRuneDataController : MonoBehaviour
    {
        [Header("Data"), Space(12)]
        [SerializeField] private TowerDataAsset _towerDataAsset;
        
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

        // private void Start()
        // {
        //     //_towerDataAsset.UpdateTowerDataConfig();
        //     
        //     SetStrategy(new InitTowerRuneStrategy());
        //     ExecuteStrategy();
        // }

        public int GetReturnStar()
        {
            return _towerDataAsset._returnStar;
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
            List<RuneDataSo> listRuneSos = _runeDataAsset.GetAllRuneData();
            List<TowerData> loadedTowerData = _towerDataAsset.TowerDatas;
            
            _towerDataAsset.UpdateTowerDataConfig();
            List<TowerDataConfig> towerDataConfig = _towerDataAsset.GetAllTowerData();
            if (towerDataConfig == null || towerDataConfig.Count == 0)
            {
                Debug.LogError("No tower data found in _towerDataAsset.");
                return;
            }
            
            
            
            // Data default is null
            if (loadedTowerData == null)
            {
                Debug.Log("Data default is null");
                // No local data, initialize from default configs
                foreach (var dataConfig in towerDataConfig)
                {
                    InitializeTowerWithDefaultRunes(dataConfig, listRuneSos);
                }
            }
            else
            {
                Debug.Log("Data default not null");
                // Local data exists, load from it
                foreach (var localData in loadedTowerData)
                {
                    if (_towerDataAsset._towerTypeDict.TryGetValue(localData.TowerId, out TowerDataConfig towerFind))
                    {
                        towerFind._runeLevels = new List<RuneLevelData>(localData.RuneLevels);
                        InitializeTowerWithLocalRunes(towerFind, listRuneSos);
                    }
                    else
                    {
                        Debug.LogWarning($"Tower with ID {localData.TowerId} not found in _towerTypeDict.");
                    }
                }
            }
        }
        private void InitializeTowerWithDefaultRunes(TowerDataConfig dataConfig, List<RuneDataSo> listRuneSos)
        {
            var runeComposites = new List<RuneComposite>();

            foreach (var runeSo in listRuneSos)
            {
                runeComposites.Add(new RuneComposite
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

            if (_towerDataAsset._towerTypeDict.TryGetValue(dataConfig.GetTowerId(), out TowerDataConfig tower))
            {
                tower._runeLevels = runeComposites.Select(rune => new RuneLevelData
                {
                    RuneId = rune.RuneId,
                    Level = rune.Level
                }).ToList();

                TowerRuneComposites.Add(new TowerRuneComposite
                {
                    TowerId = dataConfig.GetTowerId(),
                    RuneComposite = runeComposites
                });
            }
            else
            {
                Debug.LogWarning($"Tower with ID {dataConfig.GetTowerId()} not found in _towerTypeDict.");
            }
        }
        
        private void InitializeTowerWithLocalRunes(TowerDataConfig towerFind, List<RuneDataSo> listRuneSos)
        {
            var runeComposites = new List<RuneComposite>();

            foreach (var runeSo in listRuneSos)
            {
                int index = towerFind._runeLevels.FindIndex(r => r.RuneId == runeSo.GetRuneId());
                int level = 0;

                if (index != -1)
                {
                    // Retrieve the RuneLevelData struct, modify it, and assign it back
                    var runeLevelData = towerFind._runeLevels[index];
                    level = runeLevelData.Level;
                    runeLevelData.Level = level;
                    towerFind._runeLevels[index] = runeLevelData;
                }
                else
                {
                    // Add new RuneLevelData if it does not exist
                    towerFind._runeLevels.Add(new RuneLevelData(runeSo.GetRuneId(), level));
                }

                runeComposites.Add(new RuneComposite
                {
                    RuneId = runeSo.GetRuneId(),
                    Name = runeSo._name,
                    Level = level,
                    MaxLevel = runeSo._maxLevel,
                    AvatarSelected = runeSo._avatarSelected,
                    AvatarStarted = runeSo._avatarStarted,
                    Effects = runeSo._effects,
                });
            }

            // Update TowerRuneComposites
            TowerRuneComposites.Add(new TowerRuneComposite
            {
                TowerId = towerFind.GetTowerId(),
                RuneComposite = runeComposites
            });
        }

        public void UpgradeTowerRuneData(UnitId.Tower towerId, RuneComposite runeComposite)
        {
            _towerDataAsset._towerTypeDict.TryGetValue(towerId, out TowerDataConfig curTower);
            if (!curTower)
            {
                Debug.LogError("Tower type not exist in dictionary");
            }
            else
            {
                RuneLevelData runeLevelData = new RuneLevelData(runeComposite.RuneId, runeComposite.Level);
                int index = curTower._runeLevels.FindIndex(r => r.RuneId == runeComposite.RuneId);
                //Todo: addition more the condition for this logic update rune
                if (index != -1)
                {
                    // RuneId exists, update the rune
                    UpgradeRune(curTower, index);
                }
                // else
                // {
                //     // RuneId does not exist, add a new rune
                //     AddRune(curTower, runeLevelData);
                // }

                _towerDataAsset.SaveTowers(_towerDataAsset._towerTypeDict);
            }
        }
        private void AddRune(TowerDataConfig towerDataConfig, RuneLevelData runeLevelData)
        {
            if (towerDataConfig._runeLevels == null)
            {
                Debug.Log("First time......"); // No join
                towerDataConfig._runeLevels = new List<RuneLevelData>();
            }

            
            //Todo: Not setup again the rune data
            // Set the level of the rune to 1 regardless of its current level
            RuneLevelData newRune = new RuneLevelData
            {
                RuneId = runeLevelData.RuneId,
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
            RuneLevelData currentRuneLevelData = towerDataConfig._runeLevels[index];
            currentRuneLevelData.Level++;  // Increment the level by 1
            towerDataConfig._runeLevels[index] = currentRuneLevelData;
        }

        public void ResetTowerRuneData(UnitId.Tower towerId)
        {
            // Attempt to get the tower configuration
            if (!_towerDataAsset._towerTypeDict.TryGetValue(towerId, out TowerDataConfig towerDataConfig))
            {
                Debug.LogError("Tower type not exist in dictionary for reset");
                return;
            }

            _towerDataAsset._returnStar = 0;
        
            for (int i = 0; i < towerDataConfig._runeLevels.Count; i++)
            {
                RuneLevelData rune = towerDataConfig._runeLevels[i];
                if (towerDataConfig._runeLevels[i].Level > 0)
                {
                    _towerDataAsset._returnStar += rune.Level;
                    rune.Level = 0;
                    towerDataConfig._runeLevels[i] = rune;
                }
            }
        
            // Remove all runes with level
            towerDataConfig._runeLevels.RemoveAll(rune => rune.Level == 0);
        
            // Save changes to disk or server
            _towerDataAsset.SaveTowers(_towerDataAsset._towerTypeDict);
        }
    }
}

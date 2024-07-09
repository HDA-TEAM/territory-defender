using System.Collections.Generic;
using Common.Scripts;
using Common.Scripts.Data;
using Common.Scripts.Data.DataAsset;
using Common.Scripts.Data.DataConfig;
using Features.MasteryPage.Scripts.Rune;
using Features.MasteryPage.Scripts.Tower;
using UnityEngine;

namespace Features.Home.Scripts.HomeScreen.Common
{
    public class TowerRuneDataController : MonoBehaviour
    {
        [Header("Data"), Space(12)]
        [SerializeField] private TowerDataAsset _towerDataAsset;
        [SerializeField] private RuneDataConfig _runeDataConfig;
        [SerializeField] private InventoryDataAsset _inventoryDataAsset;
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

            // Get data from local data
            InitializeTowerWithLocalRunes(_towerDataAsset.TowerRuneDataList);

            // // Load Rune data into each Tower
            // _towerDataAsset.UpdateTowerDataConfig(); //TODO
            //
            // List<RuneDataSo> listRuneSos = _runeDataAsset.GetAllRuneData();
            // List<TowerDataSo> towerDataConfig = _towerDataAsset.GetAllTowerData();
            // List<TowerRuneData> loadedTowerData = _towerDataAsset.TowerDatas;
            //
            // if (towerDataConfig == null || towerDataConfig.Count == 0)
            // {
            //     Debug.LogError("No tower data found in _towerDataAsset.");
            //     return;
            // }

            // // Data in local is null
            // if (loadedTowerData.Count == 0)
            // {
            //     // No local data, initialize from default configs
            //     
            //     foreach (var itemTowerConfig in towerDataConfig)
            //     {
            //         InitializeTowerWithDefaultRunes(itemTowerConfig, _towerRuneDataConfig.GetConfigByKey(itemTowerConfig.GetTowerId()));
            //     }
            // } else {
            //     // Local data exists, load from it
            //     foreach (var localData in loadedTowerData)
            //     {
            //         if (_towerDataAsset._towerTypeDict.TryGetValue(localData.TowerId, out TowerDataSo towerFind))
            //         {
            //             var listRuneConfig = _towerRuneDataConfig.GetConfigByKey(localData.TowerId);
            //             towerFind._runeLevels = new List<RuneData>(localData.RuneLevels);
            //             InitializeTowerWithLocalRunes(towerFind, listRuneSos, listRuneConfig);
            //         } else {
            //             Debug.LogWarning($"Tower with ID {localData.TowerId} not found in _towerTypeDict.");
            //         }
            //     }
            // }
        }
        // private void InitializeTowerWithDefaultRunes(TowerDataSo dataSo, List<RuneId> runeIds)
        // {
        //     var runeComposites = new List<RuneComposite>();
        //     var listRune = _runeDataAsset._masteryPageDataDict;
        //     
        //     foreach (var runeId in runeIds)
        //     {
        //         if (listRune.TryGetValue(runeId, out RuneDataSo value) && value != null)
        //         {
        //             runeComposites.Add(new RuneComposite
        //             {
        //                 RuneId = value.GetRuneId(),
        //                 Name = value._name,
        //                 Description = value.Description,
        //                 Level = 0,
        //                 MaxLevel = value._maxLevel,
        //                 AvatarSelected = value._avatarSelected,
        //                 AvatarStarted = value._avatarStarted,
        //                 PowerUnits = value.PowerUnits,
        //                 Effects = value._effects,
        //             });
        //         }
        //     }
        //
        //     if (_towerDataAsset._towerTypeDict.TryGetValue(dataSo.GetTowerId(), out TowerDataSo tower))
        //     {
        //         tower._runeLevels = runeComposites.Select(rune => new RuneData
        //         {
        //             RuneId = rune.RuneId,
        //             Level = rune.Level
        //         }).ToList();
        //
        //         TowerRuneComposites.Add(new TowerRuneComposite
        //         {
        //             TowerId = dataSo.GetTowerId(),
        //             RuneComposite = runeComposites
        //         });
        //     }
        //     else
        //     {
        //         Debug.LogWarning($"Tower with ID {dataSo.GetTowerId()} not found in _towerTypeDict.");
        //     }
        // }

        private void InitializeTowerWithLocalRunes(List<TowerRuneData> towerRuneDataList)
        {
            // towerIsFound._runeLevels.Sort((rune1, rune2) => 
            // {
            //     int index1 = runeIds.IndexOf(rune1.RuneId);
            //     int index2 = runeIds.IndexOf(rune2.RuneId);
            //     return index1.CompareTo(index2);
            // });

            // Create a list to store the indices of runes that need to be modified
            // Iterate over the local tower's rune data
            foreach (TowerRuneData singleTowerRuneData in towerRuneDataList)
            {
                List<RuneComposite> runeComposites = new List<RuneComposite>();
                foreach (RuneData runeData in singleTowerRuneData.RuneLevels)
                {
                    RuneDataSo runeSo = _runeDataConfig.GetConfigByKey(runeData.RuneId);

                    runeComposites.Add(new RuneComposite
                    {
                        RuneId = runeSo.GetRuneId(),
                        Name = runeSo._name,
                        Description = runeSo.Description,
                        Level = runeData.Level,
                        MaxLevel = runeSo._maxLevel,
                        AvatarSelected = runeSo._avatarSelected,
                        AvatarStarted = runeSo._avatarStarted,
                        PowerUnits = runeSo.PowerUnits,
                        Effects = runeSo._effects,
                    });
                }
                // Update TowerRuneComposites
                TowerRuneComposites.Add(new TowerRuneComposite
                {
                    TowerId = singleTowerRuneData.TowerId,
                    RuneComposite = runeComposites
                });
            }

        }
        public void UpgradeTowerRuneData(UnitId.Tower towerId, RuneComposite runeComposite)
        {
            _towerDataAsset.CheckAndUpgradeTowerRuneLevel(towerId,runeComposite.RuneId,runeComposite.MaxLevel);
            // _towerDataAsset._towerTypeDict.TryGetValue(towerId, out TowerDataSo curTower);
            // if (!curTower)
            // {
            //     Debug.LogError("Tower type not exist in dictionary");
            // }
            // else
            // {
            //     //RuneData runeData = new RuneData(runeComposite.RuneId, runeComposite.Level);
            //     int index = curTower._runeLevels.FindIndex(r => r.RuneId == runeComposite.RuneId);
            //     if (index != -1)
            //     {
            //         // RuneId exists, update the rune
            //         UpgradeRune(curTower, index);
            //     }
            //
            //     _towerDataAsset.SaveTowers(_towerDataAsset._towerTypeDict);
            // }
        }
            // private void AddRune(TowerDataSo towerDataSo, RuneData runeData)
            // {
            //     if (towerDataSo._runeLevels == null)
            //     {
            //         Debug.Log("First time......"); // No join
            //         towerDataSo._runeLevels = new List<RuneData>();
            //     }
            //
            //     
            //     //Todo: Not setup again the rune data
            //     // Set the level of the rune to 1 regardless of its current level
            //     RuneData newRune = new RuneData
            //     {
            //         RuneId = runeData.RuneId,
            //         Level = 1 // Set level to 1 for the new rune
            //     };
            //     towerDataSo._runeLevels.Add(newRune);
            //
            //     // foreach (var item in towerDataConfig._runeLevels)
            //     // {
            //     //     Debug.Log("ID: "+ item.RuneId + "..." + item.Level);
            //     // }
            //     // Optionally, sort the RuneLevels list by RuneId
            //     //towerDataSo._runeLevels.Sort((a, b) => a.RuneId.CompareTo(b.RuneId));
            // }
        
            // private void UpgradeRune(TowerDataSo towerDataSo, int index)
            // {
            //     if (towerDataSo._runeLevels == null || index < 0 || index >= towerDataSo._runeLevels.Count)
            //     {
            //         Debug.LogError("Invalid index or RuneLevels is not initialized.");
            //         return;
            //     }
            //
            //     // Increment the level of the existing rune
            //     RuneData currentRuneData = towerDataSo._runeLevels[index];
            //     currentRuneData.Level++;  // Increment the level by 1
            //     towerDataSo._runeLevels[index] = currentRuneData;
            // }
            //
            public void ResetTowerRuneData(UnitId.Tower towerId)
            {
                _inventoryDataAsset.TryChangeInventoryData(InventoryType.TalentPoint, _towerDataAsset.GetReturnStar(towerId));
                _towerDataAsset.ResetSpecificTowerRuneData(towerId);
                //
                // // Attempt to get the tower configuration
                // if (!_towerDataAsset._towerTypeDict.TryGetValue(towerId, out TowerDataSo towerDataConfig))
                // {
                //     Debug.LogError("Tower type not exist in dictionary for reset");
                //     return;
                // }
                //
                // _towerDataAsset.ReturnStar = 0;
                //
                // for (int i = 0; i < towerDataConfig._runeLevels.Count; i++)
                // {
                //     RuneData rune = towerDataConfig._runeLevels[i];
                //     if (towerDataConfig._runeLevels[i].Level > 0)
                //     {
                //         _towerDataAsset.ReturnStar += rune.Level;
                //         rune.Level = 0;
                //         towerDataConfig._runeLevels[i] = rune;
                //     }
                // }
                //
                // // Remove all runes with level
                // //towerDataConfig._runeLevels.RemoveAll(rune => rune.Level == 0);
                //
                // // Save changes to disk or server
                // _towerDataAsset.SaveTowers(_towerDataAsset._towerTypeDict);
            }
        }
    }

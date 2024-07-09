using Common.Scripts.Data.DataConfig;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Scripts.Data.DataAsset
{
    [CreateAssetMenu(fileName = "TowerDataAsset", menuName = "ScriptableObject/DataAsset/TowerDataAsset")]
    public class TowerDataAsset : LocalDataAsset<TowerDataModel>
    {
        [SerializeField] private RuneDataConfig _runeDataConfig;
        [SerializeField] private TowerRuneDataConfig _towerRuneDataConfig;
        public List<TowerRuneData> TowerRuneDataList
        {
            // Load the data from json file into _model
            get
            {
                List<TowerRuneData> list = _model.ListTowerRuneDatas;
                if (list != null && list.Count > 0)
                    return _model.ListTowerRuneDatas;

                InitDefaultTowerRuneData();
                return _model.ListTowerRuneDatas;
            }
        }
        public bool CheckAndUpgradeTowerRuneLevel(UnitId.Tower towerId, RuneId runeId, int maxLevel)
        {
            TowerRuneData towerRuneData = TowerRuneDataList.Find(towerRunData => towerRunData.TowerId == towerId);
            int idx = towerRuneData.RuneLevels.FindIndex(runeDataFinder => runeDataFinder.RuneId == runeId);
            if (idx != -1)
            {
                RuneData newRuneData = towerRuneData.RuneLevels[idx];
                if (newRuneData.Level >= maxLevel)
                    return false;
                newRuneData.Level++;
                towerRuneData.RuneLevels[idx] = newRuneData;
                SaveData();
            }
            else
            {
                Debug.Log($"{name} :RuneId or towerId not exist");
                return false;
            }
            return true;
        }
        private void InitDefaultTowerRuneData()
        {
            _model.ListTowerRuneDatas = new List<TowerRuneData>();
            foreach (var keyVarPair in _towerRuneDataConfig.DataDict)
            {
                List<RuneData> listRuneData = new List<RuneData>();
                foreach (var runeId in keyVarPair.Value)
                {
                    listRuneData.Add(new RuneData
                    {
                        RuneId = runeId,
                        Level = 0,
                    });
                }
                _model.ListTowerRuneDatas.Add(new TowerRuneData
                {
                    TowerId = keyVarPair.Key,
                    RuneLevels = listRuneData,
                });
            }
            SaveData();
        }
        public int GetReturnStar(UnitId.Tower towerId)
        {
            int totalStar = 0;
            foreach (TowerRuneData towerRuneData in TowerRuneDataList)
            {
                if (towerId == towerRuneData.TowerId)
                {
                    foreach (RuneData runeData in towerRuneData.RuneLevels)
                    {
                        totalStar += _runeDataConfig.GetReturnStar(runeData.Level);
                    }
                    break;
                }
            }
            return totalStar;
        }
        public void ResetSpecificTowerRuneData(UnitId.Tower towerId)
        {
            _model.ListTowerRuneDatas.RemoveAll((towerRuneData) => towerRuneData.TowerId == towerId);

            List<RuneData> listRuneData = new List<RuneData>();
            foreach (RuneId runeId in _towerRuneDataConfig.DataDict[towerId])
            {
                listRuneData.Add(new RuneData
                {
                    RuneId = runeId,
                    Level = 0,
                });
            }
            _model.ListTowerRuneDatas.Add(new TowerRuneData
            {
                TowerId = towerId,
                RuneLevels = listRuneData,
            });
            SaveData();
        }

        // public TowerDataSo GetTower(UnitId.Tower towerId)
        // {
        //     _towerTypeDict.TryGetValue(towerId, out TowerDataSo tower);
        //     if (!tower)
        //     {
        //         Debug.LogError("Tower type not exist in dictionary");
        //         return _towerTypeDict[0];
        //     }
        //     return tower;
        // }
        //
        // public List<TowerDataSo> GetAllTowerData()
        // {
        //     return _towerTypeDict.Values.ToList();
        // }
        // private void LoadTowerDataFromLocalToTowerDict(List<TowerRuneData> towerDataSavers)
        // {
        //     foreach (var saver in towerDataSavers)
        //     {
        //         if (!_towerTypeDict.ContainsKey(saver.TowerId))
        //         {
        //             // Create a new TowerDataConfig instance and initialize it
        //             TowerDataSo towerDataSo = ScriptableObject.CreateInstance<TowerDataSo>();
        //             towerDataSo.InitializeRune(saver.RuneLevels);
        //             _towerTypeDict.Add(saver.TowerId, towerDataSo);
        //         }
        //     }
        // }
        // public void UpdateTowerDataConfig()
        // {
        //     // Load TowerDataConfig data from local
        //     var towerDatas = TowerDatas;
        //     
        //     LoadTowerDataFromLocalToTowerDict(towerDatas);
        // }
        //
        // New
        // public void SaveTowers(SerializedDictionary<UnitId.Tower, TowerDataSo> towerTypeDict)
        // {
        //     List<TowerRuneData> newTowerList = new List<TowerRuneData>(); // Create a new list for towers
        //     foreach (var kvp in towerTypeDict)
        //     {
        //         if (kvp.Value != null && kvp.Value._runeLevels is { Count: > 0 })
        //         {
        //             var towerSoSaver = new TowerRuneData
        //             {
        //                 TowerId = kvp.Key,
        //                 RuneLevels = kvp.Value._runeLevels
        //             };
        //             newTowerList.Add(towerSoSaver); // Add to the new list
        //         }
        //     }
        //     _model.ListTowerRuneDatas = newTowerList; // Update the model's TowerList only, without overwriting the entire model
        //     SaveData();
        // }
    }

    [Serializable]
    public struct TowerDataModel : IDefaultDataModel
    {
        public List<TowerRuneData> ListTowerRuneDatas;
        public bool IsEmpty()
        {
            return (ListTowerRuneDatas == null || ListTowerRuneDatas.Count == 0);
        }
        public void SetDefault()
        {
            // Ensure defaults are set for both lists
            ListTowerRuneDatas = new List<TowerRuneData>();
        }
    }

    [Serializable]
    public struct TowerRuneData
    {
        public UnitId.Tower TowerId;
        public List<RuneData> RuneLevels;
    }
}

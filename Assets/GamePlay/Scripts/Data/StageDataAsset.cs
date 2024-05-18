using Common.Scripts.Datas;
using CustomInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Features.StageInfo.Scripts.StageInfoView;
using UnityEngine;

namespace GamePlay.Scripts.Data
{
    [Serializable]
    public struct StagePassed
    {
        public int TotalStar;
        public StageId StageId;
    }

    [Serializable]
    public struct StageDataModel : IDefaultDataModel
    {
        public List<StagePassed> ListStagePassed;
        public bool IsEmpty()
        {
            return false;
        }
        public void SetDefault()
        {
            ListStagePassed = new List<StagePassed>();
        }
    }

    [CreateAssetMenu(fileName = "StageDataAsset", menuName = "ScriptableObject/Database/Stage/StageDataAsset")]
    public class StageDataAsset : BaseDataAsset<StageDataModel>
    {
        [SerializedDictionary("StageId", "StageDataSO")] 
        [SerializeField] private SerializedDictionary<StageId, StageDataSO> _stageDataDict = new SerializedDictionary<StageId, StageDataSO>();
        
        public List<StageDataSO> GetAllStageData()
        {
            return _stageDataDict.Values.ToList();
        }

        public int GetTotalStar()
        {
            int totalStar = 0;
            foreach (var item in _stageDataDict)
            {
                totalStar += item.Value._stageStar;
            }
            return totalStar;
        }
        public List<StagePassed> ListStagePassed
        {
            get
            {
                return _model.ListStagePassed ?? (_model.ListStagePassed = new List<StagePassed>());
            }
        }
#if UNITY_EDITOR
        [Button("AddStagePassed", usePropertyAsParameter: true)]
        [SerializeField] private StagePassed TestStagePassed;
#endif
        public void AddStagePassed(StagePassed newStagePassed)
        {
            StagePassed existStagePassed = ListStagePassed.Find((stage) => stage.StageId == newStagePassed.StageId);
            // not existStagePassed
            if (existStagePassed.TotalStar == 0)
                ListStagePassed.Add(newStagePassed);
            // Compare lager
            else if (existStagePassed.TotalStar < newStagePassed.TotalStar)
            {
                ListStagePassed.Remove(existStagePassed);
                ListStagePassed.Add(newStagePassed);
            }
            else
                return;
            SaveData();
        }
    }
}

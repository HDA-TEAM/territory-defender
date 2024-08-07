using CustomInspector;
using System;
using System.Collections.Generic;
using Common.Scripts.Data.DataAsset;
using UnityEngine;
using UnityEngine.Serialization;

namespace GamePlay.Scripts.Data
{
    [Serializable]
    public struct StageData
    {
        public int TotalStar;
        public StageId StageId;
    }

    [Serializable]
    public struct StageDataModel : IDefaultDataModel
    {
        [FormerlySerializedAs("ListStagePassed")] public List<StageData> StageDataList;
        public bool IsEmpty()
        {
            return false;
        }
        public void SetDefault()
        {
            StageDataList = new List<StageData>();
        }
    }

    [CreateAssetMenu(fileName = "StageDataAsset", menuName = "ScriptableObject/Database/Stage/StageDataAsset")]
    public class StageDataAsset : LocalDataAsset<StageDataModel>
    {
        [SerializeField] private StageDataConfig _stageDataConfig;

        public List<StageData> StageDataList
        {
            get
            {
                List<StageData> list = _model.StageDataList;
                if (list != null && list.Count > 0)
                    return _model.StageDataList;

                InitDefaultStageData();
                return _model.StageDataList;
            }
        }

        public StageData GetStageData(StageId stageId)
        {
            StageData stageData = StageDataList.Find(stage => stage.StageId == stageId);
            if (!stageData.Equals(default(StageData)))
            {
                return stageData;
            }
            return new StageData();
        }


        private void InitDefaultStageData()
        {
            _model.StageDataList = new List<StageData>();
            foreach (var keyVarPair in _stageDataConfig.DataDict)
            {
                _model.StageDataList.Add(new StageData
                {
                    TotalStar = 0,
                    StageId = keyVarPair.Value.StageId,
                });
            }
            SaveData();
        }
        
#if UNITY_EDITOR
        [FormerlySerializedAs("TestStagePassed")]
        [Button("AddStagePassed", usePropertyAsParameter: true)]
        [SerializeField] private StageData _testStageData;
#endif
        public void AddStagePassed(StageData newStageData)
        {
            StageData existStageData = StageDataList.Find((stage) => stage.StageId == newStageData.StageId);
            // not existStagePassed
            if (existStageData.TotalStar == 0)
                StageDataList.Add(newStageData);
            // Compare lager
            else if (existStageData.TotalStar < newStageData.TotalStar)
            {
                StageDataList.Remove(existStageData);
                StageDataList.Add(newStageData);
            }
            else
                return;
            SaveData();
        }
    }
}

using CustomInspector;
using System;
using System.Collections.Generic;
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
        public void AddStagePassed(StagePassed stagePassed)
        {
            ListStagePassed.Add(stagePassed);
            SaveData();
        }
    }
}

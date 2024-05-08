using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Scripts.Data
{
    public struct StagePassed
    {
        public int TotalStar;
        public StageId StageId;
    }
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
        public void AddStagePassed(StagePassed stagePassed)
        {
            _model.ListStagePassed.Add(stagePassed);
            SaveData();
        }
    }
}
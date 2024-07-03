using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Common.Scripts.Data.DataAsset;
using UnityEngine;
using UnityEngine.Serialization;

namespace Features.Quest.Scripts
{
    public enum QuestType
    {
        DailyQuest = 1,
        WeeklyQuest = 2,
        MonthlyQuest = 3,
    }
    [CreateAssetMenu(fileName = "QuestDataAsset", menuName = "ScriptableObject/DataAsset/QuestDataAsset")]
    public class QuestDataAsset : LocalDataAsset<QuestDataModel>
    {
        [SerializedDictionary("QuestType", "TowerDataSO")]
        public SerializedDictionary<QuestType, List<TaskDataSO>> _questTypeDict =
            new SerializedDictionary<QuestType, List<TaskDataSO>>();
        
        public List<TaskDataSO> GetTaskListByType(QuestType questType)
        {
            _questTypeDict.TryGetValue(questType, out List<TaskDataSO> taskLists);
            
            return taskLists;
        }
        
        public List<QuestData> QuestDatas
        {
            get
            {
                return _model.QuestDatas ?? (_model.QuestDatas = new List<QuestData>());
            }
        }

        public void UpdateQuestData()
        {
            
        }
    }

    [Serializable]
    public struct QuestData
    {
        public QuestType _questType;
        public List<TaskId> _taskIds;
    }   

    [Serializable]
    public struct QuestDataModel : IDefaultDataModel
    {
        public List<QuestData> QuestDatas;

        public bool IsEmpty()
        {
            return (QuestDatas == null || QuestDatas.Count == 0);
        }

        public void SetDefault()
        {
            QuestDatas = new List<QuestData>();
        }
    }
}

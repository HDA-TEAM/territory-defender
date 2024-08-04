using System;
using System.Collections.Generic;
using Common.Scripts.Data.DataAsset;
using Common.Scripts.Data.DataConfig;
using UnityEngine;

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
        [SerializeField] private TaskDataConfig _taskDataConfig;
        [SerializeField] private QuestDataConfig _questDataConfig;
        
        public List<QuestData> QuestDataList
        {
            get
            {
                List<QuestData> list = _model.ListQuestData;
                if (list != null && list.Count > 0)
                    return _model.ListQuestData;

                InitDefaultQuestData();
                return _model.ListQuestData;
            }
        }
        
        public List<TaskDataSO> GetTaskDataSo(QuestType questType)
        {
            QuestData questData = QuestDataList.Find(questData => questData.QuestType == questType);
            if (questData.TaskDataList.Count > 0)
                return questData.TaskDataList;
            
            return new List<TaskDataSO>();
        }

        private void InitDefaultQuestData()
        {
            _model.ListQuestData = new List<QuestData>();
            foreach (var keyVarPair in _questDataConfig.DataDict)
            {
                List<TaskDataSO> listTaskData = new List<TaskDataSO>();
                foreach (var taskId in keyVarPair.Value)
                {
                    if(_taskDataConfig.DataDict.TryGetValue(taskId, out TaskDataSO task))
                    {
                        listTaskData.Add(task);
                    }
                }
                _model.ListQuestData.Add(new QuestData
                {
                    QuestType = keyVarPair.Key,
                    TaskDataList = listTaskData,
                    LastRefreshTime = DateTime.MinValue
                });
            }
            SaveData();
        }
        
        public void UpdateCurQuestComposites(List<QuestComposite> curQuestComposites) //Todo: Need to optimize
        { 
            foreach (var composite in curQuestComposites)
            {
                var idx = _model.ListQuestData.FindIndex(q => q.QuestType == composite.Type);
                if (idx != -1)
                {
                    QuestDataList[idx] = new QuestData
                    {
                        QuestType = composite.Type,
                        TaskDataList = composite.ListTasks,
                        LastRefreshTime = composite.LastRefreshTime
                    };
                }
            }
            SaveData();
        }
        
    }
    [Serializable]
    public struct QuestData
    {
        public QuestType QuestType;
        public List<TaskDataSO> TaskDataList;
        public DateTime LastRefreshTime;
    }   

    [Serializable]
    public struct QuestDataModel : IDefaultDataModel
    {
        public List<QuestData> ListQuestData;

        public bool IsEmpty()
        {
            return (ListQuestData == null || ListQuestData.Count == 0);
        }

        public void SetDefault()
        {
            ListQuestData = new List<QuestData>();
        }
    }
}

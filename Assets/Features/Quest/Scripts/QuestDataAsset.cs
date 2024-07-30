using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Common.Scripts;
using Common.Scripts.Data.DataAsset;
using Common.Scripts.Data.DataConfig;
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
        //public SerializedDictionary<QuestType, List<TaskDataSO>> _questTypeDict =
        //new SerializedDictionary<QuestType, List<TaskDataSO>>();

        //[SerializeField] private List<TaskDataSO> _taskDataSoList;
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

        // public void UpdateQuestData()
        // {
        //     var questDatas = QuestDataList;
        //     //LoadQuestDataFromLocal(questDatas);
        // }
        
        // public void UpdateCurQuestComposites(List<QuestComposite> questComposites)
        // {
        //     for (int i = 0; i < questComposites.Count; i++)
        //     {
        //         var index = QuestDataList.FindIndex(q => q.QuestType == questComposites[i].Type);
        //         QuestComposite questComposite = new QuestComposite()
        //         {
        //             Type = questComposites[i].Type,
        //             ListTasks = questComposites[i].ListTasks,
        //             LastRefreshTime = questComposites[i].LastRefreshTime
        //         };
        //         
        //         questComposite.LastRefreshTime = QuestDataList[index].LastRefreshTime;
        //         questComposites[i] = questComposite;
        //     }
        // }
        //
        // private void LoadQuestDataFromLocal(List<QuestData> questDataLoader)
        // {
        //     foreach (var quest in questDataLoader)
        //     {
        //         if (_questDataConfig.GetConfigByKey(quest.QuestType).Count > 0)
        //         {
        //             // Update existing tasks in the dictionary with the ones from local data
        //             var taskIdList =  _questDataConfig.GetConfigByKey(quest.QuestType);
        //             foreach (var task in taskIdList)
        //             {
        //                 _taskDataConfig.GetConfigByKey(task);
        //             }
        //             
        //         }
        //         else
        //         {
        //             // If the quest type is not found, add it to the dictionary
        //             //_questTypeDict.Add(quest._questType, quest._tasksData);
        //         }
        //     }
        // }
        //
        // public void SaveQuestToLocal(SerializedDictionary<QuestType, List<TaskDataSO>> questTypeDict, List<QuestComposite> questComposites)
        // {
        //     List<QuestData> newQuestList = new List<QuestData>();
        //     foreach (var quest in questTypeDict)
        //     {
        //         if (quest.Value is { Count: > 0 })
        //         {
        //             var questComposite = questComposites.Find(q => q.Type == quest.Key);
        //             var questDataSaver = new QuestData
        //             {
        //                 QuestType = quest.Key,
        //                 TaskDataList = quest.Value,
        //                 LastRefreshTime = questComposite.LastRefreshTime
        //             };
        //             newQuestList.Add(questDataSaver);
        //         }
        //     }
        //     _model.ListQuestData = newQuestList;
        //     SaveData();
        // }
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

using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Common.Scripts;
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
            if (_questTypeDict.TryGetValue(questType, out var taskList))
            {
                return taskList;
            }

            return new List<TaskDataSO>();
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
            var questDatas = QuestDatas;
            LoadQuestDataFromLocal(questDatas);
        }

        private void LoadQuestDataFromLocal(List<QuestData> questDataLoader)
        {
            foreach (var quest in questDataLoader)
            {
                if (_questTypeDict.ContainsKey(quest._questType))
                {
                    // Update existing tasks in the dictionary with the ones from local data
                    _questTypeDict[quest._questType] = quest._tasksData;
                }
                else
                {
                    // If the quest type is not found, add it to the dictionary
                    _questTypeDict.Add(quest._questType, quest._tasksData);
                }
            }
        }
        
        public void SaveQuestToLocal(SerializedDictionary<QuestType, List<TaskDataSO>> questTypeDict)
        {
            List<QuestData> newQuestList = new List<QuestData>();
            foreach (var quest in questTypeDict)
            {
                if (quest.Value is { Count: > 0 })
                {
                    var questDataSaver = new QuestData
                    {
                        _questType = quest.Key,
                        _tasksData = quest.Value
                    };
                    newQuestList.Add(questDataSaver);
                }
            }
            _model.QuestDatas = newQuestList;
            SaveData();
        }
    }
    [Serializable]
    public struct QuestData
    {
        public QuestType _questType;
        public List<TaskDataSO> _tasksData;
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

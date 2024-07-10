using System;
using System.Collections.Generic;
using System.Linq;
using SuperMaxim.Messaging;
using TMPro;
using UnityEngine;

namespace Features.Quest.Scripts
{
    [Serializable]
    public struct QuestComposite
    {
        public QuestType Type;
        public List<TaskDataSO> ListTasks;
    }
    
    public class QuestDataController : MonoBehaviour
    {
        [Header("Data")] public QuestDataAsset _questDataAsset;
        [SerializeField] private List<QuestComposite> _curQuestComposites;

        private readonly TimeSpan _refreshTime = new TimeSpan(7, 0, 0); // 7:00 AM
        public Action OnDateTimeChanged;
        public List<QuestComposite> QuestComposites
        {
            get
            {
                if (_curQuestComposites.Count > 0)
                    return _curQuestComposites;
                
                InitQuestData();
                //InvokeRepeating(nameof(CheckAndRefreshTasks), 0, 60); // Check every minute
                
                return _curQuestComposites;
            }
        }
        
        private void Awake()
        {
            Messenger.Default.Subscribe<DatetimeChangePayload>(GetDateTimeTest);
        }

        private void OnDestroy()
        {
            Messenger.Default.Unsubscribe<DatetimeChangePayload>(GetDateTimeTest);
        }

        private void GetDateTimeTest(DatetimeChangePayload datetimeChangePayload)
        {
            CheckAndRefreshTasks(datetimeChangePayload.DateTime);
        }

        private void CheckAndRefreshTasks(DateTime? testTime = null)
        {
            //DateTime now = DateTime.Now;
            DateTime now = testTime ?? DateTime.Now;
            DateTime nextDailyRefresh = GetNextRefreshTime(now, _refreshTime, "daily");
            DateTime nextWeeklyRefresh = GetNextRefreshTime(now, _refreshTime, "weekly");
            DateTime nextMonthlyRefresh = GetNextRefreshTime(now, _refreshTime, "monthly");
        
            // Daily refresh
            if (now >= nextDailyRefresh)
            {
                RefreshDailyTasks();
            }
            if (now >= nextWeeklyRefresh)
            {
                RefreshWeeklyTasks();
            }

            if (now >= nextMonthlyRefresh)
            {
                RefreshMonthlyTasks();
            }
            OnDateTimeChanged?.Invoke(); 
        }
        
        private DateTime GetNextRefreshTime(DateTime now, TimeSpan refreshTime, string type)
        {
            DateTime nextRefresh = new DateTime(now.Year, now.Month, now.Day, refreshTime.Hours, refreshTime.Minutes, refreshTime.Seconds);
            
            switch (type)
            {
                case "daily":
                    if (now > nextRefresh)
                    {
                        nextRefresh = nextRefresh.AddDays(1);
                    }
                    break;

                case "weekly":
                    int daysUntilSunday = ((int)DayOfWeek.Sunday - (int)now.DayOfWeek + 7) % 7;
                    if (now > nextRefresh)
                    {
                        nextRefresh = nextRefresh.AddDays(daysUntilSunday + 1);
                    }
                    else
                    {
                        nextRefresh = nextRefresh.AddDays(daysUntilSunday);
                    }
                    break;

                case "monthly":
                    if (now > nextRefresh)
                    {
                        nextRefresh = nextRefresh.AddMonths(1);
                    }
                    break;
            }

            return nextRefresh;
        }
        
        private void RefreshDailyTasks()
        {
            var index = _curQuestComposites.FindIndex(quest => quest.Type == QuestType.DailyQuest);
            if (index != -1)
            {
                var dailyQuest = _curQuestComposites[index];
                dailyQuest.ListTasks = GenerateNewDailyTasks();
                ResetTasks(dailyQuest.ListTasks);
                _curQuestComposites[index] = dailyQuest;
            }
        }
        private void RefreshWeeklyTasks()
        {
            var index = _curQuestComposites.FindIndex(quest => quest.Type == QuestType.WeeklyQuest);
            if (index != -1)
            {
                var weeklyQuest = _curQuestComposites[index];
                weeklyQuest.ListTasks = GenerateNewWeeklyTasks();
                ResetTasks(weeklyQuest.ListTasks);
                _curQuestComposites[index] = weeklyQuest;
            }
        }
        private void RefreshMonthlyTasks()
        {
            var index = _curQuestComposites.FindIndex(quest => quest.Type == QuestType.MonthlyQuest);
            if (index != -1)
            {
                var monthlyQuest = _curQuestComposites[index];
                monthlyQuest.ListTasks = GenerateNewMonthlyTasks();
                ResetTasks(monthlyQuest.ListTasks);
                _curQuestComposites[index] = monthlyQuest;
            }
        }
        
        private void ResetTasks(List<TaskDataSO> tasks)
        {
            foreach (var task in tasks)
            {
                task.IsCompleted = false;
                task.CompletionTime = DateTime.MinValue;
            }
        }
        
        private List<TaskDataSO> GenerateNewDailyTasks()
        {
            // Return daily tasks
            return _questDataAsset.GetTaskListByType(QuestType.DailyQuest);
        }
        private List<TaskDataSO> GenerateNewWeeklyTasks()
        {
            // Return weekly tasks
            return _questDataAsset.GetTaskListByType(QuestType.WeeklyQuest);
        }
        private List<TaskDataSO> GenerateNewMonthlyTasks()
        {
            // Return monthly tasks
            return _questDataAsset.GetTaskListByType(QuestType.MonthlyQuest);
        }
        
        public void InitQuestData()
        {
            // Convert keys to a list for indexing
            var keys = new List<QuestType>(_questDataAsset._questTypeDict.Keys);
            
            for (int i = 0; i < keys.Count; i++)
            {
                QuestType questType = keys[i];
                List<TaskDataSO> taskList = _questDataAsset.GetTaskListByType(questType);
                _curQuestComposites.Add(new QuestComposite
                    {
                        Type = questType,
                        ListTasks = taskList
                    }
                );
            }
        }
        
        public TaskDataSO FindTask(QuestType questType, TaskId taskId)
        {
            var questComposite = _curQuestComposites.FirstOrDefault(q => q.Type == questType);
            if (questComposite.Equals(default(QuestComposite)))
            {
                return null;
            }

            return questComposite.ListTasks.FirstOrDefault(t => t.TaskId == taskId);
        }
    }
}

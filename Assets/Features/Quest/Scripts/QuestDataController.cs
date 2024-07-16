using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SuperMaxim.Messaging;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

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
        
        [Header("Setup Time Refresh")]
        [Space(15)]
        public int _refreshHour = 7;
        public int _refreshMinute = 0;
        public int _refreshSecond = 0;

        private TimeSpan _refreshTime;
        private DateTime _lastDailyRefresh;
        private DateTime _lastWeeklyRefresh;
        private DateTime _lastMonthlyRefresh;
        
        public Action OnDateTimeChange;
        public List<QuestComposite> QuestComposites
        {
            get
            {
                if (_curQuestComposites.Count > 0)
                    return _curQuestComposites;
                
                InitQuestData();
                StartCoroutine(RefreshAtTime());
                
                return _curQuestComposites;
            }
        }
        private void Start()
        {
            _refreshTime = new TimeSpan(_refreshHour, _refreshMinute, _refreshSecond);
            SetLastRefreshTimes();
        }
        
        private void SetLastRefreshTimes()
        {
            _lastDailyRefresh = DateTime.Now.Date.AddDays(-1).Add(_refreshTime); // Yesterday at refresh time
            _lastWeeklyRefresh = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek - 7).Date.Add(_refreshTime); // Last week
            _lastMonthlyRefresh = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1).Add(_refreshTime); // Last month
        }
        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator RefreshAtTime()
        {
            while (true)
            {
                DateTime now = DateTime.Now;
                DateTime nextDailyRefresh = GetNextDailyRefreshTime(now, _refreshTime);
                DateTime nextWeeklyRefresh = GetNextWeeklyRefreshTime(now, _refreshTime);
                DateTime nextMonthlyRefresh = GetNextMonthlyRefreshTime(now, _refreshTime);
                
                DateTime nextRefresh = new[] { nextDailyRefresh, nextWeeklyRefresh, nextMonthlyRefresh }.Min();
                
                TimeSpan timeUntilNextRefresh = nextRefresh - now;
                if (timeUntilNextRefresh.TotalSeconds < 0)
                {
                    timeUntilNextRefresh = TimeSpan.Zero; // Refresh immediately if the time has already passed
                }
                
                yield return new WaitForSeconds((float)timeUntilNextRefresh.TotalSeconds);
                
                // Update messenger
                DatetimeChangePayload datetimeChangePayload;
                datetimeChangePayload.DateTime = now;
                CheckAndRefreshTasks(datetimeChangePayload.DateTime);
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
            DateTime now = testTime ?? DateTime.Now;
            
            if (now >= GetNextDailyRefreshTime(_lastDailyRefresh, _refreshTime))
            {
                RefreshDailyTasks();
                _lastDailyRefresh = now.Date.Add(_refreshTime);
                Debug.Log(" RefreshDailyTasks........Done");
            } else { Debug.Log("RefreshDailyTasks......Fail!");}
            
            if (now >= GetNextWeeklyRefreshTime(_lastWeeklyRefresh, _refreshTime))
            {
                RefreshWeeklyTasks();
                _lastWeeklyRefresh = GetNextWeeklyRefreshTime(_lastWeeklyRefresh, _refreshTime);
                Debug.Log(" RefreshWeeklyTasks........Done");
            } else { Debug.Log("RefreshWeeklyTasks......Fail!");}
            
            if (now >= GetNextMonthlyRefreshTime(_lastMonthlyRefresh, _refreshTime))
            {
                RefreshMonthlyTasks();
                _lastMonthlyRefresh = GetNextMonthlyRefreshTime(_lastMonthlyRefresh, _refreshTime);
                Debug.Log(" RefreshMonthlyTasks........Done");
            } else { Debug.Log("RefreshWeeklyTasks......Fail!");}
            
            OnDateTimeChange?.Invoke();
        }
        
        private DateTime GetNextDailyRefreshTime(DateTime lastRefresh, TimeSpan refreshTime)
        {
            DateTime nextRefresh = new DateTime(_lastDailyRefresh.Year, _lastDailyRefresh.Month, _lastDailyRefresh.Day, refreshTime.Hours, refreshTime.Minutes, refreshTime.Seconds);
            if (nextRefresh <= lastRefresh)
            {
                nextRefresh = nextRefresh.AddDays(1);
            }
            return nextRefresh;
        }
        
        private DateTime GetNextWeeklyRefreshTime(DateTime lastRefresh, TimeSpan refreshTime)
        {
            DateTime nextRefresh = new DateTime(_lastWeeklyRefresh.Year, _lastWeeklyRefresh.Month, _lastWeeklyRefresh.Day, refreshTime.Hours, refreshTime.Minutes, refreshTime.Seconds);
            if (nextRefresh <= lastRefresh)
            {
                nextRefresh = nextRefresh.AddDays(7);
            }
            return nextRefresh;
        }
        
        private DateTime GetNextMonthlyRefreshTime(DateTime lastRefresh, TimeSpan refreshTime)
        {
            DateTime nextRefresh = new DateTime(_lastMonthlyRefresh.Year, _lastMonthlyRefresh.Month, _lastMonthlyRefresh.Day, refreshTime.Hours, refreshTime.Minutes, refreshTime.Seconds);
            if (nextRefresh <= lastRefresh)
            {
                nextRefresh = nextRefresh.AddMonths(1);
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

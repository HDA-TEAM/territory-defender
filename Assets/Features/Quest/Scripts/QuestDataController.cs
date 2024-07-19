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
        private static DateTime _lastDailyRefresh;
        private static DateTime _lastWeeklyRefresh;
        private static DateTime _lastMonthlyRefresh;
        
        private static DateTime _nextDailyRefresh;
        private static DateTime _nextWeeklyRefresh;
        private static DateTime _nextMonthlyRefresh;

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
            _lastDailyRefresh = DateTime.Now.Date.Add(_refreshTime);
            
            // Calculate last weekly refresh
            var today = DateTime.Now.Date;
            var daysSinceLastMonday = (int)today.DayOfWeek - (int)DayOfWeek.Monday;
            if (daysSinceLastMonday < 0)
            {
                daysSinceLastMonday += 7; // Adjust if the current day is Sunday
            }
            _lastWeeklyRefresh = today.AddDays(daysSinceLastMonday - 8).Add(_refreshTime);

            // Calculate last monthly refresh
            var firstDayOfCurrentMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            _lastMonthlyRefresh = firstDayOfCurrentMonth.Add(_refreshTime);

            _nextDailyRefresh = GetNextDailyRefreshTime(_lastDailyRefresh, _refreshTime);
            _nextWeeklyRefresh = GetNextWeeklyRefreshTime(_lastWeeklyRefresh, _refreshTime);
            _nextMonthlyRefresh = GetNextMonthlyRefreshTime(_lastMonthlyRefresh, _refreshTime);
            
            // Log the values
            Debug.Log("Last Daily Refresh: " + _lastDailyRefresh);
            Debug.Log("Last Weekly Refresh: " + _lastWeeklyRefresh);
            Debug.Log("Last Monthly Refresh: " + _lastMonthlyRefresh);
            Debug.Log("Next Daily Refresh: " + _nextDailyRefresh);
            Debug.Log("Next Weekly Refresh: " + _nextWeeklyRefresh);
            Debug.Log("Next Monthly Refresh: " + _nextMonthlyRefresh);
        }
        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator RefreshAtTime()
        {
            while (true)
            {
                DateTime now = DateTime.Now;
                // DateTime nextDailyRefresh = GetNextDailyRefreshTime(now, _refreshTime);
                // DateTime nextWeeklyRefresh = GetNextWeeklyRefreshTime(now, _refreshTime);
                // DateTime nextMonthlyRefresh = GetNextMonthlyRefreshTime(now, _refreshTime);
                
                DateTime nextRefresh = new[] { _nextDailyRefresh, _nextWeeklyRefresh, _nextMonthlyRefresh }.Min();
                
                TimeSpan timeUntilNextRefresh = nextRefresh - now;
                if (timeUntilNextRefresh.TotalSeconds < 0)
                {
                    timeUntilNextRefresh = TimeSpan.Zero; // Refresh immediately if the time has already passed
                }
                
                yield return new WaitForSeconds((float)timeUntilNextRefresh.TotalSeconds);
                
                // Update messenger
                // DatetimeChangePayload datetimeChangePayload;
                // datetimeChangePayload.DateTime = now;
                CheckAndRefreshTasks();
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

        private void SetLastRefreshTimesBaseOnTestData(DateTime testTime)
        {
            _lastDailyRefresh = testTime.TimeOfDay >= _refreshTime ? testTime.Date.Add(_refreshTime) : testTime.Date.AddDays(-1).Add(_refreshTime);

            // Calculate last weekly refresh
            var today = testTime.Date;
            var daysSinceLastMonday = (int)today.DayOfWeek - (int)DayOfWeek.Monday;
            if (daysSinceLastMonday < 0)
            {
                daysSinceLastMonday += 7; // Adjust if the current day is Sunday
            }
            _lastWeeklyRefresh = today.AddDays(daysSinceLastMonday - 8).Add(_refreshTime);

            // Calculate last monthly refresh
            var firstDayOfCurrentMonth = new DateTime(testTime.Year, testTime.Month, 1);
            _lastMonthlyRefresh = firstDayOfCurrentMonth.Add(_refreshTime);

            _nextDailyRefresh = GetNextDailyRefreshTime(_lastDailyRefresh, _refreshTime);
            _nextWeeklyRefresh = GetNextWeeklyRefreshTime(_lastWeeklyRefresh, _refreshTime);
            _nextMonthlyRefresh = GetNextMonthlyRefreshTime(_lastMonthlyRefresh, _refreshTime);
            
            // Log the values
            Debug.Log("Last Daily Refresh: " + _lastDailyRefresh);
            Debug.Log("Last Weekly Refresh: " + _lastWeeklyRefresh);
            Debug.Log("Last Monthly Refresh: " + _lastMonthlyRefresh);
            Debug.Log("Next Daily Refresh: " + _nextDailyRefresh);
            Debug.Log("Next Weekly Refresh: " + _nextWeeklyRefresh);
            Debug.Log("Next Monthly Refresh: " + _nextMonthlyRefresh);
        }

        private void CheckAndRefreshTasks(DateTime? time = null)
        {
            if (time != null)
            {
                SetLastRefreshTimesBaseOnTestData(time.Value);
            }
            DateTime now = time ?? DateTime.Now;
    
            if (now >= _nextDailyRefresh)
            {
                RefreshDailyTasks();
                _lastDailyRefresh = _nextDailyRefresh;
                _nextDailyRefresh = GetNextDailyRefreshTime(now, _refreshTime);
                Debug.Log("RefreshDailyTasks........Done");
            }
            else
            {
                Debug.Log("RefreshDailyTasks......Fail!");
            }
    
            if (now >= _nextWeeklyRefresh)
            {
                RefreshWeeklyTasks();
                _lastWeeklyRefresh = _nextWeeklyRefresh;
                _nextWeeklyRefresh = GetNextWeeklyRefreshTime(_lastWeeklyRefresh, _refreshTime);
                Debug.Log("RefreshWeeklyTasks........Done");
            }
            else
            {
                Debug.Log("RefreshWeeklyTasks......Fail!");
            }
    
            if (now >= _nextMonthlyRefresh)
            {
                RefreshMonthlyTasks();
                _lastMonthlyRefresh = _nextMonthlyRefresh;
                _nextMonthlyRefresh = GetNextMonthlyRefreshTime(_lastMonthlyRefresh, _refreshTime);
                Debug.Log("RefreshMonthlyTasks........Done");
            }
            else
            {
                Debug.Log("RefreshMonthlyTasks......Fail!");
            }
    
            _questDataAsset.SaveQuestToLocal(_questDataAsset._questTypeDict);
            OnDateTimeChange?.Invoke();
        }
        
        private DateTime GetNextDailyRefreshTime(DateTime lastRefresh, TimeSpan refreshTime)
        {
            //DateTime nextRefresh = new DateTime(_lastDailyRefresh.Year, _lastDailyRefresh.Month, _lastDailyRefresh.Day, refreshTime.Hours, refreshTime.Minutes, refreshTime.Seconds);
            DateTime nextRefresh = lastRefresh.Date.Add(refreshTime);
            if (nextRefresh <= lastRefresh)
            {
                nextRefresh = nextRefresh.AddDays(1);
            }
            return nextRefresh;
        }
        
        private DateTime GetNextWeeklyRefreshTime(DateTime lastRefresh, TimeSpan refreshTime)
        {
            //DateTime nextRefresh = new DateTime(_lastWeeklyRefresh.Year, _lastWeeklyRefresh.Month, _lastWeeklyRefresh.Day, refreshTime.Hours, refreshTime.Minutes, refreshTime.Seconds);
            DateTime nextRefresh = lastRefresh.Date.Add(refreshTime);
            if (nextRefresh <= lastRefresh)
            {
                nextRefresh = nextRefresh.AddDays(8);
            }
            return nextRefresh;
        }
        
        private DateTime GetNextMonthlyRefreshTime(DateTime lastRefresh, TimeSpan refreshTime)
        {
            //DateTime nextRefresh = new DateTime(_lastMonthlyRefresh.Year, _lastMonthlyRefresh.Month, _lastMonthlyRefresh.Day, refreshTime.Hours, refreshTime.Minutes, refreshTime.Seconds);
            DateTime nextRefresh = lastRefresh.Date.Add(refreshTime);
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
            // Update data
            _questDataAsset.UpdateQuestData();
            
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

        public void UpdateTaskCompletedData(TaskId taskId)
        {
            var tasks = QuestComposites.Find(quest => quest.ListTasks.Find(task => task.TaskId == taskId));
            var findTask = tasks.ListTasks.Find(task => task.TaskId == taskId);
            findTask.CompletionTime = DateTime.Now;
            _questDataAsset.SaveQuestToLocal(_questDataAsset._questTypeDict);
        }
    }
}

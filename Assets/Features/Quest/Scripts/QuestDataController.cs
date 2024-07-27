using System;
using System.Collections;
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
        public DateTime LastRefreshTime;
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

                return _curQuestComposites;
            }
        }
        
        private void Start()
        {
            _refreshTime = new TimeSpan(_refreshHour, _refreshMinute, _refreshSecond);
            SetLastRefreshTimes();
            StartCoroutine(RefreshAtTime());
        }
        
        private void SetLastRefreshTimes(DateTime? time = null)
        {
            //DateTime todayTime = time ?? DateTime.Now;
            if (time == null)
            {
                DateTime todayTime = DateTime.Now;
                _lastDailyRefresh = todayTime.TimeOfDay >= _refreshTime ? todayTime.Date.Add(_refreshTime) : todayTime.Date.AddDays(-1).Add(_refreshTime);

                // Calculate last weekly refresh
                var today = todayTime.Date;
                var daysSinceLastMonday = (int)today.DayOfWeek - (int)DayOfWeek.Monday;
                if (daysSinceLastMonday < 0)
                {
                    daysSinceLastMonday += 7; // Adjust if the current day is Sunday
                }
                _lastWeeklyRefresh = today.AddDays(-daysSinceLastMonday).Add(_refreshTime);
            
                // Calculate last monthly refresh
                var firstDayOfCurrentMonth = new DateTime(todayTime.Year, todayTime.Month, 1);
                _lastMonthlyRefresh = firstDayOfCurrentMonth.Add(_refreshTime);
            }
            else
            {
                var dailyQuest = _curQuestComposites.Find(q => q.Type == QuestType.DailyQuest);
                _lastDailyRefresh = dailyQuest.LastRefreshTime;
                
                var weeklyQuest = _curQuestComposites.Find(q => q.Type == QuestType.WeeklyQuest);
                _lastWeeklyRefresh = weeklyQuest.LastRefreshTime;
                
                var monthlyQuest = _curQuestComposites.Find(q => q.Type == QuestType.MonthlyQuest);
                _lastMonthlyRefresh = monthlyQuest.LastRefreshTime;
            }
            

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
        
        private void SetLastTimeRefresh(QuestType questType, DateTime lastTimeRefresh)
        {
            var index = _curQuestComposites.FindIndex(q => q.Type == questType);
            var quest = _curQuestComposites[index];
            quest.LastRefreshTime = lastTimeRefresh;
            _curQuestComposites[index] = quest;

        }
        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator RefreshAtTime()
        {
            Debug.Log("RefreshAtTime....???");
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
                
                CheckAndRefreshTasks();
                
                yield return new WaitForSeconds((float)timeUntilNextRefresh.TotalSeconds);
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
        private void CheckAndRefreshTasks(DateTime? time = null)
        {
            DateTime now = time ?? DateTime.Now;
            
            if (time != null)
            {
                SetLastRefreshTimes(time);
            }
            
            bool isNewDay = now >= _lastDailyRefresh && (now - _lastDailyRefresh).TotalDays >= 1;
            bool isNewWeek = now >= _lastWeeklyRefresh && (now - _lastWeeklyRefresh).TotalDays >= 7;
            bool isNewMonth = now >= _lastMonthlyRefresh && (now - _lastMonthlyRefresh).TotalDays >= DateTime.DaysInMonth(now.Year, now.Month - 1);

            // if (isNewDay) SetLastTimeRefresh(QuestType.DailyQuest, _lastDailyRefresh);
            // if (isNewWeek) SetLastTimeRefresh(QuestType.WeeklyQuest, _lastWeeklyRefresh);
            // if (isNewMonth) SetLastTimeRefresh(QuestType.MonthlyQuest, _lastMonthlyRefresh);
    
            if (isNewDay)
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
    
            if (isNewWeek)
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
    
            if (isNewMonth)
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
    
            _questDataAsset.SaveQuestToLocal(_questDataAsset._questTypeDict, _curQuestComposites);
            OnDateTimeChange?.Invoke();
        }
        
        private DateTime GetNextDailyRefreshTime(DateTime lastRefresh, TimeSpan refreshTime)
        {
            DateTime nextRefresh = lastRefresh.Date.Add(refreshTime);
            if (nextRefresh <= lastRefresh)
            {
                nextRefresh = nextRefresh.AddDays(1);
            }
            return nextRefresh;
        }
        
        private DateTime GetNextWeeklyRefreshTime(DateTime lastRefresh, TimeSpan refreshTime)
        {
            DateTime nextRefresh = lastRefresh.Date.Add(refreshTime);
            if (nextRefresh <= lastRefresh)
            {
                nextRefresh = nextRefresh.AddDays(7);
            }
            return nextRefresh;
        }
        
        private DateTime GetNextMonthlyRefreshTime(DateTime lastRefresh, TimeSpan refreshTime)
        {
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
                dailyQuest.LastRefreshTime = _lastDailyRefresh;
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
                weeklyQuest.LastRefreshTime = _lastWeeklyRefresh;
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
                monthlyQuest.LastRefreshTime = _lastMonthlyRefresh;
                _curQuestComposites[index] = monthlyQuest;
            }
        }
        
        private void ResetTasks(List<TaskDataSO> tasks)
        {
            foreach (var task in tasks)
            {
                task.IsCompleted = false;
                //task.CompletionTime = DateTime.MinValue;
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
            _questDataAsset.UpdateQuestData(_curQuestComposites);
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
            //findTask.CompletionTime = DateTime.Now;
            _questDataAsset.SaveQuestToLocal(_questDataAsset._questTypeDict, _curQuestComposites);
        }
    }
}

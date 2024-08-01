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
            //SetLastRefreshTimes();
            
            InitializeQuestRefreshTimes();
            StartCoroutine(RefreshAtTime());
        }

        private void SetLastRefreshTime(QuestType questType, ref DateTime lastRefreshTime)
        {
            var questComposite = _curQuestComposites.Find(q => q.Type == questType);

            if (questComposite.LastRefreshTime == DateTime.MinValue)
            {
                var today = DateTime.Today;

                if (questType == QuestType.DailyQuest)
                {
                    questComposite.LastRefreshTime = today.Add(_refreshTime);
                }
                else if (questType == QuestType.WeeklyQuest)
                {
                    // Calculate the Monday of the current week
                    int daysSinceMonday = (int)today.DayOfWeek - (int)DayOfWeek.Monday;
                    if (daysSinceMonday < 0)
                    {
                        daysSinceMonday += 7; // Adjust if the current day is Sunday
                    }
                    var mondayOfThisWeek = today.AddDays(-daysSinceMonday);
                    questComposite.LastRefreshTime = mondayOfThisWeek.Add(_refreshTime);
                }
                else if (questType == QuestType.MonthlyQuest)
                {
                    // Calculate the first day of the current month
                    var firstDayOfCurrentMonth = new DateTime(today.Year, today.Month, 1);
                    questComposite.LastRefreshTime = firstDayOfCurrentMonth.Add(_refreshTime);
                }

                lastRefreshTime = questComposite.LastRefreshTime;
            }
            else
            {
                lastRefreshTime = questComposite.LastRefreshTime;
            }

            // Update the quest in the list
            int index = _curQuestComposites.FindIndex(q => q.Type == questType);
            if (index != -1)
            {
                _curQuestComposites[index] = questComposite;
            }
        }

        
        private void InitializeQuestRefreshTimes()
        {
            SetLastRefreshTime(QuestType.DailyQuest, ref _lastDailyRefresh);
            SetLastRefreshTime(QuestType.WeeklyQuest, ref _lastWeeklyRefresh);
            SetLastRefreshTime(QuestType.MonthlyQuest, ref _lastMonthlyRefresh);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator RefreshAtTime()
        {
            Debug.Log("RefreshAtTime....???");
            while (true)
            {
                DateTime now = DateTime.Now;
                DateTime nextDailyRefresh = GetNextDailyRefreshTime(now, _lastDailyRefresh.TimeOfDay);
                DateTime nextWeeklyRefresh = GetNextWeeklyRefreshTime(now, _lastWeeklyRefresh.TimeOfDay);
                DateTime nextMonthlyRefresh = GetNextMonthlyRefreshTime(now, _lastMonthlyRefresh.TimeOfDay);
                
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
            
            // Log the values
            Debug.Log("Last Daily Refresh: " + _lastDailyRefresh);
            Debug.Log("Last Weekly Refresh: " + _lastWeeklyRefresh);
            Debug.Log("Last Monthly Refresh: " + _lastMonthlyRefresh);
            
            if (now >= _lastDailyRefresh)
            {
                RefreshDailyTasks();
                _nextDailyRefresh = GetNextDailyRefreshTime(now, _refreshTime);
                _lastDailyRefresh = _nextDailyRefresh;
                
                var index = _curQuestComposites.FindIndex(q => q.Type == QuestType.DailyQuest);
                var quest = _curQuestComposites.Find(q => q.Type == QuestType.DailyQuest);
                quest.LastRefreshTime = _lastDailyRefresh;
                _curQuestComposites[index] = quest;
                
                Debug.Log("RefreshDailyTasks........Done");
            }
            else
            {
                Debug.Log("RefreshDailyTasks......Fail!");
            }
    
            if (now >= _lastWeeklyRefresh)
            {
                RefreshWeeklyTasks();
                _nextWeeklyRefresh = GetNextWeeklyRefreshTime(_lastWeeklyRefresh, _refreshTime);
                _lastWeeklyRefresh = _nextWeeklyRefresh;
                
                var index = _curQuestComposites.FindIndex(q => q.Type == QuestType.WeeklyQuest);
                var quest = _curQuestComposites.Find(q => q.Type == QuestType.WeeklyQuest);
                quest.LastRefreshTime = _lastWeeklyRefresh;
                _curQuestComposites[index] = quest;
                Debug.Log("RefreshWeeklyTasks........Done");
            }
            else
            {
                Debug.Log("RefreshWeeklyTasks......Fail!");
            }
    
            if (now >= _lastMonthlyRefresh)
            {
                RefreshMonthlyTasks();
                _nextMonthlyRefresh = GetNextMonthlyRefreshTime(_lastMonthlyRefresh, _refreshTime);
                _lastMonthlyRefresh = _nextMonthlyRefresh;
                
                var index = _curQuestComposites.FindIndex(q => q.Type == QuestType.MonthlyQuest);
                var quest = _curQuestComposites.Find(q => q.Type == QuestType.MonthlyQuest);
                quest.LastRefreshTime = _lastMonthlyRefresh;
                _curQuestComposites[index] = quest;
                Debug.Log("RefreshMonthlyTasks........Done");
            }
            else
            {
                Debug.Log("RefreshMonthlyTasks......Fail!");
            }
            //Todo
            //_questDataAsset.SaveQuestToLocal(_questDataAsset._questTypeDict, _curQuestComposites);
            
            
            Debug.Log("Next Daily Refresh: " + _nextDailyRefresh);
            Debug.Log("Next Weekly Refresh: " + _nextWeeklyRefresh);
            Debug.Log("Next Monthly Refresh: " + _nextMonthlyRefresh);
            
            _questDataAsset.UpdateCurQuestComposites(_curQuestComposites);
            //_questDataAsset.SaveQuestData();
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
                task.IsGotten = false;
                //task.CompletionTime = DateTime.MinValue;
            }
        }
        
        private List<TaskDataSO> GenerateNewDailyTasks()
        {
            // Return daily tasks
            return _questDataAsset.GetTaskDataSo(QuestType.DailyQuest);
        }
        private List<TaskDataSO> GenerateNewWeeklyTasks()
        {
            // Return weekly tasks
            return _questDataAsset.GetTaskDataSo(QuestType.WeeklyQuest);
        }
        private List<TaskDataSO> GenerateNewMonthlyTasks()
        {
            // Return monthly tasks
            return _questDataAsset.GetTaskDataSo(QuestType.MonthlyQuest);
        }
        
        public void InitQuestData()
        {
            // Update data
            //_questDataAsset.UpdateQuestData();
           
            //Todo
            // Convert keys to a list for indexing
            //var keys = new List<QuestType>(_questDataAsset.QuestDataList);
            
            for (int i = 0; i < _questDataAsset.QuestDataList.Count; i++)
            {
                var type = _questDataAsset.QuestDataList[i].QuestType;
                QuestType questType = type;
                List<TaskDataSO> taskList = _questDataAsset.GetTaskDataSo(questType);
                DateTime time = _questDataAsset.QuestDataList[i].LastRefreshTime;
                _curQuestComposites.Add(new QuestComposite
                    {
                        Type = questType,
                        ListTasks = taskList,
                        LastRefreshTime = time,
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

            return questComposite.ListTasks.FirstOrDefault(t => t._taskId == taskId);
        }

        public void UpdateTaskCompletedData(TaskId taskId)
        {
            //Todo
            var idx = _curQuestComposites.FindIndex(quest => quest.ListTasks.Find(task => task._taskId == taskId));
            var foundTask = _curQuestComposites[idx].ListTasks.Find(task => task._taskId == taskId);
            var foundTaskIdx = _curQuestComposites[idx].ListTasks.FindIndex(task => task._taskId == taskId);
            
            foundTask.IsGotten = true;
            _curQuestComposites[idx].ListTasks[foundTaskIdx] = foundTask;
            
            _questDataAsset.UpdateCurQuestComposites( _curQuestComposites);
        }
    }
}

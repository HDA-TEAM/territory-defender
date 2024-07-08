using System;
using CustomInspector;
using UnityEngine;

namespace Features.Quest.Scripts
{
    public class TestQuestRefresh : MonoBehaviour
    {
        [SerializeField] private QuestDataController _questDataController;
        
        [Header("UI Elements")]
        [SerializeField] private string _day;
        [SerializeField] private string _month;

        private void Start()
        {
            int.TryParse(_day, out var day);
            int.TryParse(_month, out var month);
            
            DateTime testTime = new DateTime(2024, month, day, 7, 0, 0);
            _questDataController.TestCheckAndRefreshTasks(testTime);
        }
        
#if UNITY_EDITOR
        [Button("TestTryChangeDateTimeData")] 
        public DateTime TestDateTimee;

#endif
    }
}

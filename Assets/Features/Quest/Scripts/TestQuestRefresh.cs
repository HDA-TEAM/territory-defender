using System;
using NaughtyAttributes;
using SuperMaxim.Messaging;
using UnityEngine;

namespace Features.Quest.Scripts
{
    public class TestQuestRefresh : MonoBehaviour
    {
        [SerializeField] private string _day;
        [SerializeField] private string _month;
        [SerializeField] private int _year = 2024;
        [SerializeField] private int _hour = 7;
        [SerializeField] private int _minute = 0;
        [SerializeField] private int _second = 0;

        [Button("TestTryRefresh")]
        public void TestTryRefresh() 
        {
            int.TryParse(_day, out var day);
            int.TryParse(_month, out var month);
            
            DateTime dateTime =  new DateTime(_year, month, day, _hour, _minute, _second);
            TryChangeTimeRefresh(dateTime);
        }

        private void TryChangeTimeRefresh(DateTime dateTime)
        {
            NotifyDateTimeChange(dateTime);
        }
        private static void NotifyDateTimeChange(DateTime dateTime)
        {
            Messenger.Default.Publish(new DatetimeChangePayload
            {
                DateTime = dateTime,
            });
        }
    }
    
    public struct DatetimeChangePayload
    {
        public DateTime DateTime;
    }
}

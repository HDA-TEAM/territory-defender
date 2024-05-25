using Firebase.Analytics;
using GamePlay.Scripts.Data;
using SuperMaxim.Messaging;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Scripts
{
    public struct StageStartPayload
    {
        public StageId StageId;
    }
    public class AnalyticManager : MonoBehaviour
    {
        #region Event names
        private const string StageStart = "stage_start";
        private const string StageEnd = "stage_end";
        #endregion

        #region Event params
        private const string StageId = "stage_id";
        private const string StarClaimed = "star_claimed";
        #endregion

        private void Start()
        {
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            FirebaseAnalytics.SetUserId(SystemInfo.deviceUniqueIdentifier);
            Messenger.Default.Subscribe<StageStartPayload>(LogEventStageStart);
        }
        private void OnDestroy()
        {
            Messenger.Default.Unsubscribe<StageStartPayload>(LogEventStageStart);
        }
        private void LogEventStageStart(StageStartPayload stageStartPayload)
        {
            Firebase.Analytics.FirebaseAnalytics
                .LogEvent(Firebase.Analytics.FirebaseAnalytics.EventLogin);
            Firebase.Analytics.FirebaseAnalytics.LogEvent(
                Firebase.Analytics.FirebaseAnalytics.EventSelectContent,
                new Firebase.Analytics.Parameter(
                    Firebase.Analytics.FirebaseAnalytics.ParameterItemName, "name"),
                new Firebase.Analytics.Parameter(
                    Firebase.Analytics.FirebaseAnalytics.UserPropertySignUpMethod, "Google")
            );
            try
            {
                List<Parameter> parameters = new List<Parameter>
                {
                    new Parameter(StageId, stageStartPayload.StageId.ToString()),
                };

                LogEvent(StageStart,parameters.ToArray());
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        private static void LogEvent(string eventName, Parameter[] parameters)
        {
            FirebaseAnalytics.LogEvent(eventName,parameters);
            string strEventLog = $"LogEvent :{eventName} ";
            foreach (var parameter in parameters)
            {
                strEventLog += $"{parameter} ,";
            }
            Debug.Log(strEventLog);
        }
    }
}

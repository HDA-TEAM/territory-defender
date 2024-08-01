#if !UNITY_WEBGL
using Firebase.Analytics;
#endif
using SuperMaxim.Messaging;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Scripts
{
    public class AnalyticManager : MonoBehaviour
    {
        #region Event names
        private const string StageStart = "stage_start";
        private const string StageFailed = "stage_failed";
        private const string StageSuccess = "stage_success";
        #endregion

        #region Event params
        private const string StageId = "stage_id";
        private const string StarCollect = "star_collected";
        #endregion

#if !UNITY_WEBGL
        private void Start()
        {
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            FirebaseAnalytics.SetUserId(SystemInfo.deviceUniqueIdentifier);
            Messenger.Default.Subscribe<StageStartPayload>(LogEventStageStart);
            Messenger.Default.Subscribe<StageFinishedPayload>(LogEventStageFinished);
        }
        private void OnDestroy()
        {
            Messenger.Default.Unsubscribe<StageStartPayload>(LogEventStageStart);
            Messenger.Default.Unsubscribe<StageFinishedPayload>(LogEventStageFinished);
        }
        private void LogEventStageStart(StageStartPayload stageStartPayload)
        {
            FirebaseAnalytics
                .LogEvent(FirebaseAnalytics.EventLogin);
            FirebaseAnalytics.LogEvent(
                FirebaseAnalytics.EventSelectContent,
                new Parameter(
                    FirebaseAnalytics.ParameterItemName, "name"),
                new Parameter(
                    FirebaseAnalytics.UserPropertySignUpMethod, "Google")
            );
            try
            {
                List<Parameter> parameters = new List<Parameter>
                {
                    new Parameter(StageId, stageStartPayload.StageId.ToString()),
                };

                LogEvent(StageStart, parameters.ToArray());

            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        private void LogEventStageFinished(StageFinishedPayload stageFinishedPayload)
        {
            string eventName = stageFinishedPayload.StarCount > 0 ? StageSuccess :
                StageFailed;
            try
            {
                List<Parameter> parameters = new List<Parameter>
                {
                    new Parameter(eventName, stageFinishedPayload.StageId.ToString()),
                    new Parameter(StarCollect, stageFinishedPayload.StarCount.ToString()),
                };

                LogEvent(eventName, parameters.ToArray());

            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        private static void LogEvent(string eventName, Parameter[] parameters)
        {
            FirebaseAnalytics.LogEvent(eventName, parameters);
            string strEventLog = $"LogEvent :{eventName} ";
            foreach (var parameter in parameters)
            {
                strEventLog += $"{parameter} ,";
            }
            Debug.Log(strEventLog);
        }
#endif
    }
}

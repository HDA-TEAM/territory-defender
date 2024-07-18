using Firebase.Extensions;
using Firebase.RemoteConfig;
using GamePlay.Scripts.Data;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Common.Scripts
{
    public class RemoteConfigManager : MonoBehaviour
    {
        #region Remote keys

        private const string StageDataConfig = "stage_data_config";
        #endregion

        #region Data config
        [SerializeField] private StageDataConfig _stageDataConfig;
        #endregion
        private void Start()
        {
            FetchDataAsync();
        }
        private void FetchDataAsync() {
            Debug.Log("Fetching data...");
            Task fetchTask =
                FirebaseRemoteConfig.DefaultInstance.FetchAsync(
                    TimeSpan.Zero);
            fetchTask.ContinueWithOnMainThread(FetchComplete);
        }
        private void FetchComplete(Task fetchTask) {
            if (!fetchTask.IsCompleted) {
                Debug.LogError("Retrieval hasn't finished.");
                return;
            }

            FirebaseRemoteConfig remoteConfig = FirebaseRemoteConfig.DefaultInstance;
            ConfigInfo info = remoteConfig.Info;
            if(info.LastFetchStatus != LastFetchStatus.Success) {
                Debug.LogError($"{nameof(FetchComplete)} was unsuccessful\n{nameof(info.LastFetchStatus)}: {info.LastFetchStatus}");
                return;
            }

            // Fetch successful. Parameter values must be activated to use.
            remoteConfig.ActivateAsync()
                .ContinueWithOnMainThread(
                    task => {
                        try
                        {
                            string stringStageDataConfig = remoteConfig.GetValue(StageDataConfig).StringValue;
                            _stageDataConfig.SetRemoteConfig(stringStageDataConfig);
                        }
                        catch (Exception e)
                        {
                            Debug.LogError(e);
                        }
                        Debug.Log($"Remote data loaded and ready for use. Last fetch time {info.FetchTime}.");
                    });
        }
    }
}

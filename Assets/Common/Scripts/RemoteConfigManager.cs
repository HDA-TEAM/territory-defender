using Common.Scripts;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using GamePlay.Scripts.Data;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class RemoteConfigManager : SingletonBase<RemoteConfigManager>
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
    public Task FetchDataAsync() {
        Debug.Log("Fetching data...");
        Task fetchTask =
            FirebaseRemoteConfig.DefaultInstance.FetchAsync(
                TimeSpan.Zero);
        return fetchTask.ContinueWithOnMainThread(FetchComplete);
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
    // Invoke the listener.
//     void Start()
//     {
//         FirebaseRemoteConfig.DefaultInstance.OnConfigUpdateListener
//             += ConfigUpdateListenerEventHandler;
//     }
//
// // Handle real-time Remote Config events.
//     void ConfigUpdateListenerEventHandler(
//         object sender, ConfigUpdateEventArgs args) {
//         if (args.Error != RemoteConfigError.None) {
//             Debug.Log($"Error occurred while listening: {args.Error}");
//             return;
//         }
//
//         Debug.Log("Updated keys: " + string.Join(", ", args.UpdatedKeys));
//         // Activate all fetched values and then display a welcome message.
//         remoteConfig.ActivateAsync().ContinueWithOnMainThread(
//             task => {
//                 DisplayWelcomeMessage();
//             });
//     }
//
// // Stop the listener.
//     void OnDestroy() {
//         FirebaseRemoteConfig.DefaultInstance.OnConfigUpdateListener
//             -= ConfigUpdateListenerEventHandler;
//     }
}

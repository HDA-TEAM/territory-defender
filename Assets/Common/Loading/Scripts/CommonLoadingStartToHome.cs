using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common.Loading.Scripts
{
    [CreateAssetMenu(fileName = "CommonLoadingStartToHome", menuName = "ScriptableObject/LoadingScene/CommonLoadingStartToHome")]
    public class CommonLoadingStartToHome : CommonLoadingScene
    {
        private async UniTask LoadingAllLocalData()
        {
            foreach (var localData in _localDataList)
            {
                localData.LoadData();
                Debug.Log("Load " + localData.name);
                await UniTask.WaitUntil(localData.IsDoneLoadData);
                Debug.Log("Done Load " + localData.name);
            }
        }
        private async UniTask LoadingScene()
        {
            Debug.Log("LoadingScene ");
            string sceneLoadingName = SceneIdentified.GetSceneName(ESceneIdentified.Home);
            await SceneManager.LoadSceneAsync(sceneLoadingName, LoadSceneMode.Single);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneLoadingName));
            await UniTask.WaitUntil(() => SceneManager.GetActiveScene().name == sceneLoadingName);
            Debug.Log("Done LoadingScene ");
        }
        public override async void StartLoading(Action onCompleted, IProgress<float> progress)
        {
            _progress = progress;
            _loadingSteps = new List<LoadingStep>
            {
                new LoadingStep
                {
                    Percent = 0.5f,
                    OnAction = LoadingAllLocalData,
                    MinDelayNextStepDuration = 0.5f,
                },
                new LoadingStep
                {
                    Percent = 0.4f,
                    OnAction = LoadingScene,
                    MinDelayNextStepDuration = 0.4f,
                },
                new LoadingStep
                {
                    Percent = 0.1f,
                    OnAction = () => new UniTask(),
                    MinDelayNextStepDuration = 0.1f,
                }
            };

            await ExecuteLoadingStep();
            onCompleted?.Invoke();
        }
    }
}

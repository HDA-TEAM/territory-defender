using Cysharp.Threading.Tasks;
using GamePlay.Scripts.GamePlay;
using SuperMaxim.Messaging;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common.Loading.Scripts
{
    [CreateAssetMenu(fileName = "CommonLoadingGameToHome", menuName = "ScriptableObject/LoadingScene/CommonLoadingGameToHome")]
    public class CommonLoadingGameToHome : CommonLoadingScene
    {
        private async UniTask LoadingScene()
        {
            Messenger.Default.Publish(new ResetGamePayload());
            
            string sceneLoadingName = SceneIdentified.GetSceneName(ESceneIdentified.Home);
            await SceneManager.LoadSceneAsync(sceneLoadingName);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneLoadingName));
            
            await UniTask.WaitUntil(() => SceneManager.GetActiveScene().name == sceneLoadingName);
        }
        public override async void StartLoading(Action onCompleted, IProgress<float> progress)
        {
            _progress = progress;
            _loadingSteps = new List<LoadingStep>
            {
                new LoadingStep
                {
                    Percent = 0.9f,
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

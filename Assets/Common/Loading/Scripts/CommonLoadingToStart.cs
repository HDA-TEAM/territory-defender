using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common.Loading.Scripts
{
    [CreateAssetMenu(fileName = "CommonLoadingToStart", menuName = "ScriptableObject/LoadingScene/CommonLoadingToStart")]
    public class CommonLoadingToStart : CommonLoadingScene
    {
        private async UniTask LoadingAllLocalData()
        {
            foreach (var localData in _localDataList)
            {
                localData.LoadData();
                await UniTask.WaitUntil(localData.IsDoneLoadData);
            }
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
                    Percent = 0.5f,
                    OnAction = () => new UniTask(),
                    MinDelayNextStepDuration = 0.1f,
                }
            };

            await ExecuteLoadingStep();
            onCompleted?.Invoke();
        }
    }
}

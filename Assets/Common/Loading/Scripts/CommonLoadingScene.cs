using Common.Scripts.Data.DataAsset;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Loading.Scripts
{
    public struct LoadingStep
    {
        public float Percent;
        public float MinDelayNextStepDuration;
        public Func<UniTask> OnAction;
    }
    public abstract class CommonLoadingScene : ScriptableObject
    {
        [SerializeField]
        protected DataAsset[] _localDataList;
        protected List<LoadingStep> _loadingSteps;
        protected IProgress<float> _progress;
        // protected abstract void StartLoadingStep(Action onCompleted, IProgress<float> progress);
        public virtual void ReleaseResource()
        {
        }
        public virtual void StartLoading(Action onCompleted,IProgress<float> progress)
        {
            
        }
        protected async UniTask ExecuteLoadingStep()
        {
            float totalLoadingDuration = 0f;
            foreach (var loadingStep in _loadingSteps)
            {
                await loadingStep.OnAction.Invoke();
                await UniTask.Delay(TimeSpan.FromSeconds(loadingStep.MinDelayNextStepDuration));
                totalLoadingDuration += loadingStep.Percent;
                _progress.Report(totalLoadingDuration);
            }
        }
        public virtual bool IsLoaded() => true;
    }
}

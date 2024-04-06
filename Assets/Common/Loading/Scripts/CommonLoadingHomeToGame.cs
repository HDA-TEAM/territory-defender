using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Common.Loading.Scripts
{
    [CreateAssetMenu(fileName = "CommonLoadingHomeToGame", menuName = "ScriptableObject/LoadingScene/CommonLoadingHomeToGame")]
    public class CommonLoadingHomeToGame : CommonLoadingScene
    {
        public override async void StartLoading(Action onCompleted, IProgress<float> progress)
        {
            for (int i = 0; i <= 10; i++)
            {
                progress.Report(i * 0.1f);
                await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
            }
            OnLoadingCompleted?.Invoke();
        }
    }
}

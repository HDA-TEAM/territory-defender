using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common.Loading.Scripts
{
    [CreateAssetMenu(fileName = "CommonLoadingStartToHome", menuName = "ScriptableObject/LoadingScene/CommonLoadingStartToHome")]
    public class CommonLoadingStartToHome : CommonLoadingScene
    {
        public override async void StartLoading(Action onCompleted, IProgress<float> progress)
        {
            
            string sceneLoadingName = SceneIdentified.GetSceneName(ESceneIdentified.Home);
            await SceneManager.LoadSceneAsync(sceneLoadingName, LoadSceneMode.Single);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneLoadingName));
            // todo Wtf in here : why can't unload LoadingScene
            // SceneManager.UnloadSceneAsync(SceneIdentified.GetSceneName(ESceneIdentified.LoadingScene));

            for (int i = 0; i <= 10; i++)
            {
                progress.Report(i * 0.1f);
                await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
            }
            onCompleted?.Invoke();
        }
    }
}

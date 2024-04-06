using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common.Loading.Scripts
{
    [CreateAssetMenu(fileName = "CommonLoadingGameToHome", menuName = "ScriptableObject/LoadingScene/CommonLoadingGameToHome")]
    public class CommonLoadingGameToHome : CommonLoadingScene
    {
        public override async void StartLoading(Action onCompleted, IProgress<float> progress)
        {
            GameController.Instance.ResetGame();
            
            string sceneLoadingName = SceneIdentified.GetSceneName(ESceneIdentified.Home);
            await SceneManager.LoadSceneAsync(sceneLoadingName);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneLoadingName));
            // SceneManager.UnloadSceneAsync(SceneIdentified.GetScenePath(ESceneIdentified.GamePlay));
            
            for (int i = 0; i <= 10; i++)
            {
                progress.Report(i * 0.1f);
                await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
            }
            onCompleted?.Invoke();
        }
    }
}

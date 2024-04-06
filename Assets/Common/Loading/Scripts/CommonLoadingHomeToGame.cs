using Cysharp.Threading.Tasks;
using System;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common.Loading.Scripts
{
    [CreateAssetMenu(fileName = "CommonLoadingHomeToGame", menuName = "ScriptableObject/LoadingScene/CommonLoadingHomeToGame")]
    public class CommonLoadingHomeToGame : CommonLoadingScene
    {
        public override async void StartLoading(Action onCompleted, IProgress<float> progress)
        {
            string sceneLoadingName = SceneIdentified.GetSceneName(ESceneIdentified.GamePlay);
            await SceneManager.LoadSceneAsync(sceneLoadingName);
             SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneLoadingName));
            // SceneManager.UnloadSceneAsync(SceneIdentified.GetSceneName(ESceneIdentified.Home));
            
            GameController.Instance.SetUpNewGame();
            
            for (int i = 0; i <= 10; i++)
            {
                progress.Report(i * 0.1f);
                await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
            }
            onCompleted?.Invoke();
        }
    }
}

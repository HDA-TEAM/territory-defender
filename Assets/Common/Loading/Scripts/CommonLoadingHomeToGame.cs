using Common.Scripts;
using Cysharp.Threading.Tasks;
using GamePlay.Scripts.Data;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common.Loading.Scripts
{
    public enum StageDiff
    {
        Normal = 0,
        Hard = 1,
        Evil = 2,
    }
    
    public struct StartStageComposite
    {
        public StageId StageId;
        public UnitId.Hero HeroId;
        public StageDiff StageDiff;
    }
    [CreateAssetMenu(fileName = "CommonLoadingHomeToGame", menuName = "ScriptableObject/LoadingScene/CommonLoadingHomeToGame")]
    public class CommonLoadingHomeToGame : CommonLoadingScene
    {
        private StartStageComposite _curStartStageComposite;
        public void SetStageInformation(StartStageComposite startStageComposite)
        {
            _curStartStageComposite = startStageComposite;
        }
        public override async void StartLoading(Action onCompleted, IProgress<float> progress)
        {
            string sceneLoadingName = SceneIdentified.GetSceneName(ESceneIdentified.GamePlay);
            await SceneManager.LoadSceneAsync(sceneLoadingName);
             SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneLoadingName));
            // SceneManager.UnloadSceneAsync(SceneIdentified.GetSceneName(ESceneIdentified.Home));
            
            GamePlay.Scripts.GamePlayController.InGameStateController.Instance.SetUpNewGame(_curStartStageComposite);
            
            for (int i = 0; i <= 10; i++)
            {
                progress.Report(i * 0.1f);
                await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
            }
            onCompleted?.Invoke();
        }
    }
}

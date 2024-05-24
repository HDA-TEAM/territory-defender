using Common.Scripts;
using Cysharp.Threading.Tasks;
using GamePlay.Scripts.Data;
using GamePlay.Scripts.GamePlay;
using SuperMaxim.Messaging;
using System;
using System.Collections.Generic;
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
    [Serializable]
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
        private async UniTask LoadingScene()
        {
            string sceneLoadingName = SceneIdentified.GetSceneName(ESceneIdentified.GamePlay);
            await SceneManager.LoadSceneAsync(sceneLoadingName);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneLoadingName));
            
            await UniTask.WaitUntil(() => SceneManager.GetActiveScene().name == sceneLoadingName);
            
            Debug.Log("SetUpNewGamePayload " + _curStartStageComposite.StageId);
            Messenger.Default.Publish(new SetUpNewGamePayload
            {
                StartStageComposite = _curStartStageComposite,
            });
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

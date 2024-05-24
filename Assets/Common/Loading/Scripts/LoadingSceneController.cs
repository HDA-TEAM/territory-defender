using Common.Scripts;
using Cysharp.Threading.Tasks;
using SuperMaxim.Messaging;
using System;
using UnityEngine;

namespace Common.Loading.Scripts
{
    public class LoadingSceneController : SingletonBase<LoadingSceneController>
    {
        [Header("Loading scene")]
        [SerializeField] private CommonLoadingStartToHome _startToHomeCommonLoading;
        [SerializeField] private CommonLoadingHomeToGame _homeToGameCommonLoading;
        [SerializeField] private CommonLoadingGameToHome _gameToHomeCommonLoading;

        [SerializeField] private LoadingSceneModelView _loadingSceneModelView;
        
        [Header("Sounds"), Space(12)]
        [SerializeField] private AudioClip _audioClipBMGHome;
        [SerializeField] private AudioClip _audioClipBMGGamePlay;
        
        private void Start()
        {
            LoadingStartToHome();
        }
        private void LoadingStartToHome()
        {
            Messenger.Default.Publish(new AudioPlayLoopPayload
            {
                AudioClip = _audioClipBMGHome
            });
            
            _loadingSceneModelView.ShowLoadingScene();
            
            IProgress<float> progress = Progress.Create<float>(x => _loadingSceneModelView.UpdateProgress(x));
            
            _startToHomeCommonLoading.StartLoading(_loadingSceneModelView.HidingLoadingScene, progress: progress);
        }
        public void LoadingGameToHome()
        {
            Messenger.Default.Publish(new AudioPlayLoopPayload
            {
                AudioClip = _audioClipBMGHome
            });
            
            _loadingSceneModelView.ShowLoadingScene();
            
            var progress = Progress.Create<float>(x => _loadingSceneModelView.UpdateProgress(x));
            
            _gameToHomeCommonLoading.StartLoading(_loadingSceneModelView.HidingLoadingScene, progress: progress);
        }
        
        public void LoadingHomeToGame(StartStageComposite startStageComposite)
        {
            Messenger.Default.Publish(new AudioPlayLoopPayload
            {
                AudioClip = _audioClipBMGGamePlay,
            });
            
            _loadingSceneModelView.ShowLoadingScene();
            
            var progress = Progress.Create<float>(x => _loadingSceneModelView.UpdateProgress(x));
            _homeToGameCommonLoading.SetStageInformation(startStageComposite);
            _homeToGameCommonLoading.StartLoading(_loadingSceneModelView.HidingLoadingScene, progress: progress);
        }
    }
}

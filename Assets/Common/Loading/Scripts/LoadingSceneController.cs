using Cysharp.Threading.Tasks;
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

        private void Start()
        {
            LoadingStartToHome();
        }
        public void LoadingStartToHome()
        {
            _loadingSceneModelView.ShowLoadingScene();
            
            var progress = Progress.Create<float>(x => _loadingSceneModelView.UpdateProgress(x));
            
            _startToHomeCommonLoading.StartLoading(_loadingSceneModelView.HidingLoadingScene, progress: progress);
        }
        public void LoadingGameToHome()
        {
            _loadingSceneModelView.ShowLoadingScene();
            
            var progress = Progress.Create<float>(x => _loadingSceneModelView.UpdateProgress(x));
            
            _gameToHomeCommonLoading.StartLoading(_loadingSceneModelView.HidingLoadingScene, progress: progress);
        }
        
        public void LoadingHomeToGame(StartStageComposite startStageComposite)
        {
            _loadingSceneModelView.ShowLoadingScene();
            
            var progress = Progress.Create<float>(x => _loadingSceneModelView.UpdateProgress(x));
            _homeToGameCommonLoading.SetStageInformation(startStageComposite);
            _homeToGameCommonLoading.StartLoading(_loadingSceneModelView.HidingLoadingScene, progress: progress);
        }
    }
}

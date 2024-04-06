using CustomInspector;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Common.Loading.Scripts
{
    public class LoadingSceneController : SingletonBase<LoadingSceneController>
    {
        [Button("LoadingHomeToGame")]
        
        [Header("Loading scene")]
        [SerializeField] private CommonLoadingScene _startToHomeCommonLoading;
        [SerializeField] private CommonLoadingScene _homeToGameCommonLoading;
        [SerializeField] private CommonLoadingScene _gameToHomeCommonLoading;

        [SerializeField] private LoadingSceneModelView _loadingSceneModelView;
        
        public void LoadingGameToHome()
        {
            
        }
        
        public void LoadingHomeToGame()
        {
            _loadingSceneModelView.ShowLoadingScene();
            
            var progress = Progress.Create<float>(x => _loadingSceneModelView.UpdateProgress(x));
            
            _homeToGameCommonLoading.StartLoading(_loadingSceneModelView.HidingLoadingScene, progress: progress);
        }
    }
}

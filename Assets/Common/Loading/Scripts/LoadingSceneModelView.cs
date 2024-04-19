using Common.Scripts;
using SuperMaxim.Messaging;
using UnityEngine;

namespace Common.Loading.Scripts
{
    public class LoadingSceneModelView : MonoBehaviour
    {
        [SerializeField] private float _durationPerUnitProgressBar = 0.05f;
        [SerializeField] private float _showLoadingSceneDuration = 0.2f;
        [SerializeField] private float _hidingLoadingSceneDuration = 0.1f;
        [SerializeField] private LoadingSceneView _loadingSceneView;
        
        [Header("Sounds"), Space(12)]
        [SerializeField] private AudioClip _audioClipShowingLoadingScene;

        public void ShowLoadingScene()
        {
            Messenger.Default.Publish(new AudioPlayOneShotPayload
            {
                AudioClip = _audioClipShowingLoadingScene
            });
            
            _loadingSceneView.SetupBlockRaycast(true);
            _loadingSceneView.UpdateProgressBar(0f, 0f);
            _loadingSceneView.PlayDoFadeEffect(1f, 1f, _showLoadingSceneDuration);
        }
        public void HidingLoadingScene()
        {
            _loadingSceneView.PlayDoFadeEffect(1f, 0f, _hidingLoadingSceneDuration);
            _loadingSceneView.SetupBlockRaycast(false);
        }
        public void UpdateProgress(float val)
        {
            _loadingSceneView.UpdateProgressBar(val, _durationPerUnitProgressBar);
        }
    }
}

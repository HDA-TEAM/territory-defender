using UnityEngine;

namespace Common.Loading.Scripts
{
    public class LoadingSceneModelView : MonoBehaviour
    {
        [SerializeField] private float _durationPerUnitProgressBar = 0.05f;
        [SerializeField] private float _showLoadingSceneDuration = 0.2f;
        [SerializeField] private float _hidingLoadingSceneDuration = 0.1f;
        [SerializeField] private LoadingSceneView _loadingSceneView;

        public void ShowLoadingScene()
        {
            _loadingSceneView.UpdateProgressBar(0f, _durationPerUnitProgressBar);
            _loadingSceneView.PlayDoFadeEffect(0f, 1f, _showLoadingSceneDuration);
        }
        public void HidingLoadingScene()
        {
            _loadingSceneView.PlayDoFadeEffect(1f, 0f, _hidingLoadingSceneDuration);
        }
        public void UpdateProgress(float val)
        {
            _loadingSceneView.UpdateProgressBar(val, _durationPerUnitProgressBar);
        }
    }
}

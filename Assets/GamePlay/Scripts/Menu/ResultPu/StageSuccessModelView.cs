using Common.Loading.Scripts;
using UnityEngine;

public class StageSuccessModelView : MonoBehaviour
{
    [SerializeField] private StageSuccessView _stageSuccessView;
    
    public void SetupView(int claimingStars)
    {
        _stageSuccessView.Setup(OnClickContinue, claimingStars);
    }
    private void OnClickContinue()
    {
        // Load scene home
        LoadingSceneController.Instance.LoadingGameToHome();
    }
}

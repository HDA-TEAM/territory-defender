using UnityEngine;

public class StageSuccessModelView : MonoBehaviour
{
    [SerializeField] private StageSuccessView _stageSuccessView;
    
    public void SetupView(int claimingStars)
    {
        _stageSuccessView.Setup(OnClickQuit, claimingStars);
    }
    private void OnClickQuit()
    {

    }
}

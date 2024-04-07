using UnityEngine;

public class StageSuccessPu : GamePlayPopup
{
    [SerializeField] private StageSuccessModelView _stageSuccessModelView;
    
    public void SetupData(int claimingStars)
    {
        _stageSuccessModelView.SetupView(claimingStars);
    }
}

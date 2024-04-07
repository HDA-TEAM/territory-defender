using CustomInspector;
using UnityEngine;

public class StageSuccessPu : GamePlayPopup
{
    [Button("SetupData", usePropertyAsParameter: true)]
    [SerializeField] private int _testClaimingStars;
    
    [SerializeField] private StageSuccessModelView _stageSuccessModelView;
    
    public void SetupData(int claimingStars)
    {
        _stageSuccessModelView.SetupView(claimingStars);
    }
}

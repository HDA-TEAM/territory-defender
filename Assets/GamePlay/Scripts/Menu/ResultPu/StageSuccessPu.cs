using Common.Scripts.Navigator;
using CustomInspector;
using UnityEngine;

public class StageSuccessPu : CommonModal
{
    [SerializeField] private StageSuccessModelView _stageSuccessModelView;
    
    public void SetupData(int claimingStars)
    {
        _stageSuccessModelView.SetupView(claimingStars);
    }
    
#if UNITY_EDITOR
    [Button("SetupData", usePropertyAsParameter: true)]
    [SerializeField] private int _testClaimingStars;
#endif
}

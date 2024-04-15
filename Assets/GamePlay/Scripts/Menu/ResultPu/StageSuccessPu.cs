using CustomInspector;
using UnityEngine;

public class StageSuccessPu : GamePlayPopup
{
    [SerializeField] private StageSuccessModelView _stageSuccessModelView;

    protected override void OnEnable()
    {
    }
    protected override void OnDisable()
    {
    }
    public void SetupData(int claimingStars)
    {
        _stageSuccessModelView.SetupView(claimingStars);
    }
    
#if UNITY_EDITOR
    [Button("SetupData", usePropertyAsParameter: true)]
    [SerializeField] private int _testClaimingStars;
#endif
}

using Common.Scripts.Navigator;
using CustomInspector;
using GamePlay.Scripts.Data;
using UnityEngine;

namespace GamePlay.Scripts.Menu.ResultPu
{
    public class StageSuccessPu : CommonModal
    {
        [SerializeField] private StageSuccessModelView _stageSuccessModelView;
        [SerializeField] private StageDataAsset _stageDataAsset;
        public void SetupData(StagePassed stagePassed)
        {
            _stageDataAsset.AddStagePassed(stagePassed);
            _stageSuccessModelView.SetupView(stagePassed.TotalStar);
        }
    
#if UNITY_EDITOR
        [Button("SetupData", usePropertyAsParameter: true)]
        [SerializeField] private StagePassed _testStagePassed;
#endif
    }
}

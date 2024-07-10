using Common.Scripts.Data.DataAsset;
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
            int icrStar = stagePassed.TotalStar;
            if (_stageDataAsset.ListStagePassed.Exists(passed => passed.StageId == stagePassed.StageId))
            {
                int prevStar = _stageDataAsset.ListStagePassed.Find(passed => passed.StageId == stagePassed.StageId).TotalStar;
                if (prevStar < stagePassed.TotalStar)
                    icrStar = stagePassed.TotalStar - prevStar;
            }
            _stageDataAsset.AddStagePassed(stagePassed);
            _stageSuccessModelView.SetupView(stagePassed.TotalStar, incStars: icrStar);
        }

#if UNITY_EDITOR
        [Button("SetupData", usePropertyAsParameter: true)]
        [SerializeField] private StagePassed _testStagePassed;
#endif
    }
}

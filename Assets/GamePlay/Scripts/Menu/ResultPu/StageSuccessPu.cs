using Common.Scripts.Data.DataAsset;
using Common.Scripts.Navigator;
using CustomInspector;
using GamePlay.Scripts.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace GamePlay.Scripts.Menu.ResultPu
{
    public class StageSuccessPu : CommonModal
    {
        [SerializeField] private StageSuccessModelView _stageSuccessModelView;
        [SerializeField] private StageDataAsset _stageDataAsset;
        public void SetupData(StageData stageData)
        {
            int icrStar = stageData.TotalStar;
            if (_stageDataAsset.StageDataList.Exists(passed => passed.StageId == stageData.StageId))
            {
                int prevStar = _stageDataAsset.StageDataList.Find(passed => passed.StageId == stageData.StageId).TotalStar;
                if (prevStar < stageData.TotalStar)
                    icrStar = stageData.TotalStar - prevStar;
            }
            _stageDataAsset.AddStagePassed(stageData);
            _stageSuccessModelView.SetupView(stageData.TotalStar, incStars: icrStar);
        }

#if UNITY_EDITOR
        [FormerlySerializedAs("_testStagePassed")]
        [Button("SetupData", usePropertyAsParameter: true)]
        [SerializeField] private StageData _testStageData;
#endif
    }
}

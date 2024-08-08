using GamePlay.Scripts.Data;
using System.Collections.Generic;

namespace Features.Home.Scripts.HomeScreen.Common
{
    public class StageDataAdapter
    {
        public List<StageComposite> GetStageComposites(StageDataAsset stageDataAsset)
        {
            List<StageComposite> curStageComposite = new List<StageComposite>();
            foreach (var stageDataSo in stageDataAsset.StageDataList)
            {
                curStageComposite.Add(new StageComposite
                {
                    StageId = stageDataSo.StageId,
                    StageStar = stageDataSo.TotalStar,
                });
            }
            return curStageComposite;
        }
    }
}

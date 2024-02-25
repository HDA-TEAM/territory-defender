using System.Collections.Generic;
using UnityEngine;

public class StageDataManager : SingletonBase<StageDataManager>
{
    public List<StageComposite> StageComposites { get; private set; }
    public StageComposite CurrentStage { get; set; }

    public override void Awake()
    {
        base.Awake();
        LoadStageData();
    }

    private void LoadStageData()
    {
        if (StageComposites == null)
            StageComposites = new List<StageComposite>();

        else StageComposites.Clear();
        
        // TODO 1: Add 1 condition to check if StageDataAsset == null
            
        // TODO 2: Implement 2 stage example for test in StageInfo feature
        StageComposites.Add(
            new StageComposite
            {
                StageId = 1,
                StageStar = 2,
                StageType = "normal",
                StageName = "VUNG DAT DO",
            }
        );
        
        StageComposites.Add(
            new StageComposite
            {
                StageId = 2,
                StageStar = 0,
                StageType = "boss",
                StageName = "VUNG DAT DO BOSS"
            }
        );
    }
    
}


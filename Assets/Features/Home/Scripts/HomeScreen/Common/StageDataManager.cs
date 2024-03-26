using System.Collections.Generic;
public class StageDataManager : SingletonBase<StageDataManager>
{
    public List<StageComposite> StageComposites { get; private set; }
    public StageComposite CurrentStage { get; set; }

    protected override void Awake()
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
                StageName = "VUNG DAT DO",
                StageState = true,
            }
        );
        
        StageComposites.Add(
            new StageComposite
            {
                StageId = 1,
                StageStar = 1,
                StageName = "VUNG DAT DO 2",
                StageState = true,
            }
        );
        
        StageComposites.Add(
            new StageComposite
            {
                StageId = 3,
                StageStar = 0,
                StageName = "VUNG DAT DO BOSS",
                StageState = false,
            }
        );
    }
    
}


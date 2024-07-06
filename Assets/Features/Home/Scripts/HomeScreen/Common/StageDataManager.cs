using Common.Scripts;
using Common.Scripts.Data;
using System.Collections.Generic;
using GamePlay.Scripts.Data;
using UnityEngine;

public class StageDataManager : SingletonBase<StageDataManager>
{
    [Header("Data"), Space(12)] [SerializeField]
    private StageDataAsset _stageDataAsset;
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
        if (_stageDataAsset == null)
            return;
        
        // TODO 2: Implement 2 stage example for test in StageInfo feature
        List<StageDataSO> listStageDataSo = _stageDataAsset.GetAllStageData();

        foreach (var stageDataSo in listStageDataSo)
        {
            StageComposites.Add(new StageComposite
            {
                StageId = stageDataSo._stageId,
                StageStar = stageDataSo._stageStar,
                StageName = stageDataSo._stageName.ToUpper(),
                StageState = stageDataSo._stageState,
                
            });
        }
    }
    
}


using Common.Scripts;
using Common.Scripts.Data;
using System.Collections.Generic;
using GamePlay.Scripts.Data;
using UnityEngine;

public class StageDataController : SingletonBase<StageDataController>
{
    [Header("Data"), Space(12)]
    [SerializeField] private StageDataAsset _stageDataAsset;
    [SerializeField] private List<StageComposite> _curStageComposite = new List<StageComposite>();
    
    public StageComposite CurrentStage;
    public List<StageComposite> StageComposites
    {
        get
        {
            if (_curStageComposite.Count > 0)
                return _curStageComposite;
                
            InitStageData();
            return _curStageComposite;
        }
    }
    private void InitStageData()
    {
        foreach (var stageDataSo in _stageDataAsset.StageDataList)
        {
            _curStageComposite.Add(new StageComposite
            {
                StageId = stageDataSo.StageId,
                StageStar = stageDataSo.TotalStar,
            });
            
        }
    }
    
}


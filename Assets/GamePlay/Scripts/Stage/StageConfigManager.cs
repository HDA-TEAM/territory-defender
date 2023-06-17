using GamePlay.Scripts.Tower;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum StageChapterKey
{   
    chap_1 = 1,
}
public enum StageIdKey
{   
    stage_1 = 1,
}
public class StageConfigManager : MonoBehaviour
    {
        [SerializeField] private List<StageConfig> stages;
        // [SerializeField] private TowerKITManager towerKitManager;
        // [SerializeField] private RouteSetManager routeSetManager;
        // [SerializeField] private TowerKitSetConfigManager _towerKitSetConfigManager;
        // [SerializeField] private int currentStageId;
        // public void ChangeStage(TMP_Text stageIdText)
        // {
        //     // string preText = StringUtility.PrepareConvertIntValue(stageIdText.GetParsedText());
        //     int stageId = Int32.Parse(stageIdText.text);
        //     if (stageId > 0 && stageId <= stages.Count)
        //     {
        //         StageConfig stageConfig = FindStageConfig(stageId);
        //         if (stageConfig != null)
        //         {
        //             currentStageId = stageId;
        //             RelocateAndActiveTowerKit(stageConfig);
        //         }
        //         else
        //         {
        //             Debug.Log("Dont exist this stage id");
        //         }
        //     }
        // }
        public StageConfig FindStageConfig(StageIdKey stageId, StageChapterKey chapterKey)
        {
            foreach (var stageConfig in stages)
            {
                if (stageConfig.StageIdKey == stageId && stageConfig.StageChapterKey == chapterKey)
                {
                    return stageConfig;
                }
            }
            return null;
        }
        // public void SaveChangeOnCurrentStage()
        // {
        //     
        //     StageConfig stageConfig = stages[currentStageId - 1];
        //     stageConfig.SaveTowerKITsPosition(_towerKitManager.TowerKits);
        // }
        // public void LoadFromCurrentStage()
        // {
        //     foreach (var tk in _towerKitManager.TowerKits)
        //     {
        //         tk.gameObject.SetActive(false);
        //     }
        //     Debug.Log(_towerKitManager.TowerKits.Count);
        //     
        //     stageConfig.LoadTowerKITsPosition(_towerKitManager.TowerKits);
        // }
    }

using GamePlay.Scripts.Tower;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageConfigManager : MonoBehaviour
    {
        [SerializeField] private List<StageConfig> stages;
        [SerializeField] private TowerKITManager _towerKitManager;
        [SerializeField] private int currentStageId;
        public void ChangeStage(TMP_Text stageIdText)
        {
            string preText = StringUtility.PrepareConvertIntValue(stageIdText.GetParsedText());
            int stageId = Int32.Parse(preText);
            if (stageId > 0 && stageId <= stages.Count)
            {
                StageConfig stageConfig = FindStageConfig(stageId);
                if (stageConfig != null)
                {
                    currentStageId = stageId;
                    RelocateAndActiveTowerKit(stageConfig);
                }
                else
                {
                    Debug.Log("Dont exist this stage id");
                }
            }
        }
        private StageConfig FindStageConfig(int stageId)
        {
            foreach (var stageConfig in stages)
            {
                if (stageConfig.stageId == stageId)
                {
                    return stageConfig;
                }
            }
            return null;
        }
        public void SaveChangeOnCurrentStage()
        {
            StageConfig stageConfig = stages[currentStageId - 1];
            stageConfig.SaveTowerKITsPosition(_towerKitManager.TowerKits);
        }
        public void RelocateAndActiveTowerKit(StageConfig stageConfig)
        {
            foreach (var tk in _towerKitManager.TowerKits)
            {
                tk.gameObject.SetActive(false);
            }
            Debug.Log(_towerKitManager.TowerKits.Count);
            
            stageConfig.LoadTowerKITsPosition(_towerKitManager.TowerKits);
        }
    }

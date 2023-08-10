using System;
using UnityEngine;
using UnityEngine.UI;

public class InGamePlay : MonoBehaviour
{
    [SerializeField] private StageConfigManager stageConfigManager;
    [SerializeField] private RouteSetController routeSetController;
    [SerializeField] private TowerKitSetController towerKitSetController;
    [SerializeField] private Button callWave;
    private StageConfig currentStageConfig;
    [SerializeField] private StageSpawningInformation stageSpawningInformation;
    private void Awake()
    {
        SetUpStageConfig();
        callWave.onClick.AddListener(OnCallWave);
    }
    private void SetUpStageConfig()
    {
        currentStageConfig = stageConfigManager.FindStageConfig(StageIdKey.stage_1, ChapterKey.chap_1);
        currentStageConfig.SaveToOS(
            towerKitSetController.CurrentTowerKits, 
            routeSetController.CurrentRouteLineRenderers);
        currentStageConfig.LoadFormOs(
            towerKitSetController.CurrentTowerKits, 
            routeSetController.CurrentRouteLineRenderers);
    }
    private void OnCallWave()
    {
        StartSpawning();
    }
    private void StartSpawning()
    {
        Debug.Log("Start Spawning");
        stageSpawningInformation.StartSpawning();
    }
}

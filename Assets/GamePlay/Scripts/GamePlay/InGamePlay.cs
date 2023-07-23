using System;
using UnityEngine;

public class InGamePlay : MonoBehaviour
{
    [SerializeField]
    private StageConfigManager stageConfigManager;
    private StageConfig currentStageConfig;
    [SerializeField] private RouteSetController routeSetController;
    [SerializeField] private TowerKitSetController towerKitSetController;
    private void Awake()
    {
        // Set up stage config
        SetUpStageConfig();
    }
    void SetUpStageConfig()
    {
        currentStageConfig = stageConfigManager.FindStageConfig(StageIdKey.stage_1, StageChapterKey.chap_1);
        currentStageConfig.LoadFormOs(
            towerKitSetController.CurrentTowerKits, 
            routeSetController.CurrentRouteLineRenderers);
        foreach (var i in towerKitSetController.CurrentTowerKits)
        {
            Debug.Log("kit pos "+ i.transform.position.z);
        }
    }
}

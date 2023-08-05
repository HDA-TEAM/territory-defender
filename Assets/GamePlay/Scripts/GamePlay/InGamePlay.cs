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
    
    private void Awake()
    {
        // Set up stage config
        SetUpStageConfig();
        callWave.onClick.AddListener(OnCallWave);
    }
    private void SetUpStageConfig()
    {
        currentStageConfig = stageConfigManager.FindStageConfig(StageIdKey.stage_1, StageChapterKey.chap_1);
        currentStageConfig.LoadFormOs(
            towerKitSetController.CurrentTowerKits, 
            routeSetController.CurrentRouteLineRenderers);
    }
    private void OnCallWave()
    {
        GameObject go = PoolingManager.Instance.SpawnObject(PoolingTypeEnum.EnemyShieldMan,this.transform.position);
        go.TryGetComponent<EnemyMovement>(out EnemyMovement component);
        component.RouteToGate = routeSetController.CurrentRouteLineRenderers[0];
    }
}

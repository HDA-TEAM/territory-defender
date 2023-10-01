using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InGamePlay : MonoBehaviour
{
    [SerializeField] private bool IsWantSaveToOS;
    
    [SerializeField] private StageConfigManager stageConfigManager;
    [SerializeField] private RouteSetController routeSetController;
    [SerializeField] private TowerKitSetController towerKitSetController;
    [SerializeField] private Button callWave;
    private OldStageConfig _currentOldStageConfig;
    [FormerlySerializedAs("stageSpawningInformation")]
    [SerializeField] private StageSpawningConfig stageSpawningConfig;
    private void Awake()
    {
        SetUpStageConfig();
        callWave.onClick.AddListener(OnCallWave);
    }
    private void SetUpStageConfig()
    {
        _currentOldStageConfig = stageConfigManager.FindStageConfig(StageIdKey.stage_1, ChapterKey.chap_1);
        // if (IsWantSaveToOS)
        // {
        //     _currentOldStageConfig.SaveToOS(
        //         towerKitSetController.CurrentTowerKits, 
        //         routeSetController.CurrentRouteLineRenderers);   
        // }
        // _currentOldStageConfig.LoadFormOs(
        //     towerKitSetController.CurrentTowerKits, 
        //     routeSetController.CurrentRouteLineRenderers);
    }
    private void OnCallWave()
    {
        StartSpawning();
    }
    private void StartSpawning()
    {
        Debug.Log("Start Spawning");
        stageSpawningConfig.StartSpawning();
    }
}

using Common.Scripts;
using GamePlay.Scripts.Character.Stats;
using GamePlay.Scripts.Tower.TowerKIT.PreviewTooltip;
using GamePlay.Scripts.Tower.TowerKIT.TowerTools;
using SuperMaxim.Messaging;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgradeTool : TowerToolBase
{

    [Header("Sounds"), Space(12)]
    [SerializeField] private AudioClip _audioClipBuilding;

    private UnitBase _towerCanBuild;
    private UnitId.Tower _towerCanBuildId;
    
    protected void OnEnable()
    {
        _towerToolStatusHandle.SetUp(GetTowerToolStatus());
    }
    private TowerTooltatus GetTowerToolStatus()
    {
        UnitId.Tower curTowerId = _towerKit.GetTowerId();
        UnitId.Tower nextTowerId = GetNextUpgradableTower(curTowerId);
        // not exist next tower
        if (nextTowerId == curTowerId)
            return TowerTooltatus.Block;
        
        _towerCanBuildId = nextTowerId;
        return IsEnoughCurrency(nextTowerId) ? TowerTooltatus.Available : TowerTooltatus.UnAvailable;
    }
    private UnitId.Tower GetNextUpgradableTower(UnitId.Tower towerId)
    {
        List<UnitId.Tower> nextUpgradableTowers = _towerDataConfigBase.NextAvailableUpgradeTowers.GetSingleNextAvailableUpgradeTowers(towerId);
        
        if (nextUpgradableTowers == null || nextUpgradableTowers.Count <= 0)
            return towerId;
        
        return nextUpgradableTowers[0];
    }
    private bool IsEnoughCurrency(UnitId.Tower towerId)
    {
        // Checked enough coin to upgrade
        _towerCanBuild = _towerDataConfigBase.GetConfigByKey(towerId).UnitBase;
        StatsHandlerComponent towerStats = _towerCanBuild.UnitStatsHandlerComp();
        return towerStats.GetCurrentStatValue(StatId.CoinNeedToBuild) <= _inGameResourceRuntimeData.GetCurrencyValue();
    }
    protected override void ApplyTool()
    {
        GameObject tower = Instantiate(_towerCanBuild.gameObject);
        _towerKit.SetTower(tower, _towerCanBuildId);

        Messenger.Default.Publish(new AudioPlayOneShotPayload
        {
            AudioClip = _audioClipBuilding,
        });
    }
    protected override void ShowPreviewChanging()
    {
        _towerKit.ShowPreviewChanging(
            new TowerPreviewBuiltTowerToolTip(_towerDataConfigBase, _towerCanBuildId)
        );
    }
}

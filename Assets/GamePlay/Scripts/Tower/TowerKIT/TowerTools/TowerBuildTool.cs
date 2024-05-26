using Common.Scripts;
using GamePlay.Scripts.Character.Stats;
using GamePlay.Scripts.Tower.TowerKIT.PreviewTooltip;
using SuperMaxim.Messaging;
using UnityEngine;

namespace GamePlay.Scripts.Tower.TowerKIT.TowerTools
{
    public class TowerBuildTool : TowerToolBase
    {
        [SerializeField] private UnitId.Tower _towerBuildId;
        
        [Header("Sounds"), Space(12)]
        [SerializeField] private AudioClip _audioClipBuilding;
        
        private UnitBase _towerCanBuild;
        protected void OnEnable()
        {
            _towerToolStatusHandle.SetUp(CheckCurrencyIsEnough() ? TowerTooltatus.Available : TowerTooltatus.UnAvailable);
        }
        private bool CheckCurrencyIsEnough()
        {
            // Checked enough coin to build
            _towerCanBuild = _towerDataConfigBase.GeConfigByKey(_towerBuildId).UnitBase;
            StatsHandlerComponent towerStats = _towerCanBuild.UnitStatsHandlerComp();
            return towerStats.GetCurrentStatValue(StatId.CoinNeedToBuild) <= _inGameResourceRuntimeData.GetCurrencyValue();
        }
        protected override void ApplyTool()
        {
            _towerCanBuild.UnitController().enabled = true;
            GameObject tower = Instantiate(_towerCanBuild.gameObject);
            _towerKit.SetTower(tower, _towerBuildId);
            
            Messenger.Default.Publish(new AudioPlayOneShotPayload
            {
                AudioClip = _audioClipBuilding,
            });
        }
        protected override void ShowPreviewChanging()
        {
            _towerKit.ShowPreviewChanging(
                new TowerPreviewBuiltTowerToolTip(_towerDataConfigBase, _towerBuildId)
            );
            _towerCanBuild.UnitController().enabled = false;
            GameObject tower = Instantiate(_towerCanBuild.gameObject);
            _towerKit.SetPreviewTower(tower, _towerBuildId);

        }
    }
}

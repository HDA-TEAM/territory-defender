using GamePlay.Scripts.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace GamePlay.Scripts.Tower.TowerKIT.TowerTools
{
    public enum TowerToolType
    {
        Build = 10,
        Upgrade = 20,
        Sold = 30,
        Guarding = 40,
    }

    public abstract class TowerToolBase : MonoBehaviour
    {
        [SerializeField] protected TowerToolStatusHandle _towerToolStatusHandle;
        [FormerlySerializedAs("_towerDataConfig")]
        [SerializeField] protected TowerDataConfigBase _towerDataConfigBase;
        [SerializeField] protected InGameInventoryRuntimeData _inGameInventoryRuntimeData;
        [SerializeField] protected ConfirmHandle _confirmHandle;
        protected TowerKit _towerKit;
        public void SetUp(TowerKit towerKit)
        {
            _towerKit = towerKit;
        }
        public void Reset() => _confirmHandle = gameObject.GetComponent<ConfirmHandle>();
        private void Start()
        {
            _confirmHandle.SetUpTool(ApplyTool, ShowPreviewChanging);
        }
        protected virtual void ShowPreviewChanging(){}
        protected virtual void ApplyTool() {}
    }
// public class TowerUpgradeTool : TowerToolBase
// {
//     [SerializeField] private TowerId _towerBuildId;
//     public override void Apply()
//     { 
//         TowerBase towerBase = _towerDataAsset.GetTowerType(_towerBuildId);
//         _towerDataAsset.CurrentSelectedTowerKit.BuildTowerBase(towerBase);
//     }
// }
}
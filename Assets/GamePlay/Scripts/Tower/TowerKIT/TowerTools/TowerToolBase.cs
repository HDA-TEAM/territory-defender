using System;
using UnityEngine;
using UnityEngine.Serialization;

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
    [FormerlySerializedAs("_towerDataAsset")]
    [SerializeField] protected TowerDataConfig _towerDataConfig;
    [SerializeField] protected InGameInventoryRuntimeData _inGameInventoryRuntimeData;
    [SerializeField] protected ConfirmHandle _confirmHandle;
    public void Reset() => _confirmHandle = gameObject.GetComponent<ConfirmHandle>();
    private void Start() => _confirmHandle.SetUpTool(Apply);
    protected virtual void Apply() {}
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

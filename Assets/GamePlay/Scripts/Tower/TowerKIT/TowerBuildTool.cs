using UnityEngine;

public class TowerBuildTool : TowerToolBase
{
    [SerializeField] private TowerId _towerBuildId;
    protected override void Apply()
    { 
        TowerBase towerBase = _towerDataAsset.GetTowerType(_towerBuildId);
        GameObject tower = Instantiate(towerBase.gameObject);
        TowerKitSetController.Instance.CurrentSelectedKit.SetTower(tower);
    }
}

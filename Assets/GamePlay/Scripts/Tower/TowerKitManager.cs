using System;
using UnityEngine;


// public enum TowerKitStepEnum
// {
//     PrepareToBuild = 1,
//     UsingTools = 2,
//     Reset = 3,
// }

public class TowerKitManager : MonoBehaviour
{
    [SerializeField] private TowerKitBuildManager towerKitBuildManager;
    [SerializeField] private TowerKitToolsManager towerKitToolsManager;
    private void Start()
    {
        SetupBuildKit();
    }
    public void SetupToolsKit(TowerBase towerBase)
    {
        towerKitToolsManager.SetUpTools(towerBase);
    }
    public void SetupBuildKit()
    {
        towerKitBuildManager.Setup(this);
    }
}

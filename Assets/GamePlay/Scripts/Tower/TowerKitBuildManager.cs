using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable] 
public struct TowerCanBuild
{
    public ButtonBuildTower button;
    public GameObject gameObject;
}

// public interface TowerKitStep
// {
//     public void ExecuteStep();
//     public void GoingToNextStep();
// }
public class TowerKitBuildManager: MonoBehaviour
{
    private TowerKitManager parentKitManager;
    [SerializeField] private GameObject flagObject;
    [SerializeField] private List<TowerCanBuild> towersCanBuild;
    // public bool IsBuild = false;
    private Vector2 _place;

    public void Setup(TowerKitManager parent)
    {
        parentKitManager = parent;
        ResetCheckWantToBuild();
    }
    private void ResetCheckWantToBuild()
    {
        foreach (var towerCanBuild in towersCanBuild)
        {
            towerCanBuild.button.ResetToDefault();
            towerCanBuild.button.buttonBuild.onClick.AddListener(delegate { CheckWantToBuild(towerCanBuild); });
        }
    }
    private void CheckWantToBuild(TowerCanBuild towerCanBuild)
    {
        if (towerCanBuild.button.IsAccepted() == true)
        {
            BuildTower(towerCanBuild);
            ResetCheckWantToBuild();
        }
        else
        {
            ResetCheckWantToBuild();
            towerCanBuild.button.OnHandleAccepted();
        }
    }
    private void BuildTower(TowerCanBuild towerCanBuild)
    {
        GameObject tower = Instantiate(towerCanBuild.gameObject,this.transform,false);
        
        var towerScript = tower.GetComponent<TowerBase>();
        towerScript.TowerBuild(parentKitManager);

        DeactivateBuildKit();
        
        parentKitManager.SetupToolsKit(towerScript);
        
        //todo
        // reduce coin
    }
    public void DeactivateBuildKit()
    {
        this.gameObject.SetActive(false);
        flagObject.gameObject.SetActive(false);
    }
    public void ResetTowerKitStatus()
    {
        this.gameObject.SetActive(true);
    }
}

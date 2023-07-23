using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
// public interface TowerKitBehavious
// {
//     public void SetupFlag(Button flag, GameObject TowerBehavious)
//     {
//         flag.onClick.RemoveAllListeners();
//         flag.onClick.AddListener(delegate
//         {
//             flag.gameObject.SetActive(false);
//             TowerBehavious.gameObject.SetActive(true);
//         });
//     }
// }
public class TowerKitBuildManager: MonoBehaviour
{
    private TowerKitManager parentKitManager;
    public GameObject content;
    // [SerializeField] private Button flagObject;
    [SerializeField] private List<TowerCanBuild> towersCanBuild;
    // public bool IsBuild = false;
    private Vector2 _place;

    public void Setup(TowerKitManager parent)
    {
        ResetListener();
        // flagObject.onClick.AddListener(ToolKitTurnControl);
        parentKitManager = parent;
        ResetTowerKitStatus();
        ResetCheckWantToBuild();
    }
    void ResetListener()
    {
        // flagObject.onClick.RemoveAllListeners();
    }
    private void ToolKitTurnControl()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
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
        
        var towerScript = tower.GetComponent<TowerBaseOld>();
        towerScript.TowerBuild(parentKitManager);

        DeactivateBuildKit();

        parentKitManager.SetupToolsKit(towerScript);
        
        //todo
        // reduce coin
    }
    public void DeactivateBuildKit()
    {
        this.gameObject.SetActive(false);
        content.gameObject.SetActive(false);
        // flagObject.gameObject.SetActive(false);
    }
    public void ResetTowerKitStatus()
    {
        this.gameObject.SetActive(true);
        content.gameObject.SetActive(false);
    }
}

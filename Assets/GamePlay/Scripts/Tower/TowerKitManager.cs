using System;
using UnityEngine;
using UnityEngine.UI;


// public enum TowerKitStepEnum
// {
//     PrepareToBuild = 1,
//     UsingTools = 2,
//     Reset = 3,
// }

public class TowerKitManager : MonoBehaviour
{
    [SerializeField] private Button flagObject;
    [SerializeField] private CanvasGroup flagCanvasGr;
    [SerializeField] private TowerKitBuildManager towerKitBuildManager;
    [SerializeField] private TowerKitToolsManager towerKitToolsManager;
    private void Start()
    {
        SetupBuildKit();
        Debug.Log("Start pos "+ this.transform.position);
    }
    public void SetupToolsKit(TowerBase towerBase)
    {
        flagCanvasGr.alpha = 0;
        SetupFlag(flagObject, towerKitToolsManager.kitToolsContent.gameObject);
        towerKitToolsManager.SetUpTools(towerBase);
    }
    public void SetupBuildKit()     
    {
        // flagObject.onClick.AddListener(SetFlagActive);
        // flagObject.onClick.RemoveAllListeners();
        flagCanvasGr.alpha = 1;
        SetupFlag(flagObject, towerKitBuildManager.content.gameObject);
        towerKitBuildManager.Setup(this);
    }
    public void ResetTowerKit()
    {
        // flagObject.onClick.RemoveAllListeners();
        flagCanvasGr.alpha = 1;
        SetupFlag(flagObject, towerKitBuildManager.content.gameObject);
        towerKitBuildManager.Setup(this);
        towerKitToolsManager.gameObject.SetActive(false);
    }
    public void SetupFlag(Button flag, GameObject content)
    {
        flag.gameObject.SetActive(true);
        flag.onClick.RemoveAllListeners();
        flag.onClick.AddListener(delegate
        {
            flag.gameObject.SetActive(false);
            content.gameObject.SetActive(true);
        });
    }
    // public void SetFlagActive()
    // {
    //     flagObject.gameObject.SetActive(!flagObject.gameObject.activeSelf);
    // }
}

using SuperMaxim.Messaging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// public struct TowerKitBehaviourPayload
// {
//     public TowerBehaviourTypeEnum TowerBehaviourType;
// }
//
// public enum TowerBehaviourTypeEnum
// {
//     Update,
//     Sell,
//     Build,
//     Camping,
// }
public class TowerKitToolsManager : MonoBehaviour
{
    [SerializeField] private Button btnUpdate;
    [SerializeField] private Button btnSell;
    [SerializeField] private Button btnCamping;
    [SerializeField] private Button buttonKitTools;
    [SerializeField] private GameObject kitToolsContent;
    // [SerializeField] private TowerBase towerBase;
    public void SetUpTools(TowerBase towerBase)
    {
        this.gameObject.SetActive(true);
        btnUpdate.onClick.AddListener(towerBase.TowerUpdate);
        btnSell.onClick.AddListener(towerBase.TowerSelling);
        buttonKitTools.onClick.AddListener(ToolKitTurnControl);
        // buttonKitTools.SetActive(false);
        kitToolsContent.SetActive(false);
        // btnCamping.onClick.AddListener(towerBase.TowerUpdate);
        //todo 
        // if this tower is troop tower , register btnCamping
    }
    private void ToolKitTurnControl()
    {
        Debug.Log("onlcick open tools");
        kitToolsContent.SetActive(!kitToolsContent.activeSelf);
    }
    public void RemoveTools()
    {
        btnUpdate.onClick.RemoveAllListeners();
        btnSell.onClick.RemoveAllListeners();
        btnCamping.onClick.RemoveAllListeners();
    }
}

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
    public GameObject kitToolsContent;
    // [SerializeField] private TowerBase towerBase;
    public void SetUpTools(TowerBase towerBase)
    {
        this.gameObject.SetActive(true);

        RemoveTools();
        
        btnUpdate.onClick.AddListener(towerBase.TowerUpdate);
        btnSell.onClick.AddListener(towerBase.TowerSelling);
        buttonKitTools.onClick.AddListener(ToolKitTurnControl);
        // buttonKitTools.SetActive(false);
        kitToolsContent.SetActive(false);
        // buttonKitTools.gameObject.SetActive(false);
        // btnCamping.onClick.AddListener(towerBase.TowerUpdate);
        //todo 
        // if this tower is troop tower , register btnCamping
    }
    private void ToolKitTurnControl()
    {
        kitToolsContent.SetActive(!kitToolsContent.activeSelf);
    }
    public void RemoveTools()
    {
        btnUpdate.onClick.RemoveAllListeners();
        btnSell.onClick.RemoveAllListeners();
        btnCamping.onClick.RemoveAllListeners();
        buttonKitTools.onClick.RemoveAllListeners();
    }
}

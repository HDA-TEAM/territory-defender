using GamePlay.Scripts.Tower;
using System.Collections.Generic;
using UnityEngine;

public class TowerKitSetController : MonoBehaviour
{
    [SerializeField] private List<TowerKitManager> currentTowerKits = new List<TowerKitManager>();
    public List<TowerKitManager> CurrentTowerKits
    {
        get
        {
            return currentTowerKits;
        } 
        set
        {
            currentTowerKits = value;
        }
    }
}

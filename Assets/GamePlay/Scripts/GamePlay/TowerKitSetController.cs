using GamePlay.Scripts.Tower;
using System.Collections.Generic;
using UnityEngine;

public class TowerKitSetController : MonoBehaviour
{
    [SerializeField] private List<TowerKIT> currentTowerKits = new List<TowerKIT>();
    public List<TowerKIT> CurrentTowerKits
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

using GamePlay.Scripts.Tower;
using GamePlay.Scripts.Unit;
using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public abstract class TowerBaseOld : UnitBaseOld
{
    [SerializeField] private Button button; 
    private TowerKitManager towerKitParent;
    protected override void Awake()
    {
        base.Awake();
        unitType = UnitType.Tower;
        // button.onClick.AddListener(Detail);
        // if(towerKitParent.GetComponent<TOwe>())
    }
    // public void Reset()
    // {
    //     if (button == null)
    //     {
    //         button = this.GetComponent<Button>();
    //     }
    // }
    public void OpenToolKit()
    {
        
    }
    public void TowerUpdate()
    {
        //todo 
        // Fake inventory to check enough coin to update
        
    }
    public void TowerSelling()
    {
        //todo 
        // Fake inventory to add  coin 
        // if sell success
        battleEventManager.RemoveUnit(this);
        towerKitParent.ResetTowerKit();
        Destroy(this.gameObject);
    }
    public void TowerBuild(TowerKitManager towerKitManager)
    {
        towerKitParent = towerKitManager;
        this.transform.position = towerKitParent.transform.position;
        this.transform.SetParent(towerKitParent.transform.parent);
        //todo
        // Fake inventory to check enough coin to update
    }
    public void Detail()
    {
        //todo
        // Catch event in this object then show object information
    }
    
    // public abstract void Flag();
}

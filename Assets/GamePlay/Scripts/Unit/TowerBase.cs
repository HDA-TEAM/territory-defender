using GamePlay.Scripts.Tower;
using GamePlay.Scripts.Unit;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public abstract class TowerBase : UnitBase
{
    private TowerKIT towerKitParent;
    public void TowerUpdate()
    {
        //todo 
        // Fake inventory to check enough coin to update
        
    }
    public void Sell()
    {
        //todo 
        // Fake inventory to add  coin 
        // if sell success
        
        towerKitParent.ResetTowerKitStatus();
    }
    public void Build(TowerKIT towerKit)
    {
        towerKitParent = towerKit;
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
    protected override void Awake()
    {
        base.Awake();
        unitType = UnitType.Tower;
    }
}

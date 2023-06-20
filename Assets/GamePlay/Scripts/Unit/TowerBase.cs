using GamePlay.Scripts.Unit;
using System.Collections.Generic;
using System.Numerics;

public abstract class TowerBase : UnitBase
{
    // public abstract void TowerUpdate();
    // public abstract void Sell();
    // public abstract void Build();
    // public abstract void Detail();
    
    // public abstract void Flag();
    private void Awake()
    {
        unitType = UnitType.Tower;
    }
    public void Attack()
    {
        
    }
    
    public void Move()
    {
        // Do nothing
    }
    public void Destroy()
    {
        
    }

}

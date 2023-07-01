using GamePlay.Scripts.Unit;
using System.Collections.Generic;
using System.Numerics;

public abstract class TowerBase : UnitBase
{
    public void TowerUpdate()
    {
        
    }
    public void Sell()
    {
        
    }
    public void Build()
    {
        
    }
    public void Detail()
    {
        
    }
    
    // public abstract void Flag();
    private void Awake()
    {
        unitType = UnitType.Tower;
    }
   
    
    public void Move()
    {
        // Do nothing
    }

}

using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTroopController : UnitController
{
    public override void UpdateStatus(List<UnitBase> targets)
    {
        
        if (!CheckTargetAvailable())
            return;
        
    }
 
}

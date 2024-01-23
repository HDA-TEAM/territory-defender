using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : UnitController
{
    public override void UpdateStatus(List<UnitBase> targets)
    {
        
        // if (!CheckTargetAvailable())
        //     return;
        
        float nearestUnit = float.MaxValue;
        UnitBase target = null;
        foreach (var unit in targets)
        {
            float betweenDistance = GameObjectUtility.Distance2dOfTwoGameObject(unit.gameObject, this.gameObject);
            
            if ( betweenDistance < _unitBaseParent.UnitStatsComp().GetStat(StatId.DetectRange))
            {
                if (nearestUnit > betweenDistance)
                {
                    nearestUnit = betweenDistance;
                    target = unit;
                }
            }
        }
        
        var defenderTargetChangingComposite = new UnitBase.OnTargetChangingComposite
        {
            Target = target,
            BeingTargetCommand = BeingTargetCommand.Block
        };
        _unitBaseParent.OnTargetChanging?.Invoke(defenderTargetChangingComposite);
        
        if (!target) return;
        var attackTargetChangingComposite = new UnitBase.OnTargetChangingComposite
        {
            Target = _unitBaseParent,
            BeingTargetCommand = BeingTargetCommand.None
        };
        target.OnTargetChanging?.Invoke(attackTargetChangingComposite);
    }
 
}

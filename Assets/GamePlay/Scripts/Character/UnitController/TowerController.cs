using System.Collections.Generic;

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
            
            if ( betweenDistance < _unitBaseParent.UnitStatsHandlerComp().GetCurrentStatValue(StatId.DetectRange))
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
            BeingTargetCommand = BeingTargetCommand.None
        };
        _unitBaseParent.OnTargetChanging?.Invoke(defenderTargetChangingComposite);
    }
 
}

using System.Collections.Generic;

public class AllyTroopController : UnitController
{
    public override void UpdateStatus(List<UnitBase> targets)
    {
        if (IsSelfInUserAction())
            return;
        
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
    private bool IsSelfInUserAction()
    {
        var userActionController = _unitBaseParent.UserActionController();
        if (userActionController)
            return userActionController.IsInAction() && userActionController.IsUserActionBlocked();
        return false;
    }
}

using System.Collections.Generic;

public class EnemyTroopController : UnitController
{
    public override void UpdateStatus(List<UnitBase> targets)
    {
        if (CheckTargetInUserAction(_unitBaseParent.CurrentTarget) || !IsCurrentTargetAvailable())
            SetDefaultState();
    }
    private bool CheckTargetInUserAction(UnitBase target)
    {
        if (target == null)
            return false;
        
        var userActionController = target.UserActionController();
        if (userActionController)
            return userActionController.IsInAction() && userActionController.IsUserActionBlocked();
        return false;
    }

}

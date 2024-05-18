using GamePlay.Scripts.Character.UnitController;
using System.Collections.Generic;

public class EnemyTroopController : UnitController
{
    public override void UpdateStatus(List<UnitBase> targets)
    {
        _unitBaseParent.CharacterStateMachine().UpdateStateMachine();
        
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

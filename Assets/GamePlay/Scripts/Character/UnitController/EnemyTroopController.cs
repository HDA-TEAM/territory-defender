using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTroopController : UnitController
{
    public override void UpdateStatus(List<UnitBase> targets)
    {
        
        if (!CheckSelfAvailableTargeting())
            return;
        if (CheckTargetInUserAction(_unitBaseParent.CurrentTarget))
        {
            var targetChangingComposite = new UnitBase.OnTargetChangingComposite
            {
                Target = null,
                BeingTargetCommand = BeingTargetCommand.None
            };
            _unitBaseParent.OnTargetChanging?.Invoke(targetChangingComposite);
        }
    }
    private bool CheckTargetInUserAction(UnitBase target)
    {
        var userActionController = _unitBaseParent.UserActionController();
        if (userActionController)
            return _unitBaseParent.UserActionController().IsInAction();
        return false;
    }

}

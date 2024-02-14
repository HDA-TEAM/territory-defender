using System.Collections.Generic;

public class EnemyTroopController : UnitController
{
    public override void UpdateStatus(List<UnitBase> targets)
    {
        // if (!CheckSelfAvailableTargeting())
        //     return;
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
        if (target == null)
            return false;
        
        var userActionController = target.UserActionController();
        if (userActionController)
            return target.UserActionController().IsInAction();
        return false;
    }

}

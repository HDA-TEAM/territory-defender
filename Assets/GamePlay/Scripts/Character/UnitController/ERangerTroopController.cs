using GamePlay.Scripts.Character.Stats;
using System.Collections.Generic;

public class ERangerTroopController : UnitController
{
    public override void UpdateStatus(List<UnitBase> targets)
    {
        if (CheckTargetInUserAction(_unitBaseParent.CurrentTarget) || !IsCurrentTargetAvailable())
            SetDefaultState();
            
        float nearestUnit = float.MaxValue;
        UnitBase target = null;
        foreach (var unit in targets)
        {
            float betweenDistance = GameObjectUtility.Distance2dOfTwoGameObject(unit.gameObject, this.gameObject);

            if (betweenDistance < _unitBaseParent.UnitStatsHandlerComp().GetCurrentStatValue(StatId.DetectRange))
            {
                if (nearestUnit > betweenDistance)
                {
                    nearestUnit = betweenDistance;
                    target = unit;
                }
            }
        }

        OnChangeTarget(target, BeingTargetCommand.None);
        
        _unitBaseParent.CharacterStateMachine().UpdateStateMachine();
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

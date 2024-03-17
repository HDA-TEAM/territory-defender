using System.Collections.Generic;

public class AllyTroopController : UnitController
{
    private UnitBase _prevTarget;

    public override void UpdateStatus(List<UnitBase> targets)
    {
        _unitBaseParent.CharacterStateMachine().UpdateStateMachine();
        
        if (IsSelfInUserAction())
        {
            _prevTarget = null;
            SetDefaultState();
            return;
        }
        
        float nearestUnit = float.MaxValue;
        UnitBase target = null;
        
        if (_prevTarget == _unitBaseParent.CurrentTarget && _prevTarget != null)
        {
            target = _prevTarget;
        }
        else
        {
            // Find new target
            foreach (var unit in targets)
            {
                float betweenDistance = GameObjectUtility.Distance2dOfTwoGameObject(unit.gameObject, gameObject);
            
                if (betweenDistance < _unitBaseParent.UnitStatsHandlerComp().GetCurrentStatValue(StatId.DetectRange))
                {
                    if (nearestUnit > betweenDistance)
                    {
                        nearestUnit = betweenDistance;
                        target = unit;
                    }
                }
            }
        }

        _prevTarget = target;
        
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
        UserActionController userActionController = _unitBaseParent.UserActionController();
        if (userActionController)
            return userActionController.IsInAction() && userActionController.IsUserActionBlocked();
        return false;
    }
}

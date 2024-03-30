using System;
using UnityEngine;

public class AllyIdleState : CharacterBaseState
{
    private readonly BaseAllyStateMachine _context;
    private UserActionController _userActionController;
    private Vector3 _pos;
    public AllyIdleState(BaseAllyStateMachine currentContext) : base(currentContext)
    {
        IsRootState = true;
        _context = currentContext;
    }
    public override void EnterState()
    {
        _userActionController = _context.UserActionController;
        Context.AnimationController.PlayClip(Context.AnimationController.IdleClip);
    }
    public override void UpdateState()
    {
        CheckSwitchState();
    }
    public override void ExitState()
    {
    }
    public override void CheckSwitchState()
    {
        if (_context.IsDie)
        {
            _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Die));
        }
        else if (_userActionController.IsInAction())
        {
            switch (_userActionController.CurUserAction)
            {
                case EUserAction.SetMovingPoint:
                    {
                        _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Moving));  
                        break;
                    }
            }
        }
        else if (_context.IsAttack)
        {
            _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Attacking));
        }
        else if (_context.IsMovingToTarget)
        {
            _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Approaching));
        }
    }
    public override void InitializeSubState()
    {
    }
}

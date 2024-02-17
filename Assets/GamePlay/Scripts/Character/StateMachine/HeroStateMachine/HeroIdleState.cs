using System;
using UnityEngine;

public class HeroIdleState : CharacterBaseState
{
    private readonly BaseHeroStateMachine _context;
    private UserActionController _userActionController;
    private Vector3 _pos;
    private static readonly int IsIdle = Animator.StringToHash("IsIdle");
    public HeroIdleState(BaseHeroStateMachine currentContext) : base(currentContext)
    {
        IsRootState = true;
        _context = currentContext;
    }
    public override void EnterState()
    {
        _userActionController = _context.UserActionController;
        Context.CharacterAnimator.SetBool(IsIdle, true);
    }
    public override void UpdateState()
    {
        CheckSwitchState();
    }
    public override void ExitState()
    {
        Context.CharacterAnimator.SetBool(IsIdle, false);
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
                case EUserAction.UsingSkill:
                    {
                        _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.UsingSkill));  
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

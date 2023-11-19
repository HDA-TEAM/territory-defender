using System;

public class HeroIdleState : CharacterBaseState
{
    private readonly BaseHeroStateMachine _context;
    public HeroIdleState(BaseHeroStateMachine currentContext) : base(currentContext)
    {
        IsRootState = true;
        _context = currentContext;
    }
    public override void EnterState()
    {
        Context.CharacterAnimator.SetBool("IsIdle", true);
    }
    public override void UpdateState()
    {
        CheckSwitchState();
    }
    public override void ExitState()
    {
        Context.CharacterAnimator.SetBool("IsIdle", false);
    }
    public override void CheckSwitchState()
    {
        if (_context.IsAttack)
        {
            _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Attacking));
        }
        if (_context.IsMoving)
        {
            _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Moving));
        }
    }
    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }
}

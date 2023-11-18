using System;

public class HeroIdleState : CharacterBaseState
{
    private BaseHeroStateMachine _context;
    public HeroIdleState(BaseHeroStateMachine currentContext, CharacterStateFactory characterStateFactory) : base(currentContext, characterStateFactory)
    {
        _context = currentContext;
    }
    public override void EnterState()
    {
        
    }
    public override void UpdateState()
    {
        CheckSwitchState();
    }
    public override void ExitState()
    {
        throw new System.NotImplementedException();
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

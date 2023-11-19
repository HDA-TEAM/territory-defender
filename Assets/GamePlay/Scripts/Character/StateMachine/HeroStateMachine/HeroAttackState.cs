using System;
using UnityEngine;

public class HeroAttackState : CharacterAttackState
{
    private readonly BaseHeroStateMachine _context;
    public HeroAttackState(BaseHeroStateMachine currentContext) : base(currentContext)
    {
        _context = currentContext;
    }
    public override void CheckSwitchState()
    {
        base.EnterState();
        if (!_context.IsAttack)
        {
            _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Idle));
        }
    }
}

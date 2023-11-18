using System;
using UnityEngine;

public class HeroAttackState : CharacterAttackState
{
    private readonly BaseHeroStateMachine _context;
    public HeroAttackState(BaseHeroStateMachine currentContext, CharacterStateFactory characterStateFactory) : base(currentContext, characterStateFactory)
    {
        _context = currentContext;
    }
    public override void CheckSwitchState()
    {
        base.EnterState();
        if (_context.IsMoving)
        {
            _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Moving));
        }
    }
}

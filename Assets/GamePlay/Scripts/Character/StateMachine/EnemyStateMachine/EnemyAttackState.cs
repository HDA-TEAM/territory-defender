using System;
using UnityEngine;

public class EnemyAttackState : CharacterAttackState
{
    private readonly BaseEnemyStateMachine _context;
    public EnemyAttackState(BaseEnemyStateMachine currentContext) : base(currentContext)
    {
        _context = currentContext;
    }
    public override void CheckSwitchState()
    {
        if (_context.IsDie)
        {
            _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Die));
        }
        if (!_context.IsStopToAttack)
        {
            _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Idle));
        }
    }
}

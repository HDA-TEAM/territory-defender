using System;
using UnityEngine;

public class TowerAttackState : CharacterAttackState
{
    private readonly BaseTowerStateMachine _context;
    private Vector3 _pos;
    public TowerAttackState(BaseTowerStateMachine currentContext) : base(currentContext)
    {
        _context = currentContext;
    }
    public override void CheckSwitchState()
    {
        if (!_context.IsAttack)
        {
            _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Idle));
        }
    }
}

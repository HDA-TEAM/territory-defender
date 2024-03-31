using DG.Tweening;
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
        bool isSwitch = false;
        if (!_context.IsAttack)
        {
            _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Idle));
            isSwitch = true;
        }
        if(isSwitch)
            _attackSequence.Kill();
    }
}

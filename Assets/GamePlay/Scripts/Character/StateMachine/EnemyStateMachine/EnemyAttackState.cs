using UnityEngine;

public class EnemyAttackState : CharacterAttackState
{
    private readonly BaseEnemyStateMachine _context;
    public EnemyAttackState(BaseEnemyStateMachine currentContext) : base(currentContext)
    {
        _context = currentContext;
    }
    public override void UpdateState()
    {
        var isInAttackRange = _context.CurrentTarget
                              && GameObjectUtility.Distance2dOfTwoGameObject(_context.gameObject, _context.CurrentTarget.gameObject) < _context.CharacterStats.GetCurrentStatValue(StatId.AttackRange);
        if (isInAttackRange)
        {
            _cooldownNextAttack -= Time.deltaTime;
            _attackDame = Context.CharacterStats.GetCurrentStatValue(StatId.AttackDamage);

            CheckSwitchState();

            HandleAttack();
        }
        CheckSwitchState();
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

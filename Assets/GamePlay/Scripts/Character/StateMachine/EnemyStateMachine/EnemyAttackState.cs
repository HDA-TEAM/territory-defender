using DG.Tweening;
using GamePlay.Scripts.Character.StateMachine.EnemyStateMachine;

public class EnemyAttackState : CharacterAttackState
{
    private readonly BaseEnemyStateMachine _context;
    private bool _isInAttackRange;
    public EnemyAttackState(BaseEnemyStateMachine currentContext) : base(currentContext)
    {
        _context = currentContext;
    }
    public override void CheckSwitchState()
    {
        _context.CheckAttackingOrWaiting();
        
        bool isSwitch = false;
        
        if (_context.IsDie)
        {
            _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Die));
            isSwitch = true;
        }
        else if (!_context.IsStopToAttackingOrWaiting())
        {
            _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Idle));
            isSwitch = true;
        }
        else if (_context.IsStopToWaiting)
        {
            _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Waiting));
            isSwitch = true;
        }
        
        if (isSwitch)
            _attackSequence.Kill();
    }
}

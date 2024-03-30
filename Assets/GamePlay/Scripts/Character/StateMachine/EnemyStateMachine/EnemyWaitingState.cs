using GamePlay.Scripts.Character.StateMachine.EnemyStateMachine;

public class EnemyWaitingState : CharacterBaseState
{
    private readonly BaseEnemyStateMachine _context;
    private bool _isInAttackRange;
    public EnemyWaitingState(BaseEnemyStateMachine currentContext) : base(currentContext)
    {
        IsRootState = true;
        _context = currentContext;
    }
    public override void EnterState()
    {
        _isInAttackRange = false;
        Context.AnimationController.PlayClip(Context.AnimationController.IdleClip);
    }
    public override void UpdateState()
    {
        CheckSwitchState();
    }
    
    public override void ExitState()
    {
        _isInAttackRange = false;
    }
    public override void CheckSwitchState()
    {
        _context.CheckAttackingOrWaiting();
        if (_context.IsDie)
        {
            _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Die));
        }
        else if (!_context.IsStopToAttackingOrWaiting())
        {
            _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Idle));
        }
        else if (_context.IsStopToAttack)
        {
            _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Attacking));
        }
    }
    public override void InitializeSubState()
    {
    }
}

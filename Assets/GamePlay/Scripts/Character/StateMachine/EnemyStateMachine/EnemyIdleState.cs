public class EnemyIdleState : CharacterBaseState
{
    private readonly BaseEnemyStateMachine _context;
    public EnemyIdleState(BaseEnemyStateMachine currentContext) : base(currentContext)
    {
        IsRootState = true;
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
    }
    public override void CheckSwitchState()
    {
        if (_context.IsDie)
        {
            _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Die));
        }
        else if (_context.IsStopToAttackingOrWaiting())
        {
            if (_context.IsStopToWaiting)
            {
                _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Waiting));
            }
            else
            {
                _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Attacking));
            }
        }
        else if (_context.IsMovingToGate)
        {
            _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Moving));
        }
    }
    public override void InitializeSubState()
    {
    }
}

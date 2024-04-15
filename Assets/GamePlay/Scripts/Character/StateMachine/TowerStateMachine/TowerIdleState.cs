public class TowerIdleState : CharacterBaseState
{
    private readonly BaseTowerStateMachine _context;
    public TowerIdleState(BaseTowerStateMachine currentContext) : base(currentContext)
    {
        _context = currentContext;
        IsRootState = true;
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
        if (_context.IsAttack)
        {
            _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Attacking));
        }
    }
    public override void InitializeSubState()
    {
    }
}

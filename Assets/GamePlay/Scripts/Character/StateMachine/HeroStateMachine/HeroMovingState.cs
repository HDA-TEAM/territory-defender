public class HeroMovingState : CharacterMovingState
{
    private readonly BaseHeroStateMachine _context;
    public HeroMovingState(BaseHeroStateMachine currentContext) : base(currentContext)
    {
        IsRootState = true;
        _context = currentContext;
    }
    public override void EnterState()
    {
        base.EnterState();
        _userActionController = _context.UserActionController as UserActionHeroBaseController;
    }
    public override void CheckSwitchState()
    {
        if (!_userActionController.IsInAction())
        {
            _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Idle));
        }
    }
}

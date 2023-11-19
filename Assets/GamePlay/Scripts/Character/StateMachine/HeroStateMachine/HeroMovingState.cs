public class HeroMovingState : CharacterBaseState
{
    private readonly BaseHeroStateMachine _context;
    private float _movingSpeed;
    public HeroMovingState(BaseHeroStateMachine currentContext) : base(currentContext)
    {
        IsRootState = true; 
        _context = currentContext;
    }
    public override void EnterState()
    {
        _movingSpeed = _context.CharacterStats.GetStat(StatId.MovementSpeed);
        _context.CharacterAnimator.SetBool("IsMoving",true);
    }
    public override void UpdateState()
    {
        PlayMoving();
        CheckSwitchState();
    }
    public override void ExitState()
    {
        _context.CharacterAnimator.SetBool("IsMoving",false);
    }
    public override void CheckSwitchState()
    {
        if (!_context.IsMoving)
        {
            _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Idle));
        }
    }
    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }
    #region Moving Logic
    private void PlayMoving()
    {
        _context.transform.position =VectorUtility.Vector2MovingAToB(
            _context.transform.position,
            _context.Target.transform.position,
            _movingSpeed);
    }
    #endregion
}

using GamePlay.Scripts.Character.StateMachine;

public class CharacterMovingState : CharacterBaseState
{
    private readonly CharacterStateMachine _context;
    private float _movingSpeed;
    protected UserActionController _userActionController;
    protected CharacterMovingState(CharacterStateMachine currentContext) : base(currentContext)
    {
        IsRootState = true;
        _context = currentContext;
    }
    public override void EnterState()
    {
        _movingSpeed = _context.CharacterStats.GetCurrentStatValue(StatId.MovementSpeed);
        Context.AnimationController.PlayClip(Context.AnimationController.MovingClip);
    }
    public override void UpdateState()
    {
        PlayMoving();
        CheckSwitchState();
    }
    public override void ExitState()
    {
    }
    public override void CheckSwitchState() { }
    public override void InitializeSubState()
    {
    }
    #region Moving Logic
    private void PlayMoving()
    {
        _context.transform.position = VectorUtility.Vector3MovingAToB(
            _context.transform.position,
            _userActionController.UserMoveUnitToCampingPlace.DesPos,
            _movingSpeed);
        CheckingReachedDestination();
    }
    private void CheckingReachedDestination()
    {
        if (VectorUtility.IsTwoPointReached(_context.transform.position, _userActionController.UserMoveUnitToCampingPlace.DesPos))
        {
            _userActionController.SetFinishedUserAction();
        }
    }
    #endregion
}

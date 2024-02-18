using UnityEngine;

public class CharacterMovingState : CharacterBaseState
{
    private readonly CharacterStateMachine _context;
    private float _movingSpeed;
    protected UserActionController _userActionController;
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    public CharacterMovingState(CharacterStateMachine currentContext) : base(currentContext)
    {
        IsRootState = true;
        _context = currentContext;
    }
    public override void EnterState()
    {
        _movingSpeed = _context.CharacterStats.GetStat(StatId.MovementSpeed);
        _context.CharacterAnimator.SetBool(IsMoving, true);
    }
    public override void UpdateState()
    {
        PlayMoving();
        CheckSwitchState();
    }
    public override void ExitState()
    {
        _context.CharacterAnimator.SetBool(IsMoving, false);
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

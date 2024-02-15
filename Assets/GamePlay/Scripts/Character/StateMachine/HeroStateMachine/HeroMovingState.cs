using UnityEngine;

public class HeroMovingState : CharacterBaseState
{
    private readonly BaseHeroStateMachine _context;
    private float _movingSpeed;
    private UserActionController _userActionController;
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    public HeroMovingState(BaseHeroStateMachine currentContext) : base(currentContext)
    {
        IsRootState = true;
        _context = currentContext;
    }
    public override void EnterState()
    {
        _userActionController = _context.UserActionController;
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
    public override void CheckSwitchState()
    {
        if (!_context.UserActionController.IsInAction())
        {
            _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Idle));
        }
    }
    public override void InitializeSubState()
    {
    }
    #region Moving Logic
    private void PlayMoving()
    {
        _context.transform.position = VectorUtility.Vector3MovingAToB(
            _context.transform.position,
            _userActionController.UserMovingHero.DesPos,
            _movingSpeed);
        CheckingReachedDestination();
    }
    private void CheckingReachedDestination()
    {
        if (VectorUtility.IsTwoPointReached(_context.transform.position,_userActionController.UserMovingHero.DesPos))
        {
            _userActionController.SetFinishedUserAction();
        }
    }
    #endregion
}

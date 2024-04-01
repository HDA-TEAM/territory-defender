using UnityEngine;

public class HeroApproachingState : CharacterBaseState
{
    private readonly BaseHeroStateMachine _context;
    private float _movingSpeed;
    public HeroApproachingState(BaseHeroStateMachine currentContext) : base(currentContext)
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
        CheckSwitchState();
        PlayMoving();
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
        else if (_context.UserActionController.IsInAction())
        {
            _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Idle));
        }
        else if (!_context.IsMovingToTarget)
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
            _context.Target.transform.position,
            _movingSpeed);
    }
    #endregion
}

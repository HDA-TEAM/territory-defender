using GamePlay.Scripts.Character.Stats;
using UnityEngine;

public class AllyApproachingState : CharacterBaseState
{
    private readonly BaseAllyStateMachine _context;
    private float _movingSpeed;
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    public AllyApproachingState(BaseAllyStateMachine currentContext) : base(currentContext)
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
    public override void CheckSwitchState()
    {
        if (_context.IsDie)
        {
            _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Die));
        }
        if (_context.UserActionController.IsInAction())
        {
            _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Idle));
        }
        if (!_context.IsMovingToTarget)
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

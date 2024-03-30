using UnityEngine;

public class EnemyMovingState : CharacterBaseState
{
    private readonly BaseEnemyStateMachine _context;
    private float _movingSpeed;
    public EnemyMovingState(BaseEnemyStateMachine currentContext) : base(currentContext)
    {
        IsRootState = true; 
        _context = currentContext;
    }
    public override void EnterState()
    {
        Context.AnimationController.PlayClip(Context.AnimationController.MovingClip);
        _movingSpeed = _context.CharacterStats.GetCurrentStatValue(StatId.MovementSpeed);
    }
    public override void UpdateState()
    {
        MovingToDestination();
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
        if (!_context.IsMovingToGate)
        {
            _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Idle));
        }
    }
    public override void InitializeSubState() {}
    #region Moving Logic
    private void MovingToDestination()
    {
        if (IsReachedDestinationGate() && _context.RouteToGate != null)
        {
            // remove route to stop moving
            _context.RouteToGate = null;
            OnReachingDestination();
            return;
        }
        if (VectorUtility.IsTwoPointReached(
            _context.transform.position, 
            _context.RouteToGate.GetPosition(_context.CurrentIndexInRouteLine)))
        {
            _context.CurrentIndexInRouteLine += 1;
        }
        PlayMoving();
    }
    private bool IsReachedDestinationGate()
    {
        return (_context.CurrentIndexInRouteLine == _context.RouteToGate.positionCount - 1);
    }
    private void PlayMoving()
    {
        _context.transform.position = VectorUtility.Vector3MovingAToB(
            _context.transform.position,
            _context.RouteToGate.GetPosition(_context.CurrentIndexInRouteLine),
            _movingSpeed);
    }
    public void OnReachingDestination()
    {
        // return pooling and status
        _context.InGameInventoryData.TryChangeLife(
            - (int)_context.CharacterStats.GetCurrentStatValue(StatId.LifeReduce));
        _context.UnitBaseParent().HealthComp().ResetState();
    }
    #endregion
}

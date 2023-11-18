public class HeroMovingState : CharacterBaseState
{
    private BaseEnemyStateMachine _context;
    private float _movingSpeed;
    public HeroMovingState(BaseEnemyStateMachine currentContext, EnemyStateFactory characterStateFactory) : base(currentContext, characterStateFactory)
    {
        IsRootState = true; 
        _context = currentContext;
    }
    public override void EnterState()
    {
        _movingSpeed = _context.CharacterStats.GetStat(StatId.MovementSpeed);
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
        if (!_context.IsMovingToGate)
        {
        }
    }
    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }
    #region Moving Logic
    private void MovingToDestination()
    {
        if (IsReachedDestinationGate())
        {
            // remove route to stop moving
            // _context.RouteToGate = null;
            // _unitBaseParent.EnemyReachingDestinationComp.OnReachingDestination();
            _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Die));
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
        _context.transform.position =VectorUtility.Vector2MovingAToB(
            _context.transform.position,
            _context.RouteToGate.GetPosition(_context.CurrentIndexInRouteLine),
            _movingSpeed);
    }
    #endregion
}

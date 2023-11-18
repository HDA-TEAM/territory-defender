public class EnemyIdleState : CharacterBaseState
{
    private BaseEnemyStateMachine _context;
    public EnemyIdleState(BaseEnemyStateMachine currentContext, CharacterStateFactory characterStateFactory) : base(currentContext, characterStateFactory)
    {
        _context = currentContext;
    }
    public override void EnterState()
    {
        throw new System.NotImplementedException();
    }
    public override void UpdateState()
    {
        throw new System.NotImplementedException();
    }
    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }
    public override void CheckSwitchState()
    {
        throw new System.NotImplementedException();
    }
    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }
}

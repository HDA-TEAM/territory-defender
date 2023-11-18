public class CharacterIdleState : CharacterBaseState
{
    public CharacterIdleState(CharacterStateMachine currentContext, CharacterStateFactory characterStateFactory) 
        : base(currentContext,characterStateFactory)
    {
        IsRootState = true;
    }
    public override void EnterState()
    {
        InitializeSubState();
    }
    public override void UpdateState()
    {
        CheckSwitchState();
    }
    public override void ExitState()
    {
    }
    public override void CheckSwitchState()
    {
    }
    public override void InitializeSubState()
    {
    }
}

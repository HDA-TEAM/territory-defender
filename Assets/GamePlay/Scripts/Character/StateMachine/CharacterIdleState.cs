using GamePlay.Scripts.Character.StateMachine;

public class CharacterIdleState : CharacterBaseState
{
    public CharacterIdleState(CharacterStateMachine currentContext) : base(currentContext)
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

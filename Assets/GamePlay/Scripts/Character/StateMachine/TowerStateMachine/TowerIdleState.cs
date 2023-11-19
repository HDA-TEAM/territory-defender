using System;

public class TowerIdleState : CharacterBaseState
{

    public TowerIdleState(BaseTowerStateMachine currentContext) : base(currentContext)
    {
        IsRootState = true;
    }
    public override void EnterState()
    {
        throw new System.NotImplementedException();
    }
    public override void UpdateState()
    {
        CheckSwitchState();
    }
    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }
    public override void CheckSwitchState()
    {
    }
    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }
}

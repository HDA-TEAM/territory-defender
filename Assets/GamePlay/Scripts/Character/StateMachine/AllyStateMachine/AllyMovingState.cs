using UnityEngine;

public class AllyMovingState : CharacterMovingState
{
    private readonly BaseAllyStateMachine _context;
    public AllyMovingState(BaseAllyStateMachine currentContext) : base(currentContext)
    {
        IsRootState = true;
        _context = currentContext;
    }
    public override void EnterState()
    {
        base.EnterState();
        _userActionController = _context.UserActionController;
    }
    public override void CheckSwitchState()
    {
        if (!_userActionController.IsInAction())
        {
            _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Idle));
        }
    }
}

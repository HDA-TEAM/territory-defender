using DG.Tweening;

public class HeroAttackState : CharacterAttackState
{
    private readonly BaseHeroStateMachine _context;
    public HeroAttackState(BaseHeroStateMachine currentContext) : base(currentContext)
    {
        _context = currentContext;
    }
    public override void CheckSwitchState()
    {
        bool isSwitch = false;
        if (_context.IsDie)
        {
            _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Die));
            isSwitch = true;
        }
        if (_context.UserActionController.IsInAction())
        {
            _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Idle));
            isSwitch = true;
        }
        else if (!_context.IsAttack)
        {
            _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Idle));
            isSwitch = true;
        }
        if (isSwitch)
            _attackSequence.Kill();
    }
    
}

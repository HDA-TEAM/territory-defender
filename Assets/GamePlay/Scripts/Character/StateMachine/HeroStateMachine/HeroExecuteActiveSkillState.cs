using UnityEngine;

public class HeroExecuteActiveSkillState : CharacterBaseState
{
    private readonly BaseHeroStateMachine _context;
    private static readonly int IsPlayActiveSkill = Animator.StringToHash("IsPlayActiveSkill");
    public HeroExecuteActiveSkillState(BaseHeroStateMachine currentContext) : base(currentContext)
    {
        IsRootState = true;
        _context = currentContext;
    }
    public override void EnterState()
    {
        _context.CharacterAnimator.SetBool(IsPlayActiveSkill, true);
    }
    public override void UpdateState()
    {
        CheckSwitchState();
    }
    public override void ExitState()
    {
        _context.CharacterAnimator.SetBool(IsPlayActiveSkill, false);
    }
    public override void CheckSwitchState()
    {
        if (_context.IsDie)
        {
            _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Die));
        }
        if (!_context.UserActionController.IsInAction())
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
}

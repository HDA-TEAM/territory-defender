using UnityEngine;

public class HeroExecuteActiveSkillState : CharacterBaseState
{
    private readonly BaseHeroStateMachine _context;
    private static readonly int IsPlayActiveSkill = Animator.StringToHash("IsPlayActiveSkill");
    private float _skillDuringTime;
    private bool _isFinished;
    public HeroExecuteActiveSkillState(BaseHeroStateMachine currentContext) : base(currentContext)
    {
        IsRootState = true;
        _context = currentContext;
    }
    public override void EnterState()
    {
        _isFinished = false;
        _skillDuringTime = _context.CharacterAnimator.GetCurrentAnimatorClipInfo(0).Length;
        _context.CharacterAnimator.SetBool(IsPlayActiveSkill, true);
    }
    public override void UpdateState()
    {
        CheckSwitchState();
        CheckEndedSkill();
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
        if (_isFinished)
        {
            _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Idle));
        }
        // if (!_context.UserActionController.IsInAction())
        // {
        //     _context.CurrentState.SwitchState(_context.StateFactory.GetState(CharacterState.Idle));
        // }
    }
    private void CheckEndedSkill()
    {
        _skillDuringTime -= Time.deltaTime;
        if (_skillDuringTime <= 0)
        {
            _context.UserActionController.SetFinishedUserAction();
            _isFinished = true;
        }
    }
    public override void InitializeSubState()
    {
    }
}

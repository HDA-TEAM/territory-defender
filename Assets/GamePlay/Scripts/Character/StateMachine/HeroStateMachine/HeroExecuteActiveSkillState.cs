using System.Linq;
using UnityEngine;

public class HeroExecuteActiveSkillState : CharacterBaseState
{
    private readonly BaseHeroStateMachine _context;
    private static readonly int IsPlayActiveSkill = Animator.StringToHash("IsPlayActiveSkill");
    private UserActionController _userActionController;
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
        _userActionController = _context.UserActionController;
        _skillDuringTime = _context.CharacterAnimator.runtimeAnimatorController.animationClips[0].length;
        Debug.Log("_skillDuringTime " + _skillDuringTime);
        Debug.Log("_skillDuringTime " +  _context.CharacterAnimator.GetCurrentAnimatorClipInfo(0).Length);
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
            DealingDame();
            _context.UserActionController.SetFinishedUserAction();
            _isFinished = true;
        }
    }
    private void DealingDame()
    {
        SkillDataSO skillConfig = _userActionController.UserUsingHeroSkill.SkillConfig;

        var targetList = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        foreach (var target in targetList)
        {
            if (GameObjectUtility.Distance2dOfTwoGameObject(_context.gameObject, target) <= skillConfig.GetStat(StatId.AttackRange))
            {
                var healComp = target.GetComponent<UnitBase>().HealthComp();
                if(healComp) healComp.PlayHurting(skillConfig.GetStat(StatId.AttackDamage));
            }
        }
    }
    public override void InitializeSubState()
    {
    }
}

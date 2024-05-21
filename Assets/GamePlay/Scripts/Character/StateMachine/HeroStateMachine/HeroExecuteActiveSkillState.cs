using Common.Scripts.Data;
using Common.Scripts.Utilities;
using GamePlay.Scripts.Character.Stats;
using System.Linq;
using UnityEngine;

public class HeroExecuteActiveSkillState : CharacterBaseState
{
    private readonly BaseHeroStateMachine _context;
    private UserActionHeroBaseController _userActionController;
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
        _userActionController = _context.UserActionController as UserActionHeroBaseController;
        AnimationClip activeSkill = Context.AnimationController.FirstActiveSkillClip;
        _skillDuringTime = activeSkill.length;
        _context.AnimationController.PlayClip(activeSkill);
    }
    public override void UpdateState()
    {
        CheckSwitchState();
        CheckEndedSkill();
    }
    public override void ExitState()
    {
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
                if (healComp) healComp.PlayHurting(skillConfig.GetStat(StatId.AttackDamage));
            }
        }
    }
    public override void InitializeSubState()
    {
    }
}

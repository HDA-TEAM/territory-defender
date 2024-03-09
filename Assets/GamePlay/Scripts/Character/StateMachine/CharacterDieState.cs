using UnityEngine;

public class CharacterDieState : CharacterBaseState
{
    protected float _durationDie;
    protected CharacterDieState(CharacterStateMachine currentContext) : base(currentContext)
    {
        IsRootState = true; 
    }
    public override void EnterState()
    {
        Animator animator = Context.CharacterAnimator;
        animator.SetBool("IsDie",true);
        _durationDie = animator.runtimeAnimatorController.animationClips[0].length;
    }
    public override void UpdateState()
    {
        _durationDie -= Time.deltaTime;
        CheckSwitchState();
    }
    public override void ExitState()
    {
        Context.CharacterAnimator.SetBool("IsDie",false);
        Context.gameObject.SetActive(false);
    }
    public override void CheckSwitchState()
    {
        if (_durationDie <= 0)
        {
            _durationDie = 0;
            ExitState();
        }
    }
    public override void InitializeSubState()
    {
    }
}

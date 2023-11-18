using UnityEngine;

public class CharacterAttackState : CharacterBaseState
{
    protected float _cooldownNextAttack;
    public CharacterAttackState(CharacterStateMachine currentContext, CharacterStateFactory characterStateFactory) 
        : base(currentContext,characterStateFactory)
    {
        IsRootState = true;
    }
    public override void EnterState()
    {
    }
    public override void UpdateState()
    {
        _cooldownNextAttack -= Time.deltaTime;
        HandleAttack();
        CheckSwitchState();
    }
    public override void ExitState()
    {
        Context.CharacterAnimator.SetBool("IsAttack",false);
    }
    public override void CheckSwitchState() {}
    public override void InitializeSubState() {}
    private void HandleAttack()
    {
        if (_cooldownNextAttack <= 0)
        {
            Context.CharacterAnimator.SetBool("IsAttack",true);
            // Reset cooldown next attack time
            _cooldownNextAttack = Context.CharacterStats.GetStat(StatId.AttackSpeed);
        }
        
    }
}

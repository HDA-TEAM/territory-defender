using UnityEngine;

public class CharacterAttackState : CharacterBaseState
{
    protected float _cooldownNextAttack;
    protected float _attackDame;
    public CharacterAttackState(CharacterStateMachine currentContext) : base(currentContext)
    {
        IsRootState = true;
    }
    public override void EnterState()
    {
    }
    public override void UpdateState()
    {
        _cooldownNextAttack -= Time.deltaTime;
        _attackDame = Context.CharacterStats.GetStat(StatId.AttackDamage);
        
        CheckSwitchState();
        
        HandleAttack();
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
            
            switch (Context.CharacterTroopBehaviourType)
            {
                case TroopBehaviourType.Tower:
                    {
                        // Tower don't need to check distance, it always fire any target exist
                        
                        // Debug.Log("Target distance: " + GameObjectUtility.Distance2dOfTwoGameObject(gameObject, target.gameObject));
                        // new CharacterAttackingFactory().GetAttackingStrategy(attackingType).PlayAttacking(target,attackingDamage);
                       
                        var prjBase = Context.CharacterProjectileDataAsset.GetProjectileBase(Context.CharacterProjectileIUnitId);
                        prjBase.GetProjectileMovement().GetLineRoute(Context.transform.position, EProjectileType.Arrow, Context.CurrentTarget);
                        return;
                    }
                case TroopBehaviourType.Melee:
                case TroopBehaviourType.Ranger:
                    {
                        // Need to check is in available attack range
                        // if (GameObjectUtility.Distance2dOfTwoGameObject(this.gameObject, target.gameObject) < attackingRange)
                        //     return;
                        
                        // isNeedToWaitCoolDownAttacking = true;

                        new CharacterAttackingFactory().GetAttackingStrategy(TroopBehaviourType.Melee).PlayAttacking(Context.CurrentTarget, _attackDame);
                        // await UniTask.Delay(TimeSpan.FromSeconds(attackingCooldown));
                        //
                        // isNeedToWaitCoolDownAttacking = false;
                        return;
                    }
                default:
                    {
                        // isNeedToWaitCoolDownAttacking = true;
                        return;
                    }
            }
        }

    }
}

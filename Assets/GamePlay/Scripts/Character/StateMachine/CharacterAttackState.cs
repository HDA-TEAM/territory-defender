using UnityEngine;

public class CharacterAttackState : CharacterBaseState
{
    protected float _cooldownNextAttack;
    protected float _attackDame;
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");
    protected CharacterAttackState(CharacterStateMachine currentContext) : base(currentContext)
    {
        IsRootState = true;
    }
    public override void EnterState()
    {
        Context.CharacterAnimator.SetBool(IsAttack,true);
    }
    public override void UpdateState()
    {
        _cooldownNextAttack -= Time.deltaTime;
        _attackDame = Context.CharacterStats.GetCurrentStatValue(StatId.AttackDamage);
        
        CheckSwitchState();
        
        HandleAttack();
    }
    public override void ExitState()
    {
        Context.CharacterAnimator.SetBool(IsAttack,false);
    }
    public override void CheckSwitchState() {}
    public override void InitializeSubState() {}
    protected void HandleAttack()
    {
        if (_cooldownNextAttack <= 0)
        {
            // Reset cooldown next attack time
            _cooldownNextAttack = Context.CharacterStats.GetCurrentStatValue(StatId.AttackSpeed);
            
            switch (Context.CharacterTroopBehaviourType)
            {
                case TroopBehaviourType.Ranger:
                case TroopBehaviourType.Tower:
                    {
                        // Tower don't need to check distance, it always fire any target exist
                        
                        // Debug.Log("Target distance: " + GameObjectUtility.Distance2dOfTwoGameObject(gameObject, target.gameObject));
                        // new CharacterAttackingFactory().GetAttackingStrategy(attackingType).PlayAttacking(target,attackingDamage);

                        if (Context.CharacterProjectileIUnitId == UnitId.Projectile.None)
                        return;
                        
                        var prjBase = Context.CharacterProjectileDataAsset.GetProjectileBase(Context.CharacterProjectileIUnitId.ToString());
                        prjBase.GetProjectileMovement().GetLineRoute(Context.transform.position, EProjectileType.Arrow, Context.CurrentTarget);
                        return;
                    }
                case TroopBehaviourType.Melee:
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

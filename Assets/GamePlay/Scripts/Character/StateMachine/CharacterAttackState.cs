using DG.Tweening;
using UnityEngine;

public class CharacterAttackState : CharacterBaseState
{
    private float _cooldownNextAttack;
    private float _attackDame;
    private float _onceNormalAttackDuringTime;
    protected Sequence _attackSequence;
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");
    private static readonly int IsIdle = Animator.StringToHash("IsIdle");
    protected CharacterAttackState(CharacterStateMachine currentContext) : base(currentContext)
    {
        IsRootState = true;
    }
    
    public override void EnterState()
    {
        // Song tu 50%
        // thien binh 30%
        // bao binh  50%
        // nhan ma 30%
        // cu giai 70%
        _attackSequence = DOTween.Sequence();

        _attackDame = Context.CharacterStats.GetCurrentStatValue(StatId.AttackDamage);
        
        // Reset cooldown next attack time
        SetAttackAnim(true);
        _cooldownNextAttack = Context.CharacterStats.GetCurrentStatValue(StatId.AttackSpeed);
        _onceNormalAttackDuringTime = Context.CharacterAnimator.runtimeAnimatorController.animationClips[0].length;
        
        Debug.Log(Context.CharacterAnimator.runtimeAnimatorController.animationClips[0].name);
        Debug.Log(_onceNormalAttackDuringTime);
        HandleAttackProcessing();
    }
    public override void UpdateState()
    {
        CheckSwitchState();
    }
    public override void ExitState()
    {
        Context.CharacterAnimator.SetBool(IsAttack, false);
    }
    public override void CheckSwitchState() { }
    public override void InitializeSubState() { }
    private void SetAttackAnim(bool isInAttack)
    {
        Context.CharacterAnimator.SetBool(IsAttack, isInAttack);
        Context.CharacterAnimator.SetBool(IsIdle, !isInAttack);
    }
    private void HandleAttackProcessing()
    {
        
        Tween attackOnceTime = DOVirtual.DelayedCall(_onceNormalAttackDuringTime, () =>
        {
            SetAttackAnim(false);
            PlayingAttackOnceTime();
        }, ignoreTimeScale: false);

        Tween waitingNextAttack = DOVirtual.DelayedCall(_cooldownNextAttack, () =>  SetAttackAnim(true), ignoreTimeScale: false);

        _attackSequence.Append(attackOnceTime);
        _attackSequence.Append(waitingNextAttack);

        _attackSequence.SetLoops(-1);
        _attackSequence.Play();
    }
    private void PlayingAttackOnceTime()
    {
        switch (Context.CharacterTroopBehaviourType)
        {
            case TroopBehaviourType.Ranger:
            case TroopBehaviourType.Tower:
                {
                    // Tower don't need to check distance, it always fire any target exist
                    if (Context.CharacterProjectileIUnitId == UnitId.Projectile.None)
                        return;

                    var prjBase = Context.CharacterProjectileDataAsset.GetProjectileBase(Context.CharacterProjectileIUnitId.ToString());
                    prjBase.GetProjectileMovement().GetLineRoute(Context.StartAttackPoint.position, EProjectileType.Arrow, Context.CurrentTarget);
                    return;
                }
            case TroopBehaviourType.Melee:
                {
                    new CharacterAttackingFactory().GetAttackingStrategy(TroopBehaviourType.Melee).PlayAttacking(Context.CurrentTarget, _attackDame);

                    return;
                }
            default:
                {
                    return;
                }
        }
    }
}

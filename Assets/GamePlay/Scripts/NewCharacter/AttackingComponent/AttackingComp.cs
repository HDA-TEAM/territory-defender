using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.Serialization;

public enum AttackingType
{
    Tower = 0,
    Melee = 1,
    Ranger = 2,
    MeleeAndRanger = 3,
}

public class AttackingComp : UnitBaseComponent
{
    [Header("Attribute")]
    [SerializeField] private Transform startAttackPoint;
    [SerializeField] private AttackingType attackingType;
    
    [SerializeField] private float attackingCooldown;
    [SerializeField] private float attackingDamage;
    [SerializeField] private float attackingRange;

    [FormerlySerializedAs("bulletDataAsset")]
    [Header("Data"), Space(12)]
    [SerializeField] private ProjectileDataAsset _projectileDataAsset;
    [SerializeField] private UnitId _projectileId;

    private bool canAttacking = true;

    #region Event
    private void OnEnable()
    {
        _unitBaseParent.OnCharacterChange += AttackingTarget;
    }
    private void OnDisable()
    {
        _unitBaseParent.OnCharacterChange -= AttackingTarget;
    }
    #endregion

    #region Stats
    protected override void StatsUpdate()
    {
        var stats = _unitBaseParent.UnitStatsComp();
        attackingDamage = stats.GetStat(StatId.AttackDamage);
        attackingRange = stats.GetStat(StatId.AttackRange);
    }
    #endregion
    #region Logic
    private async void AttackingTarget(UnitBase target)
    {
        if (target == null)
            return;

        if (canAttacking)
        {
            switch (attackingType)
            {
                case AttackingType.Tower:
                    {
                        // Tower don't need to check distance, it always fire any target exist
                        canAttacking = false;

                        Debug.Log("Target distance: " + GameObjectUtility.Distance2dOfTwoGameObject(gameObject, target.gameObject));
                        // new CharacterAttackingFactory().GetAttackingStrategy(attackingType).PlayAttacking(target,attackingDamage);
                       
                        var prjBase = _projectileDataAsset.GetProjectileBase(_projectileId);
                        prjBase.GetProjectileMovement().GetLineRoute(startAttackPoint.position, EProjectileType.Arrow, target);
                        
                        await UniTask.Delay(TimeSpan.FromSeconds(attackingCooldown));

                        canAttacking = true;
                        return;
                    }
                case AttackingType.Melee:
                case AttackingType.Ranger:
                    {
                        // Need to check is in available attack range
                        if (GameObjectUtility.Distance2dOfTwoGameObject(this.gameObject, target.gameObject) < attackingRange)
                            return;
                        
                        canAttacking = false;

                        new CharacterAttackingFactory().GetAttackingStrategy(attackingType).PlayAttacking(target, attackingDamage);
                        await UniTask.Delay(TimeSpan.FromSeconds(attackingCooldown));

                        canAttacking = true;
                        return;
                    }
                default:
                    {
                        canAttacking = false;
                        return;
                    }
            }
        }

    }
    #endregion

}

internal interface ICharacterAttacking
{
    void PlayAttacking(UnitBase unitBase, float attackingDamage);
}

internal class CharacterAttackingFactory
{
    public ICharacterAttacking GetAttackingStrategy(AttackingType attackingType)
    {
        switch (attackingType)
        {
            case AttackingType.Melee:
                {
                    return new MeleeAttacking();
                }
            case AttackingType.Ranger:
                {
                    return new MeleeAttacking();
                }
            case AttackingType.Tower:
                {
                    return new TowerAttacking();
                }
            default:
                {
                    return new MeleeAttacking();
                }
        }
    }
}

internal class MeleeAttacking : ICharacterAttacking
{
    public void PlayAttacking(UnitBase target, float attackingDamage)
    {
        target.HealthComp().PlayHurting(attackingDamage);
    }
}

internal class TowerAttacking : ICharacterAttacking
{
    public void PlayAttacking(UnitBase target, float attackingDamage)
    {


    }
}

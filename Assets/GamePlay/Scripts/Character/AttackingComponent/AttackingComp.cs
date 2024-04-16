using Cysharp.Threading.Tasks;
using GamePlay.Scripts.Character.Stats;
using System;
using UnityEngine;
using UnityEngine.Serialization;

public class AttackingComp : UnitBaseComponent
{
    [Header("Attribute")]
    [SerializeField] private Transform startAttackPoint;
    [FormerlySerializedAs("attackingType")]
    [SerializeField] private TroopBehaviourType _troopBehaviourType;
    
    [SerializeField] private float attackingCooldown;
    [SerializeField] private float attackingDamage;
    [SerializeField] private float attackingRange;

    [Header("Data"), Space(12)]
    [SerializeField] private ProjectileDataAsset _projectileDataAsset;
    [SerializeField] private UnitId.Projectile _projectileId;

    private bool isNeedToWaitCoolDownAttacking = false;

    #region Event
    // private void OnEnable()
    // {
    //     _unitBaseParent.OnTargetChanging += AttackingTarget;
    // }
    // private void OnDisable()
    // {
    //     _unitBaseParent.OnTargetChanging -= AttackingTarget;
    // }
    #endregion

    #region Stats
    protected override void StatsUpdate()
    {
        SynData();
    }
    protected override void BuffUpdate()
    {
        SynData();
    }
    private void SynData()
    { 
        var stats = _unitBaseParent.UnitStatsHandlerComp();
        attackingDamage = stats.GetCurrentStatValue(StatId.AttackDamage);
        attackingRange = stats.GetCurrentStatValue(StatId.AttackRange);
    }
    #endregion
    
    #region Logic
    private async void AttackingTarget(UnitBase target)
    {
        if (target == null)
            return;

        if (!isNeedToWaitCoolDownAttacking)
        {
            switch (_troopBehaviourType)
            {
                case TroopBehaviourType.Tower:
                    {
                        // Tower don't need to check distance, it always fire any target exist
                        isNeedToWaitCoolDownAttacking = true;

                        Debug.Log("Target distance: " + GameObjectUtility.Distance2dOfTwoGameObject(gameObject, target.gameObject));
                        // new CharacterAttackingFactory().GetAttackingStrategy(attackingType).PlayAttacking(target,attackingDamage);
                       
                        var prjBase = _projectileDataAsset.GetProjectileBase(_projectileId.ToString());
                        prjBase.GetProjectileMovement().GetLineRoute(startAttackPoint.position, EProjectileType.Arrow, target);
                        
                        await UniTask.Delay(TimeSpan.FromSeconds(attackingCooldown));

                        isNeedToWaitCoolDownAttacking = false;
                        return;
                    }
                case TroopBehaviourType.Melee:
                case TroopBehaviourType.Ranger:
                    {
                        // Need to check is in available attack range
                        if (GameObjectUtility.Distance2dOfTwoGameObject(this.gameObject, target.gameObject) < attackingRange)
                            return;
                        
                        isNeedToWaitCoolDownAttacking = true;

                        new CharacterAttackingFactory().GetAttackingStrategy(_troopBehaviourType).PlayAttacking(target, attackingDamage);
                        await UniTask.Delay(TimeSpan.FromSeconds(attackingCooldown));

                        isNeedToWaitCoolDownAttacking = false;
                        return;
                    }
                default:
                    {
                        isNeedToWaitCoolDownAttacking = true;
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
    public ICharacterAttacking GetAttackingStrategy(TroopBehaviourType troopBehaviourType)
    {
        switch (troopBehaviourType)
        {
            case TroopBehaviourType.Melee:
                {
                    return new MeleeAttacking();
                }
            case TroopBehaviourType.Ranger:
                {
                    return new MeleeAttacking();
                }
            case TroopBehaviourType.Tower:
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

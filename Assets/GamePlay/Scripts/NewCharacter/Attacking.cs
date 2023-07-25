using Cysharp.Threading.Tasks;
using GamePlay.Scripts.Tower;
using SuperMaxim.Messaging;
using System;
using UnityEngine;
using UnityEngine.Events;

public enum AttackingType
{
    Tower,
    Melee = 1,
    Ranger = 2,
}
public class Attacking : MonoBehaviour
{
    [SerializeField] private AttackingType attackingType;
    [SerializeField] private float attackingCooldown  = 2f;
    [SerializeField] private int attackingDamage;
    [SerializeField] private float attackingRange = 12.95f;
    [SerializeField] private GameObject objAttackRange;
    [SerializeField] private BulletDataAsset bulletDataAsset;

    private bool canAttacking = true;
    
    private UnitBase _baseUnitBase;

    private void Awake()
    {
        Validate();
    }
    private void Validate()
    {
        if (_baseUnitBase == null)
        {
            _baseUnitBase = GetComponent<UnitBase>();
        }
    }
    private void OnEnable()
    {
        _baseUnitBase.OnCharacterChange += AttackingTarget;
    }
    private void OnDisable()
    {
        _baseUnitBase.OnCharacterChange -= AttackingTarget;
    }
    private void Start()
    {
        // attackingRange = GameObjectUtility.Distance2dOfTwoGameObject(this.gameObject.transform., objAttackRange.gameObject);
        attackingRange = 5;
    }
    private async void AttackingTarget(UnitBase target)
    {
        if (target == null)
        {
            return;
        }
        if (canAttacking && attackingType == AttackingType.Tower)
        {
            canAttacking = false;
            // new CharacterAttackingFactory().GetAttackingStrategy(attackingType).PlayAttacking(target,attackingDamage);
            bulletDataAsset.GetLineRoute(transform.position,BulletType.Arrow,target);
            await UniTask.Delay(TimeSpan.FromSeconds(attackingCooldown));
            canAttacking = true;
        }
        else if (canAttacking && GameObjectUtility.Distance2dOfTwoGameObject(this.gameObject,target.gameObject) < attackingRange)
        {
            // AttackMachineUtility.GetCooldownTime()
            
            canAttacking = false;
            new CharacterAttackingFactory().GetAttackingStrategy(attackingType).PlayAttacking(target,attackingDamage);
            await UniTask.Delay(TimeSpan.FromSeconds(attackingCooldown));
            canAttacking = true;
        }
        
    }
}
internal interface ICharacterAttacking
{
    void PlayAttacking(UnitBase unitBase, int attackingDamage);
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
    public void PlayAttacking(UnitBase target, int attackingDamage)
    {
        target.HealthComp().PlayHurting(attackingDamage);
    }
}
internal class TowerAttacking : ICharacterAttacking
{
    public void PlayAttacking(UnitBase target, int attackingDamage)
    {
        
        
    }
}
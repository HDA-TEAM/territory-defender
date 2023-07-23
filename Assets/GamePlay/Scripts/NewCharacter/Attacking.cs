using Cysharp.Threading.Tasks;
using GamePlay.Scripts.Tower;
using SuperMaxim.Messaging;
using System;
using UnityEngine;
using UnityEngine.Events;

public enum AttackingType
{
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

    private bool canAttacking = true;
    
    private Character baseCharacter;

    private void Awake()
    {
        Validate();
    }
    private void Validate()
    {
        if (baseCharacter == null)
        {
            baseCharacter = GetComponent<Character>();
        }
    }
    private void OnEnable()
    {
        baseCharacter.OnCharacterChange += AttackingTarget;
    }
    private void OnDisable()
    {
        baseCharacter.OnCharacterChange -= AttackingTarget;
    }
    private void Start()
    {
        // attackingRange = GameObjectUtility.Distance2dOfTwoGameObject(this.gameObject.transform., objAttackRange.gameObject);
        attackingRange = 5;
    }
    private async void AttackingTarget(Character target)
    {
        if (target == null)
        {
            return;
        }
        if (canAttacking && GameObjectUtility.Distance2dOfTwoGameObject(this.gameObject,target.gameObject) < attackingRange)
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
    void PlayAttacking(Character character, int attackingDamage);
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
            default:
                {
                    return new MeleeAttacking();
                }
        }
    }
}
internal class MeleeAttacking : ICharacterAttacking
{
    public void PlayAttacking(Character target, int attackingDamage)
    {
        target.HealthComp().PlayHurting(attackingDamage);
    }
}

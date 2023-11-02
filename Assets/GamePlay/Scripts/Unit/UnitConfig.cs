using System;
using UnityEngine;


[Serializable]
public struct UnitAttribute
{
    // [Header("Attack")]
    // public float attackRange;
    // public float attackCoolDown;
    // public float attackSpeed;
    // public float attackDamage;
    // public float detectRange;
    // [Header("Health")]
    // public float health;
    // [Header("Movement")]
    // public float movementSpeed;
    // // public UnitAttribute(float attackRange,float detectRange,float health,float movementSpeed,float attackSpeed,float attackDamage,float attackCoolDown = 0)
    // // {
    // public UnitAttribute(UnitConfig unitConfig)
    // {
    //     this.attackRange = unitConfig.attackRange;
    //     this.detectRange = unitConfig.detectRange;
    //     this.health = unitConfig.health;
    //     this.movementSpeed = unitConfig.MovementSpeed;
    //     this.attackSpeedMax = unitConfig.attackSpeedMax;
    //     this.attackSpeedMin = unitConfig.attackSpeedMin;
    //     this.attackDamage = unitConfig.attackDamage;
    //     this.attackCoolDown = 0;
    // }
    //
}
[CreateAssetMenu(fileName = "Unit", menuName = "ScriptableObject/Unit/UnitBase")]
public class UnitConfig : ScriptableObject
{
    [Space(20)]
    [Header("Range")]
    [SerializeField] private float _attackRange;
    [SerializeField] private float _detectRange;
    [Header("Health")]
    [SerializeField] private float _health;
    [Header("Movement")]
    [SerializeField] private float _movementSpeed;
    [Header("Dame")]
    [SerializeField] private float _attackDamage;
    [Header("AttackSpeed")]
    [SerializeField] private float _attackSpeedMax;
    [SerializeField] private float _attackSpeedMin;
    // [Header("Skill")]
    // public float cooldown;
    // public float MovementSpeed
    // {
    //     get
    //     {
    //         return movementSpeed;
    //     }
    // }
}

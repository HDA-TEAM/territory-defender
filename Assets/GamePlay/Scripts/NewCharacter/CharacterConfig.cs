
using System;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterKey
{
    //Ally 
    AllyArcher = 101,
    AllySpear = 101,

    //Enemy
    EnemyArcher = 201,
    EnemySword = 202,
    EnemyShield = 203,
    EnemyAssassin = 204,
}

[Serializable]
public struct CharacterAttributeFollowLevel
{
    public int level;
    public UnitAttribute UnitAttribute;
}

[Serializable]
public struct CharacterAttribute
{
    public CharacterKey CharacterKey;
    public List<CharacterAttributeFollowLevel> CharacterAttributeFollowLevels;
}

public class CharacterConfig : ScriptableObject
{
    [SerializeField] private float _attackRange;
    [SerializeField] private float _detectRange;
    [Header("Health")]
    [SerializeField] private float _health;
    [Header("Movement")]
    [SerializeField] private float _movementSpeed;
    [Header("Dame")]
    [SerializeField] private float _attackDamage;
    [Header("AttackSpeed")]
    [SerializeField] private float _attackSpeed;

    // public List<CharacterAttribute> characterAttributes;
}

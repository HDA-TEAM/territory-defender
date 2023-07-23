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

[CreateAssetMenu(fileName = "CharacterConfig", menuName = "ScriptableObject/CharacterConfig")]
public class CharacterConfig : ScriptableObject
{
    public List<CharacterAttribute> characterAttributes;
}

public class CharacterConfigManager : MonoBehaviour
{
    
}

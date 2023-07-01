using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine;

public enum TowerKey
{
    BambooArcher = 1,
    CombatDrum = 2,
    WarElephant = 3,
    ConeWarrior = 4,
}

[Serializable]
public struct TowerAttributeFollowLevel
{
    public int Level;
    public float Price;
    public UnitAttribute UnitAttribute;
}

[Serializable]
public struct TowerAttribute
{
    public TowerKey TowerKey;
    public List<TowerAttributeFollowLevel> TowerAttributeFollowLevels;

}
[CreateAssetMenu(fileName = "TowerConfig", menuName = "ScriptableObject/TowerConfig")]
public class TowerConfig : ScriptableObject
{
    public List<TowerAttribute> towerAttributes;
}

public class TowerConfigManager : Singleton<TowerConfigManager>
{
    
}

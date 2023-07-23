using System.Collections.Generic;
using UnityEngine;

public class CheckingCombatJoinIn : MonoBehaviour
{
    private Dictionary<AttackingType, Character> dicCurCombatMember;
    public bool CheckingCanJoinIn(Character current)
    {
        return true;
    }
}


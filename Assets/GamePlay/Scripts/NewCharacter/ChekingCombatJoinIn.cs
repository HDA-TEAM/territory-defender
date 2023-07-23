using System.Collections.Generic;
using UnityEngine;

public class CheckingCombatJoinIn : MonoBehaviour
{
    private Dictionary<AttackingType, UnitBase> dicCurCombatMember;
    public bool CheckingCanJoinIn(UnitBase current)
    {
        return true;
    }
}


using System.Collections.Generic;
using UnityEngine;

public class CheckingCombatJoinInComp : MonoBehaviour
{
    private Dictionary<TroopBehaviourType, UnitBase> dicCurCombatMember;
    public bool CheckingCanJoinIn(UnitBase current)
    {
        return true;
    }
}


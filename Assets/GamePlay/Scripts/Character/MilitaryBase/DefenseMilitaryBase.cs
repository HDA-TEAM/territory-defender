using System;
using System.Collections.Generic;
using UnityEngine;

public struct DefenseMilitaryComposite
{
    public Transform CampingPlace;
    public Action<UnitBase> OnDetectSuspect;
    public Action<UnitBase> OnSuspectOut;
    
}
public class DefenseMilitaryBase : MonoBehaviour
{
    [SerializeField] private List<UnitBase> _defenseUnits = new List<UnitBase>();
    [SerializeField] private List<UnitBase> _suspectUnits = new List<UnitBase>();
    [SerializeField] private Transform _campingPlace;

    private void Awake()
    {
        InitBase();
    }
    private void InitBase()
    {
        foreach (var defenseUnit in _defenseUnits)
        {
            defenseUnit.DefenderDetecting().Setup(new DefenseMilitaryComposite
            {
                CampingPlace = _campingPlace,
                OnDetectSuspect = OnDetectSuspect,
                OnSuspectOut = OnSuspectOut
            });
        }
    }
    public enum MilitaryCommand
    {
        Block = 1,
        Attack = 2,
    }

    private void OnUpdateCampingPlace()
    {
        
    }
    private void OnDetectSuspect(UnitBase suspectUnit)
    {
        if (suspectUnit != null && !_suspectUnits.Contains(suspectUnit))
        {
            _suspectUnits.Add(suspectUnit);
            Debug.LogError("OnDetectSuspect");
            UpdateTargeting();
        }
        
    }
    private void OnSuspectOut(UnitBase suspectUnit)
    {
        if (suspectUnit != null && _suspectUnits.Contains(suspectUnit))
        {
            _suspectUnits.Remove(suspectUnit);
            Debug.LogError("OnSuspectOut");
        }
    }
    #region Helper
    private void UpdateTargeting()
    {
        foreach (var defenseUnit in _defenseUnits)
        {
            // if (defenseUnit.GetUnitState() != UnitBase.UnitState.Free)
            //     continue;
            
            switch (defenseUnit.UnitTypeId())
            {
                case UnitBase.UnitType.Melee:
                    {
                        // Set Command Block
                        defenseUnit.OnTargetChanging?.Invoke(_suspectUnits[0]);
                        _suspectUnits[0].OnTargetChanging?.Invoke(defenseUnit);
                        break;
                    }
                case UnitBase.UnitType.Range:
                    {
                        // Set Command Attack
                        defenseUnit.OnTargetChanging?.Invoke(_suspectUnits[0]);
                        _suspectUnits[0].OnTargetChanging?.Invoke(defenseUnit);
                        break;
                    }
            }
        }
    }
    // private List<UnitBase> GetSuspectUnitsAvailableTargeting(UnitBase defender)
    // {
    //     List<UnitBase> availableTargetingUnits = new List<UnitBase>();
    //     foreach (var suspectUnit in _suspectUnits)
    //     {
    //         if (GameObjectUtility.Distance2dOfTwoGameObject(_campingTransform.gameObject, suspectUnit.gameObject) < defender.UnitStatsComp().GetStat(StatId.DetectRange))
    //             availableTargetingUnits.Add(suspectUnit);
    //     }
    //     return availableTargetingUnits;
    // }
    // private UnitBase FindTarget(UnitBase defender)
    // {
    //     UnitBase target = null;
    //     var minDis = float.MaxValue;
    //     foreach (var suspectUnit in _suspectUnits)
    //     {
    //         var curDis = GameObjectUtility.Distance2dOfTwoGameObject(_campingTransform.gameObject, suspectUnit.gameObject);
    //         if (curDis < defender.UnitStatsComp().GetStat(StatId.DetectRange) 
    //             && curDis < minDis)
    //         {
    //             target = suspectUnit;
    //         }
    //             
    //     }
    //     return target;
    // }
    #endregion
}

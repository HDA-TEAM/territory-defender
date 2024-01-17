using System;
using System.Collections.Generic;
using UnityEngine;

public struct DefenseMilitaryComposite
{
    public Transform CampingPlace;
    public Action<UnitBase> OnDetectSuspect;
    public Action<UnitBase> OnSuspectOut;
    
}
public enum BeingTargetCommand
{
    None = 0,
    Block = 1,
    Attack = 2,
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
    

    private void OnUpdateCampingPlace()
    {
        
    }
    private void OnDetectSuspect(UnitBase suspectUnit)
    {
        if (suspectUnit != null && !_suspectUnits.Contains(suspectUnit))
        {
            _suspectUnits.Add(suspectUnit);
            UpdateTargeting();
        }
        
    }
    private void OnSuspectOut(UnitBase suspectUnit)
    {
        if (suspectUnit != null && _suspectUnits.Contains(suspectUnit))
        {
            _suspectUnits.Remove(suspectUnit);
            UpdateTargeting();
        }
    }
    #region Helper
    private void UpdateTargeting()
    {
        foreach (var defenseUnit in _defenseUnits)
        {
            if (!defenseUnit.gameObject.activeSelf || _suspectUnits.Count <= 0)
                continue;
            switch (defenseUnit.UnitTypeId())
            {
                case UnitBase.UnitType.Melee:
                    {
                        var suspectTargetChangingComposite = new UnitBase.OnTargetChangingComposite
                        {
                            Target = _suspectUnits[0],
                            BeingTargetCommand = BeingTargetCommand.None
                        };
                        defenseUnit.OnTargetChanging?.Invoke(suspectTargetChangingComposite);
                        var defenderTargetChangingComposite = new UnitBase.OnTargetChangingComposite
                        {
                            Target = defenseUnit,
                            BeingTargetCommand = BeingTargetCommand.Block
                        };
                        _suspectUnits[0].OnTargetChanging?.Invoke(defenderTargetChangingComposite);
                        break;
                    }
                case UnitBase.UnitType.Range:
                    {
                        var suspectTargetChangingComposite = new UnitBase.OnTargetChangingComposite
                        {
                            Target = _suspectUnits[0],
                            BeingTargetCommand = BeingTargetCommand.None
                        };
                        defenseUnit.OnTargetChanging?.Invoke(suspectTargetChangingComposite);
                        var defenderTargetChangingComposite = new UnitBase.OnTargetChangingComposite
                        {
                            Target = defenseUnit,
                            BeingTargetCommand = BeingTargetCommand.None
                        };
                        _suspectUnits[0].OnTargetChanging?.Invoke(defenderTargetChangingComposite);
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

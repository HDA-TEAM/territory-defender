using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitObserver : SingletonBase<UnitObserver>
{
    [SerializeField] private List<UnitBase> _unitAllys = new List<UnitBase>();
    [SerializeField] private List<UnitBase> _unitEnemies = new List<UnitBase>();
    public void Subscribe(UnitBase unitBase)
    {
        if (unitBase.gameObject.tag == UnitId.Enemy.ToString())
        {
            _unitEnemies.Add(unitBase);
        }
        else if (unitBase.gameObject.tag == UnitId.Ally.ToString())
        {
            _unitAllys.Add(unitBase);
        }
    }
    public void UnSubscribe(UnitBase unitBase)
    {
        if (unitBase.gameObject.tag == UnitId.Enemy.ToString() && _unitEnemies.Contains(unitBase))
        {
            _unitEnemies.Remove(unitBase);
        }
        else if (unitBase.gameObject.tag == UnitId.Ally.ToString() && _unitAllys.Contains(unitBase))
        {
            _unitAllys.Remove(unitBase);
        }
    }
    public void NotifyAllUnit(string unitId, UnitBase unitOut)
    {
        // var listUnit = unitId switch
        // {
        //     UnitId.Ally/ToString() => _unitAllys,
        //     UnitId.Enemy => _unitEnemies,
        // };
        Debug.LogError(unitId);
        var listUnit = unitId == UnitId.Ally.ToString() ?_unitEnemies : _unitAllys;
        foreach (var unit in listUnit)
        {
            unit.OnRecheckTarget?.Invoke();
        }
    }
    
}

using System.Collections.Generic;
using UnityEngine;

public class UnitObserver : SingletonBase<UnitObserver>
{
    [SerializeField] private List<UnitBase> _unitAllys = new List<UnitBase>();
    [SerializeField] private List<UnitBase> _unitEnemies = new List<UnitBase>();
    public bool s;
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
    private void NotifyAllUnit(UnitId unitId, UnitBase unitOut)
    {
        var listUnit = unitId switch
        {
            UnitId.Ally => _unitAllys,
            UnitId.Enemy => _unitEnemies,
        };
        foreach (var unit in listUnit)
        {
            if (unit.OnTargetChanging.Target == unitOut)
            {
                unit.OnTargetChanging.Target ==
            }
        }
    }
}

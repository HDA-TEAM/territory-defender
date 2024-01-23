using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : SingletonBase<UnitManager>
{
    [SerializeField] private List<UnitBase> _unitAllys = new List<UnitBase>();
    [SerializeField] private List<UnitBase> _unitEnemies = new List<UnitBase>();
    public void Subscribe(UnitBase unitBase)
    {
        if (unitBase.gameObject.tag == UnitId.Enemy.ToString())
            _unitEnemies.Add(unitBase);
        else if (unitBase.gameObject.tag == UnitId.Ally.ToString())
            _unitAllys.Add(unitBase);
    }
    public void UnSubscribe(UnitBase unitBase)
    {
        if (unitBase.gameObject.tag == UnitId.Enemy.ToString() && _unitEnemies.Contains(unitBase))
            _unitEnemies.Remove(unitBase);
        else if (unitBase.gameObject.tag == UnitId.Ally.ToString() && _unitAllys.Contains(unitBase))
            _unitAllys.Remove(unitBase);
        NotifyAllUnit(unitBase);
    }
    public void Update()
    {
        ClearUnavailableUnit();
        foreach (var enemy in _unitEnemies)
            enemy.UnitController().UpdateStatus(_unitAllys);
        foreach (var ally in _unitAllys)
            ally.UnitController().UpdateStatus(_unitEnemies);
    }
    private void ClearUnavailableUnit()
    {
        _unitAllys.RemoveAll((unit) => unit.gameObject.activeSelf == false);
        _unitEnemies.RemoveAll((unit) => unit.gameObject.activeSelf == false);
    }
    private void NotifyAllUnit(UnitBase unitOut)
    {
        UnitBase.OnTargetChangingComposite targetChangingComposite = new UnitBase.OnTargetChangingComposite();
        targetChangingComposite.SetDefault();
        foreach (var enemy in _unitEnemies)
            enemy.OnTargetChanging?.Invoke(targetChangingComposite);
        foreach (var ally in _unitAllys)
            ally.OnTargetChanging?.Invoke(targetChangingComposite);
    }
    
}

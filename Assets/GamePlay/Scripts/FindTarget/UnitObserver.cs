using Common.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitObserver : SingletonBase<UnitObserver>
{
    private readonly List<UnitBase> _unitAllies = new List<UnitBase>();
    private readonly List<UnitBase> _unitEnemies = new List<UnitBase>();

    private readonly CampingRoomController _campingRoomController = new CampingRoomController();
    private readonly SingleTargetController _singleTargetController = new SingleTargetController();
    public CampingRoomController CampingRoomController => _campingRoomController;
    public SingleTargetController SingleTargetController => _singleTargetController;
    public void AddUnit(UnitBase unitBase)
    {
        if (unitBase.tag == UnitConstant.AllyTag && !_unitAllies.Contains(unitBase))
        {
            _unitAllies.Add(unitBase);
            unitBase.OnOutOfHeal += OnOutOfHeal;
            unitBase.OnResetFindTarget += OnUpdateAll;
        }
        else if (unitBase.tag == UnitConstant.EnemyTag && !_unitEnemies.Contains(unitBase))
        {
            _unitEnemies.Add(unitBase);
            unitBase.OnOutOfHeal += OnOutOfHeal;
            unitBase.OnResetFindTarget += OnUpdateAll;
        }
    }
    private void OnUpdateAll()
    {
        _campingRoomController.FindTarget();
        _singleTargetController.FindTarget();
    }
    private void RemoveUnit(UnitBase unitBase)
    {
        _unitAllies.Remove(unitBase);
        _unitEnemies.Remove(unitBase);
        unitBase.OnOutOfHeal -= OnOutOfHeal;
        unitBase.OnResetFindTarget -= OnUpdateAll;
    }
    private void OnOutOfHeal(UnitBase unitWilOut)
    {
        OnUpdateAll();
        RemoveUnit(unitWilOut);
    }
    // private void OnDestroy()
    // {
    //     foreach (var unitAlly in _unitAllies)
    //         RemoveUnit(unitAlly);
    //     foreach (var unitEnemy in _unitEnemies)
    //         RemoveUnit(unitEnemy);
    // }
}

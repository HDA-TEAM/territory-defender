using GamePlay.Scripts.GamePlay;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum BeingTargetCommand
{
    None = 0,
    Block = 1,
    Attack = 2,
}

public class UnitManager : GamePlaySingletonBase<UnitManager>
{
    [SerializeField] private List<UnitBase> _unitAllys = new List<UnitBase>();
    [SerializeField] private List<UnitBase> _unitEnemies = new List<UnitBase>();
    
    private readonly List<Action> _onSubscribeAction = new List<Action>();
    private readonly List<Action> _onUnSubscribeAction = new List<Action>();
    private readonly List<Action> _onUnitOutAction = new List<Action>();

    // A active and available unit can be subscribe
    public void Subscribe(UnitBase unitBase)
    {
        _onSubscribeAction.Add(() => OnSubscribe(unitBase));
    }
    private void OnSubscribe(UnitBase unitBase)
    {
        if (unitBase.gameObject.CompareTag(UnitSideId.Enemy.ToString()))
            _unitEnemies.Add(unitBase);
        else if (unitBase.gameObject.CompareTag(UnitSideId.Ally.ToString()) || unitBase.gameObject.CompareTag(UnitSideId.Tower.ToString()))
            _unitAllys.Add(unitBase);
    }
    // A de-active or unavailable unit will be UnSubscribe
    public void UnSubscribe(UnitBase unitBase)
    {
        _onUnSubscribeAction.Add(() => OnUnSubscribe(unitBase));
    }
    private void OnUnSubscribe(UnitBase unitBase)
    {
        if (unitBase.gameObject.CompareTag(UnitSideId.Enemy.ToString()) && _unitEnemies.Contains(unitBase))
            _unitEnemies.Remove(unitBase);
        else if ((unitBase.gameObject.CompareTag(UnitSideId.Ally.ToString()) || unitBase.gameObject.CompareTag(UnitSideId.Tower.ToString()))
                 && _unitAllys.Contains(unitBase))
            _unitAllys.Remove(unitBase);
        NotifyAllUnit(unitBase);
    }

    // Reset single Unit from outside handle
    public void ResetTarget(UnitBase unitBase) => _onUnitOutAction.Add(() => NotifyAllUnit(unitBase));
    
    // Update units on map
    public void Update()
    {
        if (!InGameStateController.Instance.IsGamePlaying)
            return;

        SynRuntimeAction();

        ClearUnavailableUnit();

        foreach (var enemy in _unitEnemies)
        {
            _unitsNeed = GetUnitsNeed(enemy.TargetSideNeeding()[0]);
            enemy.UnitController().UpdateStatus(_unitsNeed);
        }
        foreach (var ally in _unitAllys)
        {
            _unitsNeed = GetUnitsNeed(ally.TargetSideNeeding()[0]);
            ally.UnitController().UpdateStatus(_unitsNeed);
        }
    }
    private List<UnitBase> _unitsNeed;
    private readonly List<UnitBase> _allys = new List<UnitBase>();
    private List<UnitBase> GetUnitsNeed(UnitId.BaseId baseId)
    {
        switch (baseId)
        {
            case UnitId.BaseId.Ally:
                {
                    _allys.Clear();
                    foreach (var unit in _unitAllys)
                    {
                        if (unit.CharacterStateMachine().CharacterTroopBehaviourType != TroopBehaviourType.Tower)
                        {
                            _allys.Add(unit);
                        }
                    }
                    return _allys;
                }
            case UnitId.BaseId.Enemy:
                {
                    return _unitEnemies;
                }

        }
        return new List<UnitBase>();
    }
    // A de-active or unavailable unit will be remove
    private void ClearUnavailableUnit()
    {
        _unitAllys.RemoveAll(unit => unit.gameObject.activeSelf == false);
        _unitEnemies.RemoveAll(unit => unit.gameObject.activeSelf == false);
    }

    // Ensuring always exist single unit updating flow 
    private void SynRuntimeAction()
    {
        foreach (var action in _onUnitOutAction)
            action?.Invoke();
        _onUnitOutAction.Clear();
        
        foreach (var action in _onSubscribeAction)
            action?.Invoke();
        _onSubscribeAction.Clear();

        foreach (var action in _onUnSubscribeAction)
            action?.Invoke();
        _onUnSubscribeAction.Clear();
    }

    // When having an unit out, we will notify for all unit to know that
    private void NotifyAllUnit(UnitBase unitOut)
    {
        UnitBase.OnTargetChangingComposite targetChangingComposite = new UnitBase.OnTargetChangingComposite();
        targetChangingComposite.SetDefault();
        foreach (var enemy in _unitEnemies)
        {
            if (enemy.CurrentTarget == unitOut)
                enemy.OnTargetChanging?.Invoke(targetChangingComposite);
        }

        foreach (var ally in _unitAllys)
            if (ally.CurrentTarget == unitOut)
                ally.OnTargetChanging?.Invoke(targetChangingComposite);

        unitOut.CurrentTarget = null;
        unitOut.OnTargetChanging?.Invoke(targetChangingComposite);
    }
    public override void SetUpNewGame()
    {
    }
    public override void ResetGame()
    {
        Destroy(gameObject);
    }
}

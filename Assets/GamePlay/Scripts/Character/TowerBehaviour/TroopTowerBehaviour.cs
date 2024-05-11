using Common.Scripts;
using Common.Scripts.Utilities;
using Cysharp.Threading.Tasks;
using GamePlay.Scripts.Character.StateMachine.TowerBehaviour;
using GamePlay.Scripts.Character.Stats;
using GamePlay.Scripts.GamePlayController;
using GamePlay.Scripts.Tower.TowerKIT;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TroopTowerBehaviour : TowerBehaviourBase
{
    // Default 3 units
    private readonly int _maxAllyCount = 3;
    private readonly float _minPerUnitDistance = 0.5f;
    private readonly float _cooldownReviveUnit = 1f;
    [SerializeField] private List<UnitBase> _allyUnits = new List<UnitBase>();
    [SerializeField] private float _campingRange;
    [SerializeField] private Vector3 _campingPos;
    private Vector3 _parentPos;
    protected override void StatsUpdate()
    {
        var stats = _unitBaseParent.UnitStatsHandlerComp();
        _campingRange = stats.GetCurrentStatValue(StatId.CampingRange);
    }
    private void OnEnable()
    {
        StatsUpdate();
    }
    public override void Setup(TowerKit towerKit)
    {
        base.Setup(towerKit);
        _parentPos = _towerKit.transform.position;
        Debug.Log("transform.position " + this.transform.position);
        for (int i = 0; i < _maxAllyCount; i++)
        {
            SpawnSingleUnit(UnitId.Ally.Warrior.ToString());
        }
        Debug.Log("_parentPos " + _parentPos);
        var campingPos = RouteSetController.Instance.GetNearestPosFromRoute(_parentPos);
        Debug.Log("campingPos " + campingPos);
        SetCampingPlace(campingPos);
    }

    /// Spawning new object from pool and set on revive for it
    private void SpawnSingleUnit(string objectType)
    {
        var ally = PoolingController.Instance.SpawnObject(objectType, _parentPos);
        UnitBase unitBase = ally.GetComponent<UnitBase>();
        unitBase.UnitReviveHandlerComp().SetupRevive(OnWaitingToRevive);
        unitBase.OnUpdateStats?.Invoke();
        _allyUnits.Add(unitBase);
    }
    private void OnDisable()
    {
        // When tower is remove, all unit which tower control should clear
        foreach (UnitBase unitBase in _allyUnits)
        {
            if (unitBase && unitBase.gameObject)
            {
                // remove callback for reviving first
                unitBase.UnitReviveHandlerComp().OnRemoveRevive();
                // Return to pool
                unitBase.gameObject.SetActive(false);
            }
        }

    }
    private async void OnWaitingToRevive(UnitBase unitBase)
    {
        // remove old unit and returning it to the pool
        _allyUnits.Remove(unitBase);
        await UniTask.Delay(TimeSpan.FromSeconds(_cooldownReviveUnit));

        // Spawning new unit from pool
        SpawnSingleUnit(UnitId.Ally.Warrior.ToString());

    }
    // private void 
    public void SetCampingPlace(Vector3 newCampingPos)
    {
        // Get current tower pos on the map
        Vector3 parentPos = _towerKit.transform.position;
        if (VectorUtility.Distance2dOfTwoPos(newCampingPos, parentPos) > _campingRange)
            return;

        _campingPos = newCampingPos;

        // Set camping pos for each unit
        for (int i = 0; i < _allyUnits.Count; i++)
        {
            Vector3 curUnitCampingPlace = GetCampingPlaceOffset(_maxAllyCount, i, _campingPos);
            // Moving to camping pos
            _allyUnits[i].UserActionController().SetMovingPosition(curUnitCampingPlace);
        }
    }
    private Vector3 GetCampingPlaceOffset(int maxNumber, int index, Vector3 startPos)
    {
        float startDegree = -90; // Ensure first unit will be create at bottom middle place
        float curDegree = (360f * index / maxNumber) + startDegree;
        float curRadian = curDegree * Mathf.Deg2Rad;
        return startPos + new Vector3(_minPerUnitDistance * Mathf.Cos(curRadian), _minPerUnitDistance * Mathf.Sin(curRadian), 0f);
    }
}

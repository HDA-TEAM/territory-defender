using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

public interface OnReviveUnit
{
    
}
public class TroopTowerBehaviour : UnitBaseComponent
{
    // Default 3 units
    private readonly int _maxAllyCount = 3;
    private readonly float _minPerUnitDistance = 0.5f;
    private float _cooldownReviveUnit = 1f;
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
        _parentPos = TowerKitSetController.Instance.CurrentSelectedKit.transform.position;
        for (int i = 0; i < _maxAllyCount; i++)
        {
            SpawnSingleUnit(UnitId.Ally.Warrior.ToString());
        }
        var campingPos = RouteSetController.Instance.GetNearestPosFromRoute(_parentPos);
        SetCampingPlace(campingPos);
    }
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
        foreach (var ally in _allyUnits)
        {
            if (ally && ally.gameObject)
            {
                ally.gameObject.SetActive(false);
            }
        }
    }
    private async void OnWaitingToRevive(UnitBase unitBase)
    {
        _allyUnits.Remove(unitBase);
        await UniTask.Delay(TimeSpan.FromSeconds(_cooldownReviveUnit));
        SpawnSingleUnit(UnitId.Ally.Warrior.ToString());
    }
    // private void 
    public void SetCampingPlace(Vector3 newCampingPos)
    {
        Vector3 parentPos = TowerKitSetController.Instance.CurrentSelectedKit.transform.position;
        if (VectorUtility.Distance2dOfTwoPos(newCampingPos, parentPos) > _campingRange)
            return;

        _campingPos = newCampingPos;
        for (int i = 0; i < _allyUnits.Count; i++)
        {
            Vector3 curUnitCampingPlace = GetCampingPlaceOffset(_maxAllyCount, i, _campingPos);
            Debug.Log("curUnitCampingPlace" + curUnitCampingPlace);
            // Moving to camping pos
            _allyUnits[i].UserActionController().SetMovingPosition(curUnitCampingPlace);
        }
    }
    private Vector3 GetCampingPlaceOffset(int maxNumber, int index, Vector3 startPos)
    {
        float startDegree = -90; // Ensure first unit will be create at bottom middle place
        float curDegree = (360f * index / maxNumber) + startDegree;
        float curRadian = curDegree * Mathf.Deg2Rad;
        return startPos +  new Vector3(_minPerUnitDistance * Mathf.Cos(curRadian), _minPerUnitDistance * Mathf.Sin(curRadian), 0f);
    }
}

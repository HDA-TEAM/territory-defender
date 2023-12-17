using System;
using System.Collections.Generic;
using UnityEngine;

public class TroopTowerBehaviour : UnitBaseComponent
{
    private readonly int _maxAllyCount = 3;
    [SerializeField] private List<UnitBase> _allyUnits = new List<UnitBase>();
    [SerializeField] private float _campingRange;
    [SerializeField] private Vector3 _campingPos;
    protected override void StatsUpdate()
    {
        var stats = _unitBaseParent.UnitStatsComp();
        _campingRange = stats.GetStat(StatId.CampingRange);
    }
    private void OnEnable()
    {
        //todo: Default camping at most nearest place on the route
        // SetCampingPlace(Vector3.zero);
        for (int i = 0; i < _maxAllyCount; i++)
        {
            var ally = PoolingController.Instance.SpawnObject(UnitId.Ally, _unitBaseParent.gameObject.transform.position);
            Debug.LogError("Spawn pos : " + gameObject.transform.position);
            _allyUnits.Add(ally.GetComponent<UnitBase>());
        }
        
    }

    private void OnDisable()
    {
        foreach (var ally in _allyUnits)
        {
            ally.gameObject.SetActive(false);
        }
    }
    public void SetCampingPlace(Vector3 campingPos)
    {
        _campingPos = campingPos;
        foreach (var ally in _allyUnits)
        {
            ally.gameObject.transform.position = _campingPos;
        }
    }
}

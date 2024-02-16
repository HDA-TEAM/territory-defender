using System.Collections.Generic;
using UnityEngine;

public class TroopTowerBehaviour : UnitBaseComponent
{
    private readonly int _maxAllyCount = 1;
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
        Vector3 parentPos = TowerKitSetController.Instance.CurrentSelectedKit.transform.position;
        for (int i = 0; i < _maxAllyCount; i++)
        {
            var ally = PoolingController.Instance.SpawnObject(UnitId.AllyWarrior, parentPos);
            _allyUnits.Add(ally.GetComponent<UnitBase>());
        }
        var campingPos = RouteSetController.Instance.GetNearestPosFromRoute(parentPos);
        SetCampingPlace(campingPos);
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
    public void SetCampingPlace(Vector3 campingPos)
    {
        _campingPos = campingPos;
        foreach (var ally in _allyUnits)
        {
            ally.gameObject.transform.position = _campingPos;
            Debug.LogError("Camping " + ally.gameObject.transform.position);
        }
    }
}

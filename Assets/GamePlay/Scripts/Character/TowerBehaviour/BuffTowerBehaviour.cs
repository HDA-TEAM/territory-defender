using System.Collections.Generic;
using UnityEngine;

public class BuffTowerBehaviour : UnitBaseComponent
{
    
    [SerializeField] private List<UnitBase> _buffsBuffer;
    [SerializeField] private float _buffRange;
    
    protected override void StatsUpdate()
    {
        var stats = _unitBaseParent.UnitStatsHandlerComp();
        _buffRange = stats.GetCurrentStatValue(StatId.BuffRange);
    }
   
}

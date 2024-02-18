using UnityEngine;

public class BuffTowerBehaviour : UnitBaseComponent
{
    [SerializeField] private float _buffRange;
    protected override void StatsUpdate()
    {
        var stats = _unitBaseParent.UnitStatsComp();
        _buffRange = stats.GetStat(StatId.BuffRange);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        
    }
}

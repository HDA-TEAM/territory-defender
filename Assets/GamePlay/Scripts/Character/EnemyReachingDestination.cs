using UnityEngine;

public class EnemyReachingDestination : UnitBaseComponent
{
    [SerializeField] private InGameInventoryDataAsset _inGameInventoryDataAsset;
    public void OnReachingDestination()
    {
        // return pooling and status
        _inGameInventoryDataAsset.TryChangeLife(
            - (int)_unitBaseParent.UnitStatsComp().GetStat(StatId.LifeReduce));
        _unitBaseParent.HealthComp().ResetState();
    }
}


using UnityEngine;
using UnityEngine.Serialization;

public class EnemyReachingDestination : UnitBaseComponent
{
    [FormerlySerializedAs("_inGameInventoryEvent")]
    [FormerlySerializedAs("_inGameInventoryDataAsset")]
    [SerializeField] private InGameInventoryRuntimeData _inGameInventoryRuntimeData;
    public void OnReachingDestination()
    {
        // return pooling and status
        _inGameInventoryRuntimeData.TryChangeLife(
            - (int)_unitBaseParent.UnitStatsComp().GetStat(StatId.LifeReduce));
        _unitBaseParent.HealthComp().ResetState();
    }
}


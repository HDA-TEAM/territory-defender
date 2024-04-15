using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct StageInventory
{
    public StageId StageId;
    public int Currency;
    public int MaxLife;
    public int StarClaimable;
    
}

[CreateAssetMenu(fileName = "StageInventoryConfig", menuName = "ScriptableObject/InGameConfig/StageInventoryConfig")]
public class StageInventoryConfig : ScriptableObject
{
    [SerializeField] private List<StageInventory> _stageInventories;

    public StageInventory GetStageInventory(StageId stageId)
    {
        foreach (var stageInventory in _stageInventories)
        {
            if (stageInventory.StageId == stageId)
            {
                return stageInventory;
            }
        }
        return new StageInventory();
    }
}

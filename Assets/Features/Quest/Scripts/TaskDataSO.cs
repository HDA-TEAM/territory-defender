using System;
using System.Collections.Generic;
using Common.Scripts.Data.DataAsset;
using UnityEngine;
using UnityEngine.Serialization;

namespace Features.Quest.Scripts
{
    public enum TaskId
    {
        None = 0,
        Logging = 1,
        Gathering = 2,
        Winning1Stage = 3,
        WinningAMap = 4,
    }
    [CreateAssetMenu(fileName = "TaskDataSO", menuName = "ScriptableObject/Config/TaskDataSO")]
    public class TaskDataSO : ScriptableObject
    {
        public TaskId _taskId;
        public string TxtTask;
        public List<InventoryData> InventoryDatas;
        public bool IsCompleted;
        public bool IsGotten;
        
        public virtual bool isCompleted()
        {
            return false;
        }
    }

    public class GatherTaskSO : TaskDataSO
    {
        public int Amount;
        public override bool isCompleted()
        {
            int starCollected = 0; //test
            return starCollected >= Amount;
        }
    }
    
    
}

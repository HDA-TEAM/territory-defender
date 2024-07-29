using System;
using System.Collections.Generic;
using Common.Scripts.Data.DataAsset;
using UnityEngine;
using UnityEngine.Serialization;

namespace Features.Quest.Scripts
{
    public enum TaskType
    {
        Logging = 1,
        Gathering = 2,
        Winning = 3
    }
    [CreateAssetMenu(fileName = "TaskDataSO", menuName = "ScriptableObject/Config/TaskDataSO")]
    public class TaskDataSO : ScriptableObject
    {
        public TaskType _taskType;
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

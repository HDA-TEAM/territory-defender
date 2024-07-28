using System;
using System.Collections.Generic;
using Common.Scripts.Data.DataAsset;
using UnityEngine;
using UnityEngine.Serialization;

namespace Features.Quest.Scripts
{
    public enum TaskId
    {
        Task1 = 1,
        Task2 = 2,
        Task3 = 3,
        Task4 = 4,
    }
    [CreateAssetMenu(fileName = "TaskDataSO", menuName = "ScriptableObject/Config/TaskDataSO")]
    public class TaskDataSO : ScriptableObject
    {
        public TaskId TaskId;
        public string TxtTask;
        public List<InventoryData> InventoryDatas;
        // public DateTime CompletionTime;
        public bool IsCompleted;
    }

    
}

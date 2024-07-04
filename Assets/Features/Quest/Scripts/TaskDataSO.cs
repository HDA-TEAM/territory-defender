using System.Collections.Generic;
using Common.Scripts.Data.DataAsset;
using UnityEngine;

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
        public TaskId _taskId;
        public string _txtTask;
        public List<InventoryData> _inventoryDatas;
    }

    
}

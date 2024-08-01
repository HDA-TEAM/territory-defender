using Features.Quest.Scripts;
using UnityEngine;

namespace Common.Scripts.Data.DataConfig
{
    [CreateAssetMenu(fileName = "TaskDataConfig", menuName = "ScriptableObject/Configs/TaskDataConfig")]
    public class TaskDataConfig : DataConfigBase<TaskId, TaskDataSO>
    {
        
    }
}

using System.Collections.Generic;
using Common.Scripts.Data.DataAsset;
using Features.Quest.Scripts;
using UnityEngine;

namespace Common.Scripts.Data.DataConfig
{
    [CreateAssetMenu(fileName = "QuestDataConfig", menuName = "ScriptableObject/Configs/QuestDataConfig")]
    public class QuestDataConfig : DataConfigBase<QuestType, List<TaskId>>
    {
        
    }
}

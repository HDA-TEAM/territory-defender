using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Features.Quest.Scripts
{
    public struct QuestComposite
    {
        public QuestType Type;
        public List<TaskDataSO> ListTaskData;
    }

    public class QuestDataController : MonoBehaviour
    {
        [Header("Data")] public QuestDataAsset _questDataAsset;
        private List<QuestComposite> _curQuestComposites;
        public List<QuestComposite> QuestComposites
        {
            get
            {
                if (_curQuestComposites.Count > 0)
                    return _curQuestComposites;
                InitQuestData();
                return _curQuestComposites;
            }
        }

        public void InitQuestData()
        {
            // Convert keys to a list for indexing
            var keys = new List<QuestType>(_questDataAsset._questTypeDict.Keys);
            
            for (int i = 0; i < keys.Count; i++)
            {
                //Todo
                QuestType questType = keys[i];
                List<TaskDataSO> taskList = _questDataAsset.GetTaskListByType(questType);
                _curQuestComposites = new List<QuestComposite>
                {
                    new QuestComposite
                    {
                        Type = questType, //Todo
                        ListTaskData = taskList
                    }
                };
            }
        }
    }
}

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Features.Quest.Scripts
{
    [Serializable]
    public struct QuestComposite
    {
        public QuestType Type;
      
    }
    
    public class QuestDataController : MonoBehaviour
    {
        [Header("Data")] public QuestDataAsset _questDataAsset;
        [SerializeField] private List<QuestComposite> _curQuestComposites;
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
                //Todo rrr
                QuestType questType = keys[i];
                List<TaskDataSO> taskList = _questDataAsset.GetTaskListByType(questType);
                _curQuestComposites.Add(new QuestComposite
                    {
                        Type = questType, //Todo
                        
                    }
                );
            }
        }
    }
}

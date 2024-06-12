using System;
using System.Collections.Generic;
using UnityEngine;

namespace Features.Quest.Scripts
{
    public class ListQuestViewModel : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private List<ItemTaskView> _itemTaskViews;

        [Header("Data")] 
        [SerializeField] private QuestDataAsset _questDataAsset;

        private void Start()
        {
            Debug.Log("Runnnnnnnnnnnnnn");
            UpdateData();
        }

        private void UpdateData()
        {
            
            UpdateView();
        }

        private void UpdateView()
        {
            for (int i = 0; i < _itemTaskViews.Count; i++)
            {
                _itemTaskViews[i].Setup(_questDataAsset.GetAllDailyListTask(QuestType.DailyQuest)[i], OnSelectedGet);
            }
        }

        private void OnSelectedGet(ItemTaskView itemTaskView)
        {
            
        }
    }
}

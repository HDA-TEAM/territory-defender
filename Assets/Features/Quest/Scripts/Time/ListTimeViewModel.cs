using System;
using System.Collections.Generic;
using Features.Quest.Scripts.Quest;
using UnityEngine;

namespace Features.Quest.Scripts.Time
{
    public class ListTimeViewModel : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private List<ItemTimeView> _itemQuestTimes;

        [Header("Data")] 
        [SerializeField] private ListQuestViewModel _listQuestViewModel;

        private List<QuestComposite> _questComposites;
        public Action<QuestType> _onUpdateViewAction;
        private ItemTimeView _preItemTimeView;

        //public ItemTimeView GetPreItemTime => _preItemTimeView;
        public void SetupTime()
        {
            UpdateData();
            
            OnSelectedQuestTime(_itemQuestTimes[0]);
        }

        private void UpdateData()
        {
            _questComposites = _listQuestViewModel._questDataController.QuestComposites;
            UpdateView();
        }

        private void UpdateView()
        {
            for (int i = 0; i < _questComposites.Count; i++)
            {
                _itemQuestTimes[i].Initialize(_questComposites[i].Type);
                _itemQuestTimes[i].SetUp(OnSelectedQuestTime, SetQuestText(_questComposites[i]));
            }
        }

        private void OnSelectedQuestTime(ItemTimeView itemTimeView)
        {
            if (_preItemTimeView == itemTimeView) return;
            if (_preItemTimeView != null)
            {
                _preItemTimeView.RemoveSelected();
            }
            
            _preItemTimeView = itemTimeView;
            _preItemTimeView.OnSelectedItemTime();
            
            //Post data
            if (_preItemTimeView == null)
            {
                Debug.LogError("_preItemTimeView is null");
                return;
            }

            _onUpdateViewAction?.Invoke(_preItemTimeView.GetQuestType);
        }
        
        private string SetQuestText(QuestComposite questComposite)
        {
            string textName;
            switch (questComposite.Type)
            {
                case QuestType.DailyQuest:
                    textName = "Daily";
                    break;
                case QuestType.WeeklyQuest:
                    textName = "Weekly";
                    break;
                case QuestType.MonthlyQuest:
                    textName = "Monthly";
                    break;
                default:
                    textName = "Unknown";
                    break;
            }

            return textName;
        }
    }
}

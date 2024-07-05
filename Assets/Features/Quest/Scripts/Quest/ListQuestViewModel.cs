using System.Collections.Generic;
using Features.Common.Scripts;
using Features.Quest.Scripts.Time;
using UnityEngine;

namespace Features.Quest.Scripts.Quest
{
    public class ListQuestViewModel : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private List<ItemTaskView> _itemTaskViews;

        [Header("Data")] 
        [SerializeField] private ListTimeViewModel _listTimeViewModel;
        public QuestDataController _questDataController;

        private QuestType _preQuestType;
        private void SubscribeEvents()
        {
            if (_listTimeViewModel != null)
            {
                //Debug.Log(_listTimeViewModel._preItemTimeView.GetQuestType + "............_listTimeViewModel._preItemTimeView.GetQuestType");
                _listTimeViewModel._onUpdateViewAction += UpdateView;
            }
        }
        
        private void UnSubscribeEvents()
        {
            if (_listTimeViewModel != null)
            {
                _listTimeViewModel._onUpdateViewAction -= UpdateView;
            }
        }

        private void Setup()
        {
        }
        private void Start()
        {
            UpdateData();
            _listTimeViewModel.SetupTime();
            
            UnSubscribeEvents();
            SubscribeEvents();
        }

        private void UpdateData()
        {
            _questDataController.InitQuestData();
            
            //Default setting
            UpdateView(QuestType.DailyQuest);
        }

        private void UpdateView(QuestType questType)
        {
            if (_preQuestType == questType) return;
            
            _preQuestType = questType;
       
            var tasks = GetTasksByTimeType(questType);
            IItemSetupView<TaskDataSO> setupItemTask = new SetupItemTask();
            
            //TODO
            for (int i = 0; i < _itemTaskViews.Count; i++)
            {
                if (i < tasks.Count)
                {
                    _itemTaskViews[i].gameObject.SetActive(true);
                    _itemTaskViews[i].Initialize(setupItemTask, tasks[i], OnSelectedGet);
                }
                else
                {
                    _itemTaskViews[i].gameObject.SetActive(false);
                }
            }
        }

        private List<TaskDataSO> GetTasksByTimeType(QuestType questType)
        {
            return _questDataController._questDataAsset.GetTaskListByType(questType);
        }

        private void OnSelectedGet(ItemViewBase<TaskDataSO> itemTaskView)
        {
            Debug.Log("Get rewards....");
        }
    }
}

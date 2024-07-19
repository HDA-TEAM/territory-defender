using System;
using System.Collections.Generic;
using Common.Scripts.Data.DataAsset;
using Features.Common.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Quest.Scripts.Quest
{
    public class ItemTaskView : ItemViewBase<TaskDataSO>
    {
        public Button _btnGet;
        [SerializeField] private TextMeshProUGUI _txtContent;

        private TaskId _taskId;
        public TaskId GetTaskId => _taskId;
        public List<InventoryData> InventoryGetAfterCompleteTask { get; set; }

        private void Start()
        {
            _btnGet.onClick.AddListener(OnSelectedButton);
        }

        public void Setup(TaskDataSO taskDataSo, Action<ItemTaskView> onAction)
        {
            _taskId = taskDataSo.TaskId;
            OnSelected = (Action<ItemViewBase<TaskDataSO>>)onAction;
            InventoryGetAfterCompleteTask = taskDataSo.InventoryDatas;
            SetName(taskDataSo);
        }

        protected override void SetName(TaskDataSO taskDataSo)
        {
            _txtContent.text = taskDataSo.TxtTask;
        }
        private void GetInventoryItem(InventoryData item)
        {
            // Example processing logic
            Debug.Log("Getting inventory item: " + item.Amount + " " + item.InventoryType);
        }
    }
}

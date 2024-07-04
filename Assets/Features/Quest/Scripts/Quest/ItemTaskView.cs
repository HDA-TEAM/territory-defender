using System;
using Features.Common.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Quest.Scripts.Quest
{
    public class ItemTaskView : ItemViewBase<TaskDataSO>
    {
        [SerializeField] private Button _btnGet;
        [SerializeField] private TextMeshProUGUI _txtContent;

        private void Start()
        {
            _btnGet.onClick.AddListener(OnSelectedButton);
        }

        public void Setup(TaskDataSO taskDataSo, Action<ItemTaskView> onAction)
        {
            OnSelected = (Action<ItemViewBase<TaskDataSO>>)onAction;
            SetName(taskDataSo);
        }

        protected override void SetName(TaskDataSO taskDataSo)
        {
            _txtContent.text = taskDataSo._txtTask;
        }
    }
}

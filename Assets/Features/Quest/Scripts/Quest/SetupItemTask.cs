using System;
using Features.Common.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Quest.Scripts.Quest
{
    public class SetupItemTask : IItemSetupView<TaskDataSO>
    {
        [SerializeField] private Button _ad;

        void Awake()
        {
            _ad.onClick.AddListener(OnClick);
        }

        private Action<bool> _isClick;

        void OnClick()
        {
            _isClick?.Invoke(true);
        }

        void SetUp(Action<bool> isClick)
        {
            _isClick = isClick;
        }
        public void Setup(ItemViewBase<TaskDataSO> itemView, TaskDataSO data, Action<ItemViewBase<TaskDataSO>> onAction)
        {
            if (itemView is ItemTaskView taskView)
            {
                taskView.Setup(data, onAction);
            }
        }
    }
}

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

        [SerializeField] private Image _imageBtn;
        [SerializeField] private Sprite _spriteUncompleted;
        [SerializeField] private Sprite _spriteCompleted;
        [SerializeField] private TextMeshProUGUI _txtBtnName;
        
        public TaskDataSO TaskDataSo;
        public List<InventoryData> InventoryGetAfterCompleteTask { get; private set; }

        private void Start()
        {
            _btnGet.onClick.AddListener(OnSelectedButton);
        }

        public void Setup(TaskDataSO taskDataSo, Action<ItemTaskView> onAction)
        {
            TaskDataSo = taskDataSo;
            OnSelected = (Action<ItemViewBase<TaskDataSO>>)onAction;
            InventoryGetAfterCompleteTask = taskDataSo.InventoryDatas;
            SetName(taskDataSo);
        }

        protected override void SetName(TaskDataSO taskDataSo)
        {
            _txtContent.text = taskDataSo.TxtTask;
        }
        public void SetUnCompleted(string txt)
        {
            _imageBtn.sprite = _spriteUncompleted;
            _txtBtnName.text = txt;
        }

        public void SetCompleted(string txt)
        {
            _imageBtn.sprite = _spriteCompleted;
            _txtBtnName.text = txt;
        }
    }
}

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Quest.Scripts
{
    public class ItemTaskView : MonoBehaviour
    {
        [SerializeField] private Button _btnGet;
        [SerializeField] private TextMeshProUGUI _txtContent;
        
        private Action<ItemTaskView> _onSelected;

        public void Setup(TaskDataSO taskDataSo, Action<ItemTaskView> onAction)
        {
            _onSelected = onAction;
            SetContent(taskDataSo._txtTask);
            _btnGet.onClick.AddListener(OnSelectedGet);
        }

        private void OnSelectedGet()
        {
            _onSelected?.Invoke(this);
        }

        private void SetContent(string content)
        {
            _txtContent.text = content;
        }
    }
}

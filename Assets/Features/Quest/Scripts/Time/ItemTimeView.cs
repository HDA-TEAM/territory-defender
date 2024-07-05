using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Quest.Scripts.Time
{
    public class ItemTimeView : MonoBehaviour
    {
        [SerializeField] private Button _btn;
        [SerializeField] private TextMeshProUGUI _txt;
        [SerializeField] private Sprite _spriteSelectedBg;
        [SerializeField] private Sprite _spriteUnSelectedBg;
        [SerializeField] private Image _imageBg;

        //Internal
        private Action<ItemTimeView> _onSelected;
        private QuestType _questType;
        public QuestType GetQuestType => _questType;
        
        public void Initialize(QuestType questType)
        {
            _questType = questType;
        }
        public void SetUp(Action<ItemTimeView> onAction, string timeType)
        {
            _onSelected = onAction;
            SetName(timeType);
            
            _btn.onClick.AddListener(OnSelectedItemTime);
        }

        public void OnSelectedItemTime()
        {
            _imageBg.sprite = _spriteSelectedBg;
            _onSelected?.Invoke(this);
        }

        public void RemoveSelected()
        {
            _imageBg.sprite = _spriteUnSelectedBg;
        }

        private void SetName(string nameText)
        {
            _txt.text = nameText;
        }
    }
}

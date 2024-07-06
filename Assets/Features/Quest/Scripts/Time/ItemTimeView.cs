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

        public QuestComposite QuestComposite;
        private Action<ItemTimeView> _onSelected;
        
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

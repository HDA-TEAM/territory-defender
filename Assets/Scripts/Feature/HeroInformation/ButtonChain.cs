using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
namespace Feature.HeroInformation
{
    public class ButtonChain : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private ButtonView _characterController;
        [SerializeField] private MenuCharacterModes _menuType;
        private TextMeshProUGUI _text;
        public Button Button() => _button;

        private Action<Button> _onSelected;
        private Action<MenuCharacterModes> _onChangedContent;
        public TextMeshProUGUI Text() => _text;
        private void Awake()
        {
             _text = _button.GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetUp(Action<Button> onSelected)
        {
            _onSelected = onSelected;
        }

        public void ChangeContent(Action<MenuCharacterModes> onChangedContent)
        {
            _onChangedContent = onChangedContent;
        }

        public void OnButtonClick()
        {
            _onSelected?.Invoke(_button);
            _onChangedContent?.Invoke(_menuType);
        }
    }

}

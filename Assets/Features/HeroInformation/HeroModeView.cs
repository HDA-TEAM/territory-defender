using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.HeroInformation
{
    public class HeroModeView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private MenuCharacterModes _menuType;

        private TextMeshProUGUI _text;
        private Action<MenuCharacterModes> _onButtonSelected;

        public MenuCharacterModes MenuType { get; private set; }
        public void Initialize(Action<MenuCharacterModes> onButtonSelected)
        {
            _onButtonSelected = onButtonSelected;
            _text = _button.GetComponentInChildren<TextMeshProUGUI>();
            _button.onClick.AddListener(OnButtonClick);
        }
        
        public void SetMenuType(MenuCharacterModes menuType)
        {
            MenuType = menuType;
        }

        private void OnButtonClick()
        {
            _onButtonSelected?.Invoke(_menuType);
        }

        // Call this method to update the button's visual state
        public void UpdateVisualState(Sprite positiveImage, Color positiveColor)
        {
            _button.image.sprite = positiveImage;
            if (_text != null)
            {
                _text.color = positiveColor;
            }
        }
    }
}
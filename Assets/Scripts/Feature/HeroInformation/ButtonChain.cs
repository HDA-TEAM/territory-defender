using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Feature.HeroInformation
{
    public class ButtonChain : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private ChangeButtonController _characterController;
        
        private TextMeshProUGUI _text;
        public Button Button() => _button;
        
        public TextMeshProUGUI Text() => _text;
        private void Awake()
        {
             _text = _button.GetComponentInChildren<TextMeshProUGUI>();
        }

        public void OnButtonClick()
        {
            _characterController.ChangeButtonImagesAndColorText(_button);
        }
    }

}

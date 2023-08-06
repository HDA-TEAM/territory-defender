using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Feature.HeroInformation
{
    public class ButtonChain : MonoBehaviour
    {
        [SerializeField] private Button button;

        [SerializeField] private ChangeButtonController characterController;
        private TextMeshProUGUI _text;

        public Button Button() => button;
        
        public TextMeshProUGUI Text() => _text;
        private void Awake()
        {
             _text = button.GetComponentInChildren<TextMeshProUGUI>();
        }

        public void OnButtonClick()
        {
            characterController.ChangeButtonImagesAndColorText(button);
        }
    }

}

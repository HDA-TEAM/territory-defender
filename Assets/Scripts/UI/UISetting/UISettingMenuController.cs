using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UISetting
{
    public class UISettingMenuController : MonoBehaviour
    {
        [SerializeField] private Button _buttonSound;
        [SerializeField] private Button _buttonMusic;
        [SerializeField] private Button _buttonLanguage;
        [SerializeField] private Button _buttonCommunity;

        private void Start()
        {
            _buttonSound.onClick.AddListener(TurnOnSettingSound);
            _buttonMusic.onClick.AddListener(TurnOnSettingMusic);
            _buttonLanguage.onClick.AddListener(TurnOnSettingLanguage);
            _buttonCommunity.onClick.AddListener(TurnOnSettingCommunity);
        }

        private void TurnOnSettingSound()
        {
            Debug.Log("Sound is turn on");
        }
        
        private void TurnOnSettingMusic()
        {
            Debug.Log("Music is turn on");
        }
        
        private void TurnOnSettingLanguage()
        {
            Debug.Log("Language is turn on");
        }
        
        private void TurnOnSettingCommunity()
        {
            Debug.Log("Community is turn on");
        }
        
    }
}
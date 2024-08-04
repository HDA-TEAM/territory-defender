using System;
using Common.Scripts;
using SuperMaxim.Messaging;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Home.Scripts.HomeScreen
{
    public class ButtonUIInHomeClickEffect : MonoBehaviour
    {
        [Header("Sounds"), Space(12)] [SerializeField]
        private AudioClip _audioClipChangeUI;

        [SerializeField] private bool _isButtonAudioPlay;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();

            if (_button != null)
            {
                _button.onClick.AddListener(ApplyTool);
            }
            else
            {
                Debug.LogWarning("Button component not found on " + gameObject.name);
            }
        }

        private void ApplyTool()
        {
            if (_isButtonAudioPlay)
            {
                Messenger.Default.Publish(new AudioPlayOneShotPayload
                {
                    AudioClip = _audioClipChangeUI,
                });
            }
        }
    }
}

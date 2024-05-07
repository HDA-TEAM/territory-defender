using Common.Scripts;
using SuperMaxim.Messaging;
using System;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace GamePlay.Scripts.Tower.TowerKIT
{
    public enum ConfirmStatus
    {
        None = 0,
        WaitingConfirm = 1,
    }
    public class ConfirmHandle : MonoBehaviour
    {
        [SerializeField] private Button _button; 
        [SerializeField] private Image _defaultIcon;
        [SerializeField] private Image _acceptedIcon;
        [Header("Sounds"),Space(12)]
        [SerializeField] private AudioClip _audioClipCheck;
        private ConfirmStatus _confirmStatus;
        private Action _onApply;
        private Action _onPreviewChanging;
        private Action<Object> _callbackSelected;

        #region Core
        private void Start() => _button.onClick.AddListener(OnClick);
        public void OnEnable() => ResetToDefault();
        public void SetUp(Action onApply, Action onPreviewChanging)
        {
            _onApply = onApply;
            _onPreviewChanging = onPreviewChanging;
        }
        public void SetUpSelected(Action<Object> callback) => _callbackSelected = callback;
    
        #endregion
    
        private void OnClick()
        {
            _callbackSelected?.Invoke(this);
            switch (_confirmStatus)
            {
                case ConfirmStatus.WaitingConfirm:
                    {
                        OnAccepted();
                        ResetToDefault();
                        return;
                    }
                default:
                    { 
                        OnWaitingConfirm();
                        return;
                    }
            }
        }
        private void OnWaitingConfirm()
        {
            // Show preview
            _onPreviewChanging?.Invoke();
            
            _confirmStatus = ConfirmStatus.WaitingConfirm;
            _acceptedIcon.gameObject.SetActive(true);
            _defaultIcon.gameObject.SetActive(false);
            
            Messenger.Default.Publish(new AudioPlayOneShotPayload
            {
                AudioClip = _audioClipCheck,
            });
        }
        private void OnAccepted() => _onApply?.Invoke();
        public void ResetToDefault()
        {
            _confirmStatus = ConfirmStatus.None;
            _acceptedIcon.gameObject.SetActive(false);
            _defaultIcon.gameObject.SetActive(true);
        }
    }
}
using Common.Scripts;
using DG.Tweening;
using GamePlay.Scripts.Menu.UnitInformationPanel;
using SuperMaxim.Messaging;
using UnityEngine;

namespace GamePlay.Scripts.Menu.InGameStageScreen.UnitInformationPanel
{
    public class UnitInformationPanel : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransformBoard;
        [SerializeField] private RectTransform _startPos;
        [SerializeField] private RectTransform _endPos;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _showHidePanelDuration = 0.3f;
        [Header("Sounds"), Space(12)]
        [SerializeField] private AudioClip _audioClipSelection;
    
        [SerializeField] private UnitShowInformationViewModel _unitShowInformationViewModel;
    
        private bool _isShowing = false;

        private void Awake()
        {
            Messenger.Default.Subscribe<ShowUnitInformationPayload>(ShowPanelInformation);
            Messenger.Default.Subscribe<HideUnitInformationPayload>(HidePanelInformation);
        }
        private void OnDestroy()
        {
            Messenger.Default.Unsubscribe<ShowUnitInformationPayload>(ShowPanelInformation);
            Messenger.Default.Unsubscribe<HideUnitInformationPayload>(HidePanelInformation);
        }

        private void ShowPanelInformation(ShowUnitInformationPayload payload)
        {
            Messenger.Default.Publish(new AudioPlayOneShotPayload
            {
                AudioClip = _audioClipSelection,
            });
        
            _canvasGroup.alpha = 1;
            _rectTransformBoard.DOAnchorPosY(_endPos.anchoredPosition.y, _showHidePanelDuration);
            if (_isShowing)
                _isShowing = false;
            _unitShowInformationViewModel.Setup(payload.UnitBase.UnitStatsHandlerComp().GetShowStatsInformation());
            _isShowing = true;
        }
        private void HidePanelInformation(HideUnitInformationPayload payload)
        {
            if (_isShowing)
            {
                _isShowing = false;
                _rectTransformBoard.DOAnchorPosY(_startPos.anchoredPosition.y, _showHidePanelDuration);
            }
            _isShowing = false;
        }
    }

    public struct ShowUnitInformationPayload
    {
        public UnitBase UnitBase;
    }

    public struct HideUnitInformationPayload
    {
    }
}
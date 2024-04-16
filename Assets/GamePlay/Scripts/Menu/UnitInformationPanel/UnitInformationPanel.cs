using DG.Tweening;
using GamePlay.Scripts.Menu.UnitInformationPanel;
using SuperMaxim.Messaging;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitInformationPanel : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransformBoard;
    [SerializeField] private RectTransform _startPos;
    [SerializeField] private RectTransform _endPos;
    
    [SerializeField] private Image _avatar;
    [SerializeField] private TextMeshProUGUI _txtName;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private UnitBase _curUnitBaseInfo;
    [SerializeField] private float _showHidePanelDuration = 0.3f;
    [SerializeField] private ItemUnitStatListViewModel _unitStatListViewModel;
    
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
        _canvasGroup.alpha = 1;
        _rectTransformBoard.DOAnchorPosY(_endPos.anchoredPosition.y, _showHidePanelDuration);
        if (_isShowing)
            _isShowing = false;
        _unitStatListViewModel.SetupStats(payload.UnitBase.UnitStatsHandlerComp().GetShowStatsInformation());
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

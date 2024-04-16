using DG.Tweening;
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
        _txtName.text = payload.StatsData.GetInformation(InformationId.Name);
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
    public Stats StatsData;
    public UnitBase UnitBase;
}

public struct HideUnitInformationPayload
{
}

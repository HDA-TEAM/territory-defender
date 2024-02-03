using SuperMaxim.Messaging;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitInformationPanel: SingletonBase<UnitInformationPanel>
{
    [SerializeField] private Image _avatar;
    [SerializeField] private TextMeshProUGUI _txtName;
    // [SerializeField] private AnimationSequencerController _openAnim;
    // [SerializeField] private AnimationSequencerController _closeAnim;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private UnitBase _curUnitBaseInfo;
    private bool _isShowing = false;
    
    public void Awake()
    {
        Messenger.Default.Subscribe<ShowUnitInformationPayload>(ShowPanelInformation);
        Messenger.Default.Subscribe<HideUnitInformationPayload>(HidePanelInformation);
    }
    public void OnDestroy()
    {
        Messenger.Default.Unsubscribe<ShowUnitInformationPayload>(ShowPanelInformation);
        Messenger.Default.Unsubscribe<HideUnitInformationPayload>(HidePanelInformation);
    }

    private void ShowPanelInformation(ShowUnitInformationPayload payload)
    {
        _canvasGroup.alpha = 1;
        // if (!_isShowing)
        //     _openAnim.Play();
        _txtName.text = payload.StatsData.GetInformation(InformationId.Name);
        _isShowing = true;
    }
    private void HidePanelInformation(HideUnitInformationPayload payload)
    {
        if (_isShowing)
        {
            _isShowing = false;
                // _closeAnim.Play(() =>
                // {
                //     _canvasGroup.alpha = 0;
                // });
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
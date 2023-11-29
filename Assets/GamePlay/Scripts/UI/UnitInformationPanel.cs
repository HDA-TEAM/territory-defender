using BrunoMikoski.AnimationSequencer;
using Cysharp.Threading.Tasks;
using SuperMaxim.Messaging;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitInformationPanel: MonoBehaviour
{
    [SerializeField] private Image _avatar;
    [SerializeField] private TextMeshProUGUI _txtName;
    [SerializeField] private AnimationSequencerController _openAnim;
    [SerializeField] private AnimationSequencerController _closeAnim;
    [SerializeField] private CanvasGroup _canvasGroup;
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
        if (!_isShowing)
            _openAnim.Play();
        _txtName.text = payload.StatsData.GetInformation(InformationId.Name);
        _isShowing = true;
    }
    private async void HidePanelInformation(HideUnitInformationPayload payload)
    {
        if (_isShowing)
        {
            _isShowing = false;
            await _closeAnim.PlayAsync();
            _canvasGroup.alpha = 0f;
        }
        _isShowing = false;
    }
}
public struct ShowUnitInformationPayload
{
    public Stats StatsData;
}
public struct HideUnitInformationPayload
{
}
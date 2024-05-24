using GamePlay.Scripts.Menu.InGameStageScreen.UnitInformationPanel;
using SuperMaxim.Messaging;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HandleCancelRaycast : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private CanvasGroup _canvasGroup;

    private Action _callback;
    private void Awake()
    {
        _button.onClick.AddListener(OnClose);
        Messenger.Default.Subscribe<HandleCancelRaycastPayload>(OnHandleCancelRaycast);
    }
    private void OnHandleCancelRaycast(HandleCancelRaycastPayload payload)
    {
        _callback = payload.callback;
        _canvasGroup.blocksRaycasts = payload.IsOn;
        
        if (payload.UnitSelectionShowType == EUnitSelectionShowType.OnlyBlockRaycast)
        {
            // Hiding Information panel
            Messenger.Default.Publish(new HideUnitInformationPayload());
        }
        else if (payload.UnitSelectionShowType == EUnitSelectionShowType.HidingAll)
        {
            OnClose();
        }
    }
    private void OnDestroy() => Messenger.Default.Unsubscribe<HandleCancelRaycastPayload>(OnHandleCancelRaycast);
    private void OnClose()
    {
        _canvasGroup.blocksRaycasts = false;
        _callback?.Invoke();
        
        // Hiding Information panel
        Messenger.Default.Publish(new HideUnitInformationPayload());
    }
}

public enum EUnitSelectionToken
{
    None = 0,
    NeedToTriggerAction = 1,
    CanBeOverride = 2,
}
public enum EUnitSelectionShowType
{
    None = 0,
    OnlyShowInformationPanel = 1,
    OnlyBlockRaycast = 2,
    ShowInformationPanelAndBlockRaycast = 3,
    HidingAll = 4,
}
public struct HandleCancelRaycastPayload
{
    public EUnitSelectionToken UnitSelectionToken;
    public EUnitSelectionShowType UnitSelectionShowType;
    public bool IsOn;
    public Action callback;
}

using SuperMaxim.Messaging;
using System;
using System.Collections;
using System.Collections.Generic;
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
    }
    private void OnDestroy() => Messenger.Default.Unsubscribe<HandleCancelRaycastPayload>(OnHandleCancelRaycast);
    private void OnClose()
    {
        _canvasGroup.blocksRaycasts = false;
        _callback?.Invoke();
    }
    
}

public struct HandleCancelRaycastPayload
{
    public bool IsOn;
    public Action callback;
}

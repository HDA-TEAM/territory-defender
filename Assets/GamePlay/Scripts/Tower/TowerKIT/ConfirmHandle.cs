using System;
using UnityEngine;
using UnityEngine.UI;

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
    private ConfirmStatus _confirmStatus;
    private Action _callbackToolAction;
    private Action<ConfirmHandle> _callbackSelected;

    #region Core
    private void Start() => _button.onClick.AddListener(OnClick);
    public void OnEnable() => ResetToDefault();
    public void SetUpTool(Action callback) => _callbackToolAction = callback;
    public void SetUpSelected(Action<ConfirmHandle> callback) => _callbackSelected = callback;
    
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
        _confirmStatus = ConfirmStatus.WaitingConfirm;
        _acceptedIcon.gameObject.SetActive(true);
        _defaultIcon.gameObject.SetActive(false);
        
    }
    private void OnAccepted() => _callbackToolAction?.Invoke();
    public void ResetToDefault()
    {
        _confirmStatus = ConfirmStatus.None;
        _acceptedIcon.gameObject.SetActive(false);
        _defaultIcon.gameObject.SetActive(true);
    }
}

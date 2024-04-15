using System;
using UnityEngine;
using UnityEngine.UI;

public class UserActionHandle : SingletonBase<UserActionHandle>
{
    [SerializeField] private Button _btn;
    private Action _onComplete;
    protected override void Awake()
    {
        base.Awake();
        _btn.onClick.AddListener(OnClick);
    }
    private void OnExecuteAction()
    {
        
    }
    private void OnClick()
    {
        Debug.Log("UserActionHandle OnClick ");
        _onComplete?.Invoke();
        _onComplete = null;
    }
    public void OnCompleteAction(Action onComplete)
    {
        _onComplete = onComplete;
    }
}

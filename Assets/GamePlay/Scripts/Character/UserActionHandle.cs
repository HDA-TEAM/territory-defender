using System;
using UnityEngine;
using UnityEngine.UI;

public class UserActionHandle : SingletonBase<UserActionHandle>
{
    [SerializeField] private Button _btn;
    private Action _onComplete;
    private void Awake()
    {
        _btn.onClick.AddListener(OnClick);
    }
    private void OnExecuteAction()
    {
        
    }
    private void OnClick()
    {
        _onComplete?.Invoke();
    }
    public void OnCompleteAction(Action onComplete)
    {
        _onComplete = onComplete;
    }
}

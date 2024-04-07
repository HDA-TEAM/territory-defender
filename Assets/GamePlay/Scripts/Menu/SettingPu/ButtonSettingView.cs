using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSettingView : MonoBehaviour
{
    [SerializeField] private Button _btn;
    [SerializeField] private Image _imgTurnOff;
    private Action _onClick;
    private void Awake()
    {
        _btn.onClick.AddListener(OnClickButton);
    }
    private void OnClickButton()
    {
        _onClick?.Invoke();
    }
    public void Setup(Action onClick)
    {
        _onClick = onClick;
    }
    public void SetStatus(bool isTurnOn)
    {
        _imgTurnOff.gameObject.SetActive(!isTurnOn);
    }
}

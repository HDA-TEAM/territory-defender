using System;
using UnityEngine;
using UnityEngine.UI;

public class InGameActiveSkill : MonoBehaviour
{
    [SerializeField] private Button _btnUsingKill;
    private Action _onClick;
    private void Awake()
    {
        _btnUsingKill.onClick.AddListener(OnClickUsingSkill);
    }
    public void SetUpSkill(Action onUsingSkill)
    {
        _onClick = onUsingSkill;
    }
    private void OnClickUsingSkill()
    {
        _onClick?.Invoke();
    }
}

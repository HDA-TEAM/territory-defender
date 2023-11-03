using System;
using UnityEngine;
using UnityEngine.UI;

public class InGameStageScreen : MonoBehaviour
{
    [SerializeField] private Button _btnSetting;
    [SerializeField] private GameObject _puSetting;

    private void Awake()
    {
        _btnSetting.onClick.AddListener(() => _puSetting.SetActive(true));
    }
}


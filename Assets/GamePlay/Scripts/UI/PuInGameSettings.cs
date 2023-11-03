using System;
using UnityEngine;
using UnityEngine.UI;

public class PuInGameSettings : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button _btnSound;
    [SerializeField] private Button _btnMusic;
    [SerializeField] private Button _btnReplay;
    [SerializeField] private Button _btnQuit;
    [SerializeField] private Button _btnClose;
    [SerializeField] private CanvasGroup _canvasGroup;
    [Header("Data"), Space(12)] private SettingDataAsset _settingDataAsset;

    private void OnEnable() => Time.timeScale = 0f;
    private void OnDisable() => Time.timeScale = 1f;
    private void Awake()
    {
        _btnClose.onClick.AddListener(OnClose);
    }
    private void OnClose() => this.gameObject.SetActive(false);
}

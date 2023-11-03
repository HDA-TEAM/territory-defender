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
    [SerializeField] private Button[] _btnCloses;
    [SerializeField] private CanvasGroup _canvasGroup;
    [Header("Data"), Space(12)] [SerializeField] private SettingDataAsset _settingDataAsset;

    private void OnEnable() => _settingDataAsset.TimeScaleSetting = ETimeScaleType.Pause;
    private void OnDisable() => _settingDataAsset.TimeScaleSetting = _settingDataAsset.PreTimeScaleSetting();
    private void Awake()
    {
        foreach (var btnClose in _btnCloses)
        {
            btnClose.onClick.AddListener(OnClose);
        }
    }
    private void OnClose() => this.gameObject.SetActive(false);
}

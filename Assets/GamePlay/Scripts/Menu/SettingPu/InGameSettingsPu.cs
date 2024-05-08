using Common.Loading.Scripts;
using Common.Scripts.Navigator;
using GamePlay.Scripts.Data;
using UnityEngine;
using UnityEngine.UI;

public class InGameSettingsPu : CommonModal
{
    [Header("UI")]
    [SerializeField] private ButtonSettingView _btnSound;
    [SerializeField] private ButtonSettingView _btnMusic;
    [SerializeField] private Button _btnQuit;
    
    [Header("Data"), Space(12)] 
    [SerializeField] private SettingDataAsset _settingDataAsset;

    private void OnEnable()
    {
        _settingDataAsset.TimeScaleSetting = ETimeScaleType.Pause;
        SetupView();
    }
    private void OnDisable() => _settingDataAsset.TimeScaleSetting = _settingDataAsset.PreTimeScaleSetting();
    protected override void Awake()
    {
        base.Awake();
        
        _btnQuit.onClick.AddListener(OnClickQuit);
        
        _btnSound.Setup(OnClickSound);
        _btnMusic.Setup(OnClickMusic);
    }

    private void SetupView()
    {
        _btnSound.SetStatus(_settingDataAsset.IsSoundOn);
        _btnMusic.SetStatus(_settingDataAsset.IsMusicOn);
    }
    private void OnClickSound()
    {
        bool isTurnOnNew = !_settingDataAsset.IsSoundOn;
        _settingDataAsset.IsSoundOn = isTurnOnNew;
        _btnSound.SetStatus(isTurnOnNew);   
    }
    private void OnClickMusic()
    {
        bool isTurnOnNew = !_settingDataAsset.IsMusicOn;
        _settingDataAsset.IsMusicOn = isTurnOnNew;
        _btnMusic.SetStatus(isTurnOnNew);   
    }
    private void OnClickQuit()
    {
        // Reset gameplay and Load scene home
        LoadingSceneController.Instance.LoadingGameToHome();
    }
}

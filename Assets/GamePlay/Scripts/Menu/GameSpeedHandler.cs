using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeedHandler : MonoBehaviour
{
    [SerializeField] private SettingDataAsset _settingDataAsset;
    [SerializeField] private TextMeshProUGUI _txtSpeed;
    [SerializeField] private Button _btnSpeed;
    private void Awake()
    {
        _btnSpeed.onClick.AddListener(OnChangeSpeed);
        _settingDataAsset.TimeScaleSetting = ETimeScaleType.Normal;
        
        _txtSpeed.text = $"x{_settingDataAsset.ConvertTimeScaleValue(_settingDataAsset.TimeScaleSetting)}";
    }
    private void OnChangeSpeed()
    {
        var nextTimeScale = _settingDataAsset.GetNextTimeScaleValue();
        _settingDataAsset.TimeScaleSetting = nextTimeScale;
        _txtSpeed.text = $"x{_settingDataAsset.ConvertTimeScaleValue(nextTimeScale)}";
    }
}

using UnityEngine;

public enum ETimeScaleType
{
    Normal,
    Fast,
    VeryFast,
    Pause,
}

[CreateAssetMenu(fileName = "SettingDataAsset", menuName = "ScriptableObject/DataAsset/SettingDataAsset")]
public class SettingDataAsset : ScriptableObject
{
    [SerializeField] private bool _isMusicOn;
    [SerializeField] private bool _isSoundOn;
    [SerializeField] private ETimeScaleType _timeScale;
    [SerializeField] private ETimeScaleType _preTimeScale;

    public ETimeScaleType TimeScaleSetting
    {
        set
        {
            _preTimeScale = _timeScale;
            _timeScale = value;
            Time.timeScale = ConvertTimeScaleValue(_timeScale);
        }
        get
        {
            return _timeScale;
        }
    }
    public ETimeScaleType PreTimeScaleSetting() => _preTimeScale;
    private float ConvertTimeScaleValue(ETimeScaleType timeScaleType)
    {
        switch (timeScaleType)
        {
            case ETimeScaleType.Pause: return 0f;
            case ETimeScaleType.Fast: return 1.5f;
            case ETimeScaleType.VeryFast: return 2f;
            default: return 1f;
        }
    }
}

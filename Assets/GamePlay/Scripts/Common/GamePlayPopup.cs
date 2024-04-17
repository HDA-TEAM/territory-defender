using GamePlay.Scripts.Data;
using UnityEngine;

public class GamePlayPopup : MonoBehaviour
{
    [Header("Data"), Space(12)] [SerializeField]
    protected SettingDataAsset _settingDataAsset;
    protected virtual void OnEnable() => _settingDataAsset.TimeScaleSetting = ETimeScaleType.Pause;
    protected virtual void OnDisable() => _settingDataAsset.TimeScaleSetting = _settingDataAsset.PreTimeScaleSetting();
}

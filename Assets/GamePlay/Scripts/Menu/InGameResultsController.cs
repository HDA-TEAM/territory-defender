using Common.Loading.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameResultsController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button _btnReplay;
    [SerializeField] private Button _btnQuit;
    [SerializeField] private CanvasGroup _canvasGroup;
    [Header("Data"), Space(12)] [SerializeField]
    private SettingDataAsset _settingDataAsset;

    private void OnEnable() => _settingDataAsset.TimeScaleSetting = ETimeScaleType.Pause;
    private void OnDisable() => _settingDataAsset.TimeScaleSetting = _settingDataAsset.PreTimeScaleSetting();
    private void Awake()
    {
        _btnQuit.onClick.AddListener(OnClickQuit);
    }

    private void OnClickQuit()
    {
        // Load scene home
        LoadingSceneController.Instance.LoadingGameToHome();
    }
}

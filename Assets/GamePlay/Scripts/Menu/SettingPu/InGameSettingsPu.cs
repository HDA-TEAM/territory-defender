using Common.Loading.Scripts;
using Common.Scripts;
using GamePlay.Scripts.Data;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Scripts.Menu.SettingPu
{
    public class InGameSettingsPu : CommonSettingPu
    {
        [Header("UI")]
        [SerializeField] private Button _btnQuit;
   
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
        }
        private void OnClickQuit()
        {
            // Reset gameplay and Load scene home
            LoadingSceneController.Instance.LoadingGameToHome();
        }
    }
}

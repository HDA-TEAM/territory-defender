using Common.Scripts.Navigator;
using GamePlay.Scripts.Data;
using UnityEngine;

namespace Common.Scripts
{
    public class CommonSettingPu : CommonModal
    {
        [Header("UI")]
        [SerializeField] private ButtonSettingView _btnSound;
        [SerializeField] private ButtonSettingView _btnMusic;
    
        [Header("Data"), Space(12)] 
        [SerializeField] protected SettingDataAsset _settingDataAsset;

        protected override void Awake()
        {
            base.Awake();
        
            _btnSound.Setup(OnClickSound);
            _btnMusic.Setup(OnClickMusic);
        }
        protected virtual void OnEnable()
        {
            SetupView();
        }

        protected void SetupView()
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
    }
}

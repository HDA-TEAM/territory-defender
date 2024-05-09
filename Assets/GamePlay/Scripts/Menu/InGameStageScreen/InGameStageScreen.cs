using Common.Scripts.Navigator;
using UnityEngine;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Page;

namespace GamePlay.Scripts.Menu.InGameStageScreen
{
    public class InGameStageScreen : Page
    {
        [SerializeField] private Button _btnSetting;

        private void Awake()
        {
            _btnSetting.onClick.AddListener(OnClickSetting);
        }
        private void OnClickSetting()
        {
            NavigatorController.MainModalContainer.Push<InGameSettingsPu>(ResourceKey.InGame.InGameSettingsPu, playAnimation: true);
        }
    }
}

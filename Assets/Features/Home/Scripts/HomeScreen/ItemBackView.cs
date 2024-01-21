using BrunoMikoski.UIManager;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UIInHomeScreen
{
    public class ItemBackView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        
        [SerializeField] private ListTowerViewModel _listTowerViewModel;
        [SerializeField] private ListHeroViewModel _listHeroViewModel;
        private void Start()
        {
            _button.onClick.AddListener(SceneBackLoad);
        }

        private void SceneBackLoad()
        {
            GlobalUtility.ResetView(_listTowerViewModel, _listHeroViewModel);
            Debug.Log("Back home....");
            
            if (UiWindowCollectionStatic.MasteryPagePopup.IsPopup)
            {
                UiWindowCollectionStatic.MasteryPagePopup.Close();
            }

            if (UiWindowCollectionStatic.QuestPopup.IsPopup)
            {
                UiWindowCollectionStatic.QuestPopup.Close();
            }

            if (UiWindowCollectionStatic.SettingPopup.IsPopup)
            {
                UiWindowCollectionStatic.SettingPopup.Close();
            }

            //TODO
            UiWindowCollectionStatic.HomeMenuScreen.Open();
        }
    }
}


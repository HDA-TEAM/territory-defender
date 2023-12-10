using BrunoMikoski.UIManager;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UIInHomeScreen
{
    public class HomeMenuViewModel: MonoBehaviour
    {
        [SerializeField] private Button _buttonShop;
        [SerializeField] private Button _buttonHeroInfo;
        [SerializeField] private Button _buttonDictionary;
        [SerializeField] private Button _buttonHistory;
        [SerializeField] private Button _buttonUpgradeTower;
        [SerializeField] private Button _buttonSetting;
        [SerializeField] private Button _buttonQuest;
        
        private void Start()
        {
            _buttonHeroInfo.onClick.AddListener(HeroInformationLoad);
            _buttonShop.onClick.AddListener(ShopLoad);
            _buttonDictionary.onClick.AddListener(DictionaryLoad);
            _buttonHistory.onClick.AddListener(HistoryLoad);
            _buttonUpgradeTower.onClick.AddListener(UpgradeTowerLoad);
            _buttonSetting.onClick.AddListener(SettingLoad);
            _buttonQuest.onClick.AddListener(QuestLoad);
        }
        
        private void HeroInformationLoad()
        {
            UiWindowCollectionStatic.HeroesScreen.Open();
            Debug.Log("Hero info is open");
        }
        
        private void ShopLoad()
        {
            Debug.Log("Shop is open");
        }
        
        private void DictionaryLoad()
        {
            UiWindowCollectionStatic.DictionaryScreen.Open();
            Debug.Log("Dictionary is open");
        }
        
        private void HistoryLoad()
        {
            UiWindowCollectionStatic.HistoryScreen.Open();
            Debug.Log("History is open");
        }

        private void UpgradeTowerLoad()
        { 
            UiWindowCollectionStatic.MasteryPagePopup.Open();
            Debug.Log("Upgrade tower is open");
        }

        private void SettingLoad()
        {
            UiWindowCollectionStatic.SettingPopup.Open();
            Debug.Log("Setting is open");
        }

        private void QuestLoad()
        {
            UiWindowCollectionStatic.QuestPopup.Open();
            Debug.Log("Quest is open");
        }
    }
}
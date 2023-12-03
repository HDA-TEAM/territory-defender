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

        private void Start()
        {
            _buttonHeroInfo.onClick.AddListener(HeroInformationLoad);
            _buttonShop.onClick.AddListener(ShopLoad);
            _buttonDictionary.onClick.AddListener(DictionaryLoad);
            _buttonHistory.onClick.AddListener(HistoryLoad);
            _buttonUpgradeTower.onClick.AddListener(UpgradeTowerLoad);
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
            UiWindowCollectionStatic.UpgradeTowerPopup.Open();
            Debug.Log("Upgrade tower is open");
        }
    }
}
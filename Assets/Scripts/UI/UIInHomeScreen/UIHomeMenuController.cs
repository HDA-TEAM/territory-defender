using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UIInHomeScreen
{
    public class UIHomeMenuController: MonoBehaviour
    {
        [SerializeField] private Button _buttonShop;
        [SerializeField] private Button _buttonHeroInfor;
        [SerializeField] private Button _buttonDictionary;
        [SerializeField] private Button _buttonHistory;

        private void Start()
        {
            _buttonHeroInfor.onClick.AddListener(HeroInformationLoad);
            _buttonShop.onClick.AddListener(ShopLoad);
            _buttonDictionary.onClick.AddListener(DictionaryLoad);
            _buttonHistory.onClick.AddListener(HistoryLoad);
        }
        
        private void HeroInformationLoad()
        {
            Debug.Log("Hero info is open");
            // _hero = gameObject.AddComponent<HeroController>();
            //_hero.Load();
        }
        
        private void ShopLoad()
        {
            Debug.Log("Shop is open");
            //_shop = gameObject.AddComponent<ShopController>();
            //_shop.Load();
        }
        
        private void DictionaryLoad()
        {
            Debug.Log("Dictionary is open");
        }
        
        private void HistoryLoad()
        {
            Debug.Log("History is open");
        }
    }
}
using BrunoMikoski.UIManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.UIInHomeScreen
{
    public class StageController : MonoBehaviour
    {
        [SerializeField] private int _scene;
        public void Load(){
            GlobalValue.Instance._nextScene = _scene;
            SceneManager.LoadScene(4);
        }
    }
    
    public class ShopController : MonoBehaviour
    {
        [SerializeField] private int _scene;

        public void Load()
        {
            GlobalValue.Instance._nextScene = _scene;
            SceneManager.LoadScene(6);
        }
    }

    public class HeroController : MonoBehaviour
    {
        [SerializeField] private int _scene;

        public void Load()
        {
            GlobalValue.Instance._nextScene = _scene;
            SceneManager.LoadScene(7);
        }
    }

    public class TalentUpgradeController : MonoBehaviour
    {
        [SerializeField] private GameObject _talentUpgrade;

        public void Load(GameObject upgradePicture)
        {
            _talentUpgrade = upgradePicture;
            _talentUpgrade.gameObject.SetActive(true);
        }
    }

    public class QuestController : MonoBehaviour
    {
        
    }
    
    public class DictionaryController : MonoBehaviour
    {
        
    }
    
    public class HistoryController : MonoBehaviour
    {
        
    }
    
    public class SettingController : MonoBehaviour
    {
        
    }
    
    public class GoldenCoinController : MonoBehaviour
    {
        
    }
    
    public class SliverCoinController : MonoBehaviour
    {
        
    }

    public class SceneBackController : MonoBehaviour
    {
        public void BackFromHeroToHome()
        {
            UiWindowCollectionStatic.HomeMenuScreen.Open();
        }
    }

}  




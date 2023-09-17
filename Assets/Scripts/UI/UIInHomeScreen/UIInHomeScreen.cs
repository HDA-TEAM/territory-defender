using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.UIInHomeScreen
{
    public class StageController : MonoBehaviour
    {
        [SerializeField] private int scene;
        public void Load(){
            GlobalValue.Instance.nextScene = scene;
            SceneManager.LoadScene(4);
        }
    }
    
    public class ShopController : MonoBehaviour
    {
        [SerializeField] private int scene;

        public void Load()
        {
            GlobalValue.Instance.nextScene = scene;
            SceneManager.LoadScene(6);
        }
    }

    public class HeroController : MonoBehaviour
    {
        [SerializeField] private int scene;

        public void Load()
        {
            GlobalValue.Instance.nextScene = scene;
            SceneManager.LoadScene(7);
        }
    }

    public class TalentUpgradeController : MonoBehaviour
    {
        [SerializeField] private GameObject talentUpgrade;

        public void Load(GameObject upgradePicture)
        {
            talentUpgrade = upgradePicture;
            talentUpgrade.gameObject.SetActive(true);
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
        [SerializeField] private int previousSceneIndex;
        public void BackFromHeroToHome()
        {
            // Load the previous scene in the build settings
            previousSceneIndex = SceneManager.GetActiveScene().buildIndex - 2;
            SceneManager.LoadScene(previousSceneIndex);
        }
    }

}  




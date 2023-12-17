using BrunoMikoski.UIManager;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UIInHomeScreen
{
    public class ItemBackView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private void Start()
        {
            _button.onClick.AddListener(SceneBackLoad);
        }
        private void SceneBackLoad()
        {
            Debug.Log("Back home....");
            UiWindowCollectionStatic.HomeMenuScreen.Open();
        }
    }
}


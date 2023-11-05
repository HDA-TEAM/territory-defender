using BrunoMikoski.UIManager;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UIInHomeScreen
{
    public class UISceneBack : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private void Start()
        {
            _button.onClick.AddListener(SceneBackLoad);
        }
        private void SceneBackLoad()
        {
            UiWindowCollectionStatic.HomeMenuScreen.Open();
        }
    }
}


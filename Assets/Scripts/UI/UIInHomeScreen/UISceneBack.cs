using UnityEngine;
using UnityEngine.UI;

namespace UI.UIInHomeScreen
{
    public class UISceneBack : MonoBehaviour
    {
        private SceneBackController _sceneBack;
        [SerializeField] private Button _button;
        private void Start()
        {
            _sceneBack = gameObject.AddComponent<SceneBackController>();
            _button.onClick.AddListener(SceneBackLoad);
        }
        private void SceneBackLoad()
        {
            _sceneBack.BackFromHeroToHome();
        }
    }
}


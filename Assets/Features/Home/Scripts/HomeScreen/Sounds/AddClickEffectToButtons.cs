#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Features.Home.Scripts.HomeScreen.Sounds
{
    public class AddClickEffectToButtons : EditorWindow
    {
        private string _prefabPath;
        
        [MenuItem("Tools/Add ClickEffect to Buttons")]
        public static void ShowWindow()
        {
            GetWindow<AddClickEffectToButtons>("Add ClickEffect to Buttons");
        }

        private void OnGUI()
        {
            GUILayout.Label("Prefab Path", EditorStyles.boldLabel);
            _prefabPath = GUILayout.TextField(_prefabPath);
            if (GUILayout.Button("Add ClickEffect to All Buttons"))
            {
                AddClickEffect();
            }
        }

        private void AddClickEffect()
        {
            // Tìm tất cả các Button trong scene
            GameObject prefab =  Resources.Load<GameObject>(_prefabPath);
            
            Debug.Log(prefab);

            Button[] buttons = prefab.GetComponentsInChildren<Button>();
            
            foreach (Button button in buttons)
            {
                // Kiểm tra và xóa tất cả các instance của ButtonUIInHomeClickEffect
                ButtonUIInHomeClickEffect[] existingEffects = button.gameObject.GetComponents<ButtonUIInHomeClickEffect>();
                foreach (var effect in existingEffects)
                {
                    DestroyImmediate(effect, true);
                }

                // Thêm mới ClickEffect
                button.gameObject.AddComponent<ButtonUIInHomeClickEffect>();
                EditorUtility.SetDirty(button.gameObject);
            }
        
            // Lưu lại sự thay đổi trong scene
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
            Debug.Log("Added ClickEffect to " + buttons.Length + " buttons.");
        }
    }
}
#endif

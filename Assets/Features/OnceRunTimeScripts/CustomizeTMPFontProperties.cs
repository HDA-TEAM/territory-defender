#if UNITY_EDITOR
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Features.OnceRunTimeScripts
{
    public class CustomizeTMPFontProperties : EditorWindow
    {
        private TMP_FontAsset newFontAsset;

        [MenuItem("Tools/Customize TMP Font Properties")]
        public static void ShowWindow()
        {
            GetWindow<CustomizeTMPFontProperties>("Customize TMP Font Properties");
        }


        void OnGUI()
        {
            GUILayout.Label("Customize Font Properties", EditorStyles.boldLabel);
        
            newFontAsset = EditorGUILayout.ObjectField("Font Asset", newFontAsset, typeof(TMP_FontAsset), false) as TMP_FontAsset;
            if (GUILayout.Button("Apply Changes"))
            {
                ApplyChangesToAllTMP();
            }
        }

        private void ApplyChangesToAllTMP()
        {
            TextMeshProUGUI[] texts = Resources.FindObjectsOfTypeAll<TextMeshProUGUI>();
            foreach (TextMeshProUGUI text in texts)
            {
                Undo.RecordObject(text, "Customize TMP Font Properties");
                if (newFontAsset != null) text.font = newFontAsset;
                EditorUtility.SetDirty(text);
            }
            Debug.Log("Customized properties applied to all TMP texts.");
        }
    }
}
#endif

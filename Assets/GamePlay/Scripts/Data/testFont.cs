#if UNITY_EDITOR
using Common.Scripts;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace _ZZ
{
    public class testFont : SingletonBase<testFont>
    {
        [SerializeField] private TMP_FontAsset newFontAsset;

        private void FixedUpdate()
        {
            ApplyChangesToAllTMP();
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
            //Debug.Log("Customized properties applied to all TMP texts.");
        }
    }
}
#endif

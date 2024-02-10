using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public static class SceneUtility
{
    public static void OpenSceneByIndex(int index)
    {
        if (index >= 0 && index < EditorBuildSettings.scenes.Length)
        {
            var scene = EditorBuildSettings.scenes[index];
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.OpenScene(scene.path);
            }
        }
        else
        {
            Debug.LogError("Scene index out of range.");
        }
    }
}

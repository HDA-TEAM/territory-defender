using System;
using UnityEngine;
using UI.UIInHomeScreen;

public class UISceneBack : MonoBehaviour
{
    private SceneBackController _sceneBack;
    private void Start()
    {
        _sceneBack = gameObject.AddComponent<SceneBackController>();
    }

    public void SceneBack()
    {
        _sceneBack.BackFromHeroToHome();
    }
}

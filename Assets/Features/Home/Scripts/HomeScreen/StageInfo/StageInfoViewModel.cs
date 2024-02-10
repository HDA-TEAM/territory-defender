using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageInfoViewModel : MonoBehaviour
{
    [Header("UI")] [SerializeField] private ItemPlayView _itemPlayView;

    // Internal
    
    private void Awake()
    {
        _itemPlayView.Setup(OnSelectedItemPlay);
        UpdateData();
    }

    private void UpdateData()
    {
        UpdateView();
    }

    private void UpdateView()
    {
        
    }

    private void OnSelectedItemPlay(ItemPlayView itemPlayView)
    {
        LoadSceneByIndex(2); //TODO
    }
    
    // This method is safe to use at runtime
    void LoadSceneByIndex(int sceneIndex)
    {
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogError("Scene index out of range: " + sceneIndex);
        }
    }
}


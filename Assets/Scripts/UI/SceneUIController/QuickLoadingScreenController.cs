using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class QuickLoadingScreenController : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    [SerializeField] private int scene;
    
    AsyncOperation _asyncOperation;
    private void LoadScene(){
        StartCoroutine(LoadSceneAsync());
    }

    private void ActivateScene(){
        _asyncOperation.allowSceneActivation = true;
    }


    IEnumerator LoadSceneAsync(){
        yield return null;
        _asyncOperation = SceneManager.LoadSceneAsync(scene);
        _asyncOperation.allowSceneActivation = false;
        //When the load is still in progress, output the Text and progress bar
        while (!_asyncOperation.isDone)
        {
            float progressVal = Mathf.Clamp01(_asyncOperation.progress /0.9f);
            progressBar.fillAmount = progressVal;
            // Check if the load has finished
            if (_asyncOperation.progress >= 0.9f)
            {
                Invoke("ActivateScene",0.5f);
            }

            yield return null;
        }
    }

    

    void Start() {
        scene = GlobalValue.Instance.nextScene;
        LoadScene();
    }

    void Update() {
        
    }
}

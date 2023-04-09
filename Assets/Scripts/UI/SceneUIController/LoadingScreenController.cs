using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingScreenController : MonoBehaviour
{
    [SerializeField] Image _progressBar;
    [SerializeField] int _scene;
    [SerializeField] GameObject _waiter;

    [SerializeField] TMP_Text _waiterText;
   
    void LoadScene(){
        StartCoroutine(LoadSceneAsync());
    }

    void UpdateScreen(){
        _progressBar.gameObject.SetActive(false);
        _waiter.gameObject.SetActive(true);
    }

    IEnumerator LoadSceneAsync(){
        yield return null;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_scene);
        asyncOperation.allowSceneActivation = false;
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            float progressVal = Mathf.Clamp01(asyncOperation.progress /0.9f);
            _progressBar.fillAmount = progressVal;
            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                //replace progress bar with wait text
                Invoke("UpdateScreen", 1);
                //Wait to you click/tap to continue
                if (Input.GetButtonDown("Fire1"))
                    //Activate the Scene
                    asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    

    void Start() {
        _scene = GlobalValue.Instance.nextScene;
        _waiterText.text = UIString.Instance.waiterText;
        _waiter.gameObject.SetActive(false);
        LoadScene();
    }

    void Update() {
        
    }
}

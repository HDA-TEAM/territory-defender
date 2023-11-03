using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingScreenController : MonoBehaviour
{
    [SerializeField] Image progressBar;
    [SerializeField] int scene;
    [SerializeField] GameObject waiter;

    [SerializeField] TMP_Text waiterText;
   
    private void LoadScene(){
        StartCoroutine(LoadSceneAsync());
    }

    private void UpdateScreen(){
        progressBar.gameObject.SetActive(false);
        waiter.gameObject.SetActive(true);
    }

    IEnumerator LoadSceneAsync(){
        yield return null;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene);
        asyncOperation.allowSceneActivation = false;
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            float progressVal = Mathf.Clamp01(asyncOperation.progress /0.9f);
            progressBar.fillAmount = progressVal;
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
        scene = GlobalValue.Instance._nextScene;
        waiterText.text = UIString.Instance.WaiterText;
        waiter.gameObject.SetActive(false);
        LoadScene();
    }

    void Update() {
        
    }
}

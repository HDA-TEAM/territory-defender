using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SettingPanelController : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text quitBtnText;

    public void ApplicationQuit(){
        Application.Quit();
    }    

    public void MuteAll(){
        AudioListener.volume = 0;
    }

    public void UnMuteAll(){
        AudioListener.volume = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        title.text = UIString.Instance.SettingTitle;
        //_quitBtnText.text = UIString.Instance.quitBtnText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

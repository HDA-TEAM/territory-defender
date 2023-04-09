using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SettingPanelController : MonoBehaviour
{
    [SerializeField] TMP_Text _title;
    [SerializeField] TMP_Text _quitBtnText;

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
        _title.text = UIString.Instance.settingTitle;
        //_quitBtnText.text = UIString.Instance.quitBtnText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

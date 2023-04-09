using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LanguagePanelController : MonoBehaviour
{

    [SerializeField] TMP_Text _languageTitle;
    [SerializeField] TMP_Text _languageViBtn;
    [SerializeField] TMP_Text _languageEnBtn;

    public void langToVietnamese(){
        PlayerPrefs.SetString("Language", "vn");
        UIString.Instance.language = "vn";
        StageString.Instance.language = "vn";
        GlobalValue.Instance.nextScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(1);
    }

    public void langToEnglish(){
        PlayerPrefs.SetString("Language", "en");
        UIString.Instance.language = "en";
        StageString.Instance.language = "en";
        GlobalValue.Instance.nextScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(1);
    }

    // Start is called before the first frame update
    void Start()
    {
        _languageTitle.text = UIString.Instance.languageTitle;
        _languageViBtn.text = UIString.Instance.vietnameseBtnText;
        _languageEnBtn.text = UIString.Instance.englishBtnText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

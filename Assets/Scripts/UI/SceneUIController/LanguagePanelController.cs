using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LanguagePanelController : MonoBehaviour
{

    [SerializeField] private TMP_Text languageTitle;
    [SerializeField] private TMP_Text languageViBtn;
    [SerializeField] private TMP_Text languageEnBtn;

    public void LangToVietnamese(){
        PlayerPrefs.SetString("Language", "vn");
        UIString.Instance.language = "vn";
        StageString.Instance.language = "vn";
        GlobalValue.Instance.nextScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(1);
    }

    public void LangToEnglish(){
        PlayerPrefs.SetString("Language", "en");
        UIString.Instance.language = "en";
        StageString.Instance.language = "en";
        GlobalValue.Instance.nextScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(1);
    }

    // Start is called before the first frame update
    void Start()
    {
        languageTitle.text = UIString.Instance.LanguageTitle;
        languageViBtn.text = UIString.Instance.VietnameseBtnText;
        languageEnBtn.text = UIString.Instance.EnglishBtnText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

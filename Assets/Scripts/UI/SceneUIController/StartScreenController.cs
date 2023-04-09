using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartScreenController : MonoBehaviour
{
    [SerializeField] TMP_Text _startBtn;
    
    public void toMainMenu(){
        GlobalValue.Instance.nextScene = 3;
        SceneManager.LoadScene(1);
    }

    // Start is called before the first frame update
    void Start()
    {
        _startBtn.text = UIString.Instance.startBtnText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

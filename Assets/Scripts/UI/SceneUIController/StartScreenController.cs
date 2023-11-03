using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartScreenController : MonoBehaviour
{
    [SerializeField] private TMP_Text startBtn;
    
    public void ToMainMenu(){
        GlobalValue.Instance._nextScene = 3;
        SceneManager.LoadScene(1);
    }

    // Start is called before the first frame update
    private void Start()
    {
        startBtn.text = UIString.Instance.StartBtnText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

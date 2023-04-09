using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageLoader : MonoBehaviour
{
    [SerializeField] int _scene;
    public void load(){
        GlobalValue.Instance.nextScene = _scene;
        SceneManager.LoadScene(4);
    }
    
}

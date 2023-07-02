using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StageSelectController: MonoBehaviour
{
    private User _user;

    // Start is called before the first frame update
    private void Start()
    {
        _user = User.Instance;
        _user.saveUserData();
    } 

    // Update is called once per frame
    void Update()
    {

       
    }
}
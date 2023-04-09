using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StageSelectController: MonoBehaviour
{
    User user;

    // Start is called before the first frame update
    void Start()
    {
        user = User.Instance;
        user.saveUserData();
    } 

    // Update is called once per frame
    void Update()
    {

       
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UserController: MonoBehaviour
{
    [SerializeField] private TMP_Text username;
    [SerializeField] private Image expBar;

   private User _user;

    // Start is called before the first frame update
    void Start()
    {
        _user = User.Instance;
        float expVal = Mathf.Clamp01(_user.exp /0.9f);
        username.text = _user.username;
        expBar.fillAmount = expVal;
    } 

    // Update is called once per frame
    void Update()
    {

       
    }
    
}

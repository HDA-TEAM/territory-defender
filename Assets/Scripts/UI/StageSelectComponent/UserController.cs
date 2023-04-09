using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UserController: MonoBehaviour
{
    [SerializeField] TMP_Text _username;
    [SerializeField] Image _expBar;

    User user;

    // Start is called before the first frame update
    void Start()
    {
        user = User.Instance;
        float expVal = Mathf.Clamp01(user.exp /0.9f);
        _username.text = user.username;
        _expBar.fillAmount = expVal;
    } 

    // Update is called once per frame
    void Update()
    {

       
    }
    
}

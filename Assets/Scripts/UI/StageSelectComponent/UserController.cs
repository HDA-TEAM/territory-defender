using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UserController: MonoBehaviour
{
    [SerializeField] private TMP_Text _username;
    [SerializeField] private Image _expBar;

   private User _user;

    // Start is called before the first frame update
    void Start()
    {
        _user = User.Instance;
        float expVal = Mathf.Clamp01(_user._exp /0.9f);
        _username.text = _user._username;
        _expBar.fillAmount = expVal;
    } 

    // Update is called once per frame...

}

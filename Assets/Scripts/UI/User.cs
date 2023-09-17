using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class User : Singleton<User>
{
    public string _username = "Guest";
    public int _gem = 0;
    public int _star = 0;
    public float _exp = 0.2f;

    public void saveUserData(){
        PlayerPrefs.SetString("username", _username);
	    PlayerPrefs.SetInt("gem", _gem);
	    PlayerPrefs.SetInt("star", _star);
        PlayerPrefs.SetFloat("exp", _exp);
	    PlayerPrefs.Save();
    }

    public void loadUserData(){
        if (PlayerPrefs.HasKey("username")){
            _username = PlayerPrefs.GetString("username");
            _gem = PlayerPrefs.GetInt("gem");
            _star = PlayerPrefs.GetInt("star");
        }
    }
}

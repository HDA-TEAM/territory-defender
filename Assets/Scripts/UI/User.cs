using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : Singleton<User>
{
    public string username = "Guest";
    public int gem = 0;
    public int star = 0;
    public float exp = 0.2f;

    public void saveUserData(){
        PlayerPrefs.SetString("username", username);
	    PlayerPrefs.SetInt("gem", gem);
	    PlayerPrefs.SetInt("star", star);
        PlayerPrefs.SetFloat("exp", exp);
	    PlayerPrefs.Save();
    }

    public void loadUserData(){
        if (PlayerPrefs.HasKey("username")){
            username = PlayerPrefs.GetString("username");
            gem = PlayerPrefs.GetInt("gem");
            star = PlayerPrefs.GetInt("star");
        }
    }
}

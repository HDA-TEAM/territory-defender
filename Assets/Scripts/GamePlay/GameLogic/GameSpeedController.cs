using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeedController : MonoBehaviour
{
    /// <summary>
    /// Control game Speed in a stage
    /// </summary>
    public void ChangeGameSpeed(){
        switch(GameControler.GAME_SPEED){
            case (float)GameSpeed.Default:{
                GameControler.GAME_SPEED = (float)GameSpeed.Double;
                break;
            }
            case (float)GameSpeed.Double:{
                GameControler.GAME_SPEED = (float)GameSpeed.Default;
                break;
            }
        }
        Time.timeScale = GameControler.GAME_SPEED;
    }
}

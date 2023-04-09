using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControler : MonoBehaviour
{
    public static float GAME_SPEED = 1f;

    
}
public enum GameSpeed{
    Default = 1,
    Double = 2
}
public enum PlayerSide{
    Enemy = 0,
    Ally = 1,
}
public enum UnitType{
    Melee = 0,
    Range = 1
    
}
public enum MovingType{
    MoveDefault = 0,
    MoveToTarget = 1,
    WaitTargetArrived = 2,
}

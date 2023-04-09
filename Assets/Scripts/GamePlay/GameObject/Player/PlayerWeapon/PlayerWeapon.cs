using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    // private Vector2 _originPos;
    float distance = 0.25f;
    public void Default(){
        distance = distance * -1f;
        Vector2 targetPosition = new Vector2(transform.position.x + distance,transform.position.y);
        transform.position = targetPosition;
    }
    void Down(){
        Vector2 targetPosition = new Vector2(transform.position.x -0.25f,transform.position.y);
        transform.position = targetPosition;
    }
    void Rotate(){

    }

    public void Attack(bool flipY){
        // if(flipY == true){
        //     distance = distance * -1f;
        // }
        Vector2 targetPosition = new Vector2(transform.position.x + distance ,transform.position.y);
        transform.position = targetPosition;
    }
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update () {
        if(Input.GetKeyDown("up")){
            Default();
        }
        if(Input.GetKeyDown("down")){
            Down();
        }

    }
    // Update is called once per frame

}

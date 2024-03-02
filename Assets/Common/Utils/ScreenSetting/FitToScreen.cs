using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitToScreen : MonoBehaviour
{
    /// <summary>
    /// Scale the localSize following the runtime screen
    /// </summary>
   
    [SerializeField] private SpriteRenderer sr;
    
    private void Start()
    {
        float worldScreenHeight = Camera.main.orthographicSize * 2;

        // world width is calculated by diving world height with screen heigh
        // then multiplying it with screen width
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        // world height is always camera's orthographicSize * 2

        // to scale the game object we divide the world screen width with the
        // size x of the sprite, and we divide the world screen height with the
        // size y of the sprite
       
        transform.localScale = new Vector3(
            worldScreenWidth  / (sr.sprite.bounds.size.x * sr.transform.localScale.x * transform.localScale.x),
            worldScreenHeight / (sr.sprite.bounds.size.y * sr.transform.localScale.y * transform.localScale.y), 1);
        
       
    }
  

}

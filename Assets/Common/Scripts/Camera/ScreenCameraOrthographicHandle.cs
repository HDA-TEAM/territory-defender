using CustomInspector;
using UnityEngine;

public class ScreenCameraOrthographicHandle : MonoBehaviour
{
    [Button(nameof(Start))]
    public SpriteRenderer rink;
    
    // Use this for initialization
    void Start () {
        Camera.main.orthographicSize = rink.bounds.size.x / ((float)Screen.width / (float)Screen.height) / 2;
    }
}

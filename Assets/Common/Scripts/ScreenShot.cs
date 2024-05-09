using UnityEngine;

#if UNITY_EDITOR
public class ScreenShot : MonoBehaviour
{
    public KeyCode ScreenShotButton;
    private int _index;
    void Update()
    {
        if (Input.GetKeyDown(ScreenShotButton))
        {
            ScreenCapture.CaptureScreenshot($"screenshot{_index++}.png");
            Debug.Log("A screenshot was taken!");
        }
    }
}
#endif


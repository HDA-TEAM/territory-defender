using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    public KeyCode screenShotButton;
    void Update()
    {
        if (Input.GetKeyDown(screenShotButton))
        {
            ScreenCapture.CaptureScreenshot("screenshot.png");
            Debug.Log("A screenshot was taken!");
        }
    }
}

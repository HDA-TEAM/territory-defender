using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;


public class RescaleScreenUI : MonoBehaviour
{
    [SerializeField] CanvasScaler canvasUi;

    private float scaler;
    private int width;
    private int height;
    private float GetScale(int width, int height, Vector2 scalerReferenceResolution, float scalerMatchWidthOrHeight)
    {
        return Mathf.Pow(width / scalerReferenceResolution.x, 1f - scalerMatchWidthOrHeight) *
               Mathf.Pow(height / scalerReferenceResolution.y, scalerMatchWidthOrHeight);
    }
    void Awake()
    {
        SetCanvasScaler();
    }

    // Scaling canvas 
    private void SetCanvasScaler()
    {
        height = Screen.height;
        width = Screen.width;
        float screenScale = 16f/9;
        scaler = GetScale(height, width, new Vector2(width, height),1f);
        if (scaler >= screenScale)
        {
            if (canvasUi != null)
                canvasUi.matchWidthOrHeight = 1;
        }
        else
        {
            if (canvasUi != null)
                canvasUi.matchWidthOrHeight = 0;
        }
    }
}
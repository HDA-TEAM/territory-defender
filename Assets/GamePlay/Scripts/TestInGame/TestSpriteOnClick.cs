using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpriteOnClick : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("hello");
    }
    private void OnMouseDown()
    {
        Debug.Log("OnClick");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

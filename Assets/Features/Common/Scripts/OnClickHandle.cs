using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickHandle : MonoBehaviour
{
    void Start()
    {
        
    }

    //TODO
    //Task1: Solution for Event spam click
    // Static method to call an action after a delay
    public void DelayedAction(float delay, Action action)
    {
        StartCoroutine(DelayedActionCoroutine(delay, action));
    }

    private IEnumerator DelayedActionCoroutine(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }

    
    //Task2: Sound when clicking
    
}

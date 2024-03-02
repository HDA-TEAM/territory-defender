using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScreenNavigator.Runtime.Core.Modal;
using UnityScreenNavigator.Runtime.Core.Page;

public class HomeInit : MonoBehaviour
{
    private static PageContainer MainPageContainer => PageContainer.Find("MainPageContainer");
    private static ModalContainer MainModalContainer => ModalContainer.Find("MainModalContainer");
    private void Start()
    {
        ApplicationStarted();
    }
    public void ApplicationStarted()
    {
        Debug.Log(MainPageContainer);
        MainPageContainer.Push<HomePage>(ResourceKey.Prefabs.HomeScreen, false);
    }
}

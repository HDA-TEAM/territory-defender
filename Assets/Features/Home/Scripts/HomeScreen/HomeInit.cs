using UnityEngine;
using UnityScreenNavigator.Runtime.Core.Modal;
using UnityScreenNavigator.Runtime.Core.Page;

public class HomeController : SingletonBase<HomeController>
{
    private static PageContainer MainPageContainer => PageContainer.Find("MainPageContainer");
    private static ModalContainer MainModalContainer => ModalContainer.Find("MainModalContainer");
    private void Start()
    {
        ApplicationStarted();
    }
    public static void ApplicationStarted()
    {
        Debug.Log(MainPageContainer);
        MainPageContainer.Push<HomePage>(ResourceKey.Prefabs.HomeScreen, false);
    }
    public static void PopPage()
    {
        MainPageContainer.Pop(true);
    }
    public static void PopModal()
    {
        MainModalContainer.Pop(true);
    }
}

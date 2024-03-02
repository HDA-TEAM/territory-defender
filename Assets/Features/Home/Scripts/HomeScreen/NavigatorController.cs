using UnityScreenNavigator.Runtime.Core.Modal;
using UnityScreenNavigator.Runtime.Core.Page;

public class NavigatorController : SingletonBase<NavigatorController>
{
    private static PageContainer MainPageContainer => PageContainer.Find("MainPageContainer");
    public static ModalContainer MainModalContainer => ModalContainer.Find("MainModalContainer");
    private void Start()
    {
        PushScreen();
    }
    public static void PushScreen()
    {
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

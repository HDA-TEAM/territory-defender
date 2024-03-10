using UnityScreenNavigator.Runtime.Core.Modal;
using UnityScreenNavigator.Runtime.Core.Page;

public static class NavigatorController 
{
    //private static UIManagerStateMachine _uiStateMachine;
    private static PageContainer MainPageContainer => PageContainer.Find("MainPageContainer");
    public static ModalContainer MainModalContainer => ModalContainer.Find("MainModalContainer");
    
    public static void PushScreen()
    {
        MainPageContainer.Push<HomePage>(ResourceKey.Prefabs.HomeScreen, true);
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

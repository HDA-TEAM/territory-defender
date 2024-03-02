using UnityEngine;

public abstract class UIState
{
    public abstract void Enter();
    public abstract void Exit();
}
public interface IUIPopupState { }

public interface IUISceneState { }

public class HomeScreenState : UIState, IUISceneState
{
    public override void Enter()
    {
        HomeController.ApplicationStarted();
        Debug.Log("Home Screen is open");
    }

    public override void Exit() { } // TODO: do sth when turn off the game
}
public class HeroInfoState : UIState, IUISceneState
{
    public override void Enter()
    {
        // UiWindowCollectionStatic.HeroesScreen.Open();
        Debug.Log("Hero info is open");
    }

    public override void Exit()
    {
        //TODO
    } 
}

public class ShopState : UIState, IUISceneState
{
    public ShopState() { }

    public override void Enter() => Debug.Log("Entering Shop State");
    public override void Exit() => Debug.Log("Exiting Shop State");
}

public class DictionaryState : UIState, IUISceneState
{
    public DictionaryState() { }

    public override void Enter()
    {
        Debug.Log("Dictionary is open");
        // UiWindowCollectionStatic.DictionaryScreen.Open();
    }

    public override void Exit() {} //TODO
}

public class HistoryState : UIState, IUISceneState
{
    public HistoryState() { }

    public override void Enter()
    {
        Debug.Log("History is open");
        // UiWindowCollectionStatic.HistoryScreen.Open();
    } 
    public override void Exit() { } //TODO
}

public class MasteryPageState : UIState, IUIPopupState
{
    public MasteryPageState() { }

    public override void Enter()
    {
        Debug.Log("Mastery page is open");
        // UiWindowCollectionStatic.MasteryPagePopup.Open();
    } 
    public override void Exit()
    {
        // UiWindowCollectionStatic.MasteryPagePopup.Close();
    }
}

public class SettingState : UIState, IUIPopupState
{
    public SettingState() { }

    public override void Enter()
    {
        Debug.Log("Setting is open");
        // UiWindowCollectionStatic.SettingPopup.Open();
    } 
    public override void Exit()
    {
        // UiWindowCollectionStatic.SettingPopup.Close();
    }
}

public class QuestState : UIState, IUIPopupState
{
    public QuestState() { }

    public override void Enter()
    {
        Debug.Log("Quest is open");
        // UiWindowCollectionStatic.QuestPopup.Open();
    } 
    public override void Exit()
    {
        // UiWindowCollectionStatic.QuestPopup.Close();
    }
}

public class StageInfoState : UIState, IUIPopupState
{
    public StageInfoState() {}

    public override void Enter()
    {
        Debug.Log("Stage info is open");
        // UiWindowCollectionStatic.StageInfoPopup.Open();
    } 
    
    public override void Exit()
    {
        // UiWindowCollectionStatic.StageInfoPopup.Close();
    }
}




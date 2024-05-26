using Common.Scripts;
using Common.Scripts.Navigator;
using UnityEngine;

public abstract class UIState
{
    public abstract void Enter();
    public abstract void Exit();
}
public interface IUISceneState { }
public interface IUIPopupState { }

public class HomeScreenState : UIState, IUISceneState
{
    public override void Enter()
    { 
        NavigatorController.PushScreen();
        Debug.Log("Home Screen is open");
    }
    public override void Exit()
    {
        NavigatorController.PopModal();
    } // TODO: do sth when turn off the game
}
public class HeroInfoPuState : UIState, IUIPopupState
{
    public override void Enter()
    {
        NavigatorController.MainModalContainer.Push<HeroInformationPu>(ResourceKey.Prefabs.HeroInformationPu, true);
        Debug.Log("Hero info is open");
    }

    public override void Exit()
    {
        //TODO
        NavigatorController.PopModal();
    } 
}

public class ShopPuState : UIState, IUIPopupState
{
    public override void Enter() => Debug.Log("Entering Shop State");
    public override void Exit() => Debug.Log("Exiting Shop State");
}

public class DictionaryPuState : UIState, IUIPopupState
{
    public override void Enter()
    {
        NavigatorController.MainModalContainer.Push<DictionaryPu>(ResourceKey.Prefabs.DictionaryPu, true);
    }

    public override void Exit()
    {
        NavigatorController.PopModal();
    } //TODO
}

public class HistoryPuState : UIState, IUIPopupState
{
    public override void Enter()
    {
        Debug.Log("History is open");
        NavigatorController.MainModalContainer.Push<HistoryPu>(ResourceKey.Prefabs.HistoryPu, true);
    }

    public override void Exit()
    {
        NavigatorController.PopModal();
    } //TODO
}

public class MasteryPagePuState : UIState, IUIPopupState
{
    public override void Enter()
    {
        Debug.Log("Mastery page is open");
        NavigatorController.MainModalContainer.Push<MasteryPu>(ResourceKey.Prefabs.MasteryPu, true);
    } 
    public override void Exit()
    {
        NavigatorController.PopModal();
    }
}

public class SettingPuState : UIState, IUIPopupState
{
    public override void Enter()
    {
        Debug.Log("Setting is open");
        NavigatorController.MainModalContainer.Push<CommonSettingPu>(ResourceKey.Prefabs.SettingPu, true);
    } 
    public override void Exit()
    {
        NavigatorController.PopModal();
    }
}

public class QuestPuState : UIState, IUIPopupState
{
    public override void Enter()
    {
        Debug.Log("Quest is open");
        NavigatorController.MainModalContainer.Push<QuestPu>(ResourceKey.Prefabs.QuestPu, true);
    } 
    public override void Exit()
    {
        NavigatorController.PopModal();
    }
}
public class StageInfoPuState : UIState, IUIPopupState
{
    public override void Enter()
    {
        Debug.Log("Stage info is open");
        NavigatorController.MainModalContainer.Push<StageInformationPu>(ResourceKey.Prefabs.StageInformationPu, true);
    } 
    
    public override void Exit()
    {
        NavigatorController.PopModal();
    }
}




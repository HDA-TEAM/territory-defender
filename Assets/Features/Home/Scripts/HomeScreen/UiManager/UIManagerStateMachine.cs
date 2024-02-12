using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerStateMachine
{
    
    private Dictionary<Type, UIState> _states = new Dictionary<Type, UIState>();
    private static UIState _currentState;
    
    public UIManagerStateMachine()
    {
        // Pre-instantiate all state instances
        _states.Add(typeof(HomeScreenState), new HomeScreenState());
        _states.Add(typeof(HeroInfoState), new HeroInfoState());
        _states.Add(typeof(ShopState), new ShopState());
        _states.Add(typeof(DictionaryState), new DictionaryState());
        _states.Add(typeof(HistoryState), new HistoryState());
        
        _states.Add(typeof(MasteryPageState), new MasteryPageState());
        _states.Add(typeof(SettingState), new SettingState());
        _states.Add(typeof(QuestState), new QuestState());
    }

    public void ChangeState<T>() where T : UIState, new()
    {
        _currentState?.Exit();

        var type = typeof(T);
        if (!_states.TryGetValue(type, out UIState state))
        {
            state = new T();
            _states.Add(type, state);
        }

        _currentState = state;
        Debug.Log(state);
        _currentState.Enter();
    }
    public void BackPressed()
    {
        if (_currentState is IUIPopupState)
        {
            // Close the current popup
            _currentState.Exit();
            // Optionally, return to a specific state after closing a popup
        }
        else if (_currentState is IUISceneState)
        {
            // Return to Home Scene
            ChangeState<HomeScreenState>();
        }
    }
}

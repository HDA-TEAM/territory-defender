using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerStateMachine
{
    private Dictionary<Type, UIState> _states = new Dictionary<Type, UIState>();
    private static Stack<UIState> _popupStateStack = new Stack<UIState>();
    private static UIState _currentState;
    
    public UIManagerStateMachine()
    {
        InitializeStates();
    }
    private void InitializeStates()
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
        
        _states.Add(typeof(StageInfoState), new StageInfoState());
    }
    public void ChangeState<T>() where T : UIState, new()
    {
        var type = typeof(T);
        if (!_states.TryGetValue(type, out UIState nextState))
            return;
        
        if (_currentState == nextState)
        {
            return;
        }

        _currentState?.Exit();
        
        if (nextState is IUIPopupState)
        {
            _popupStateStack.Push(nextState);
        }
        else
        {
            while (_popupStateStack.Count > 0)
            {
                _popupStateStack.Pop().Exit(); // Ensure all popups are closed before moving to a new non-popup state
            }
        }

        _currentState = nextState;
        _currentState.Enter();
    }
    public void BackPressed()
    {
        if (_currentState is IUIPopupState && _popupStateStack.Count > 0)
        {
            // Close the current popup and remove it from the stack
            _currentState.Exit();
            _popupStateStack.Pop();

            if (_popupStateStack.Count > 0)
            {
                // Return to the previous popup
                _currentState = _popupStateStack.Peek();
                _currentState.Enter();
            }
            else
            {
                // No popups left, return to the home screen or a suitable default state
                ChangeState<HomeScreenState>();
            }
        }
        else
        {
            // Optionally, handle back navigation for non-popup states
            ChangeState<HomeScreenState>();
        }
    }
}

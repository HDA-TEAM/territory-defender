using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerStateMachine : SingletonBase<UIManagerStateMachine>
{
    private Dictionary<Type, UIState> _states = new Dictionary<Type, UIState>();
    private static Stack<UIState> _popupStateStack = new Stack<UIState>();
    private static UIState _currentState;
    
    private void Start()
    {
        InitializeStates();
        _states.TryGetValue(typeof(HomeScreenState), out UIState uiState);
        if (uiState != null)
            uiState.Enter();
    }
    private void InitializeStates()
    {
        // Pre-instantiate all state instances
        // Setup Pages
        _states.Add(typeof(HomeScreenState), new HomeScreenState());
        
        //Setup Modal
        _states.Add(typeof(HeroInfoPuState), new HeroInfoPuState());
        _states.Add(typeof(ShopPuState), new ShopPuState());
        _states.Add(typeof(DictionaryPuState), new DictionaryPuState());
        _states.Add(typeof(HistoryPuState), new HistoryPuState());
        _states.Add(typeof(MasteryPagePuState), new MasteryPagePuState());
        _states.Add(typeof(SettingPuState), new SettingPuState());
        _states.Add(typeof(QuestPuState), new QuestPuState());
        _states.Add(typeof(StageInfoPuState), new StageInfoPuState());
    }
    public void ChangeModalState<T>() where T : UIState, new()
    {
        var type = typeof(T);
        if (!_states.TryGetValue(type, out UIState nextState))
            return;
        
        if (_currentState == nextState)
            return;
        
        // Close the current state
        //_currentState?.Exit();

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
                _currentState.Exit();
                _currentState = null;
            }
        }
        // else
        // {
        //     _currentState = null;
        // }
        // else
        // {
        //     _currentState = null;
        //     // Optionally, handle back navigation for non-popup states
        //     //ChangePageState<HomeScreenState>();
        //     _currentState.Exit();
        // }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
public enum GameMode
{
    Normal,
    Hard
}

public class GameModeViewModel : MonoBehaviour
{
    [Header("UI")] [SerializeField] private List<ItemModeView> _itemModeViews;

    private ItemModeView _preSelectedModeItem;

    private static GameMode _currentMode;
    private void Awake()
    {
        // Automatically setup normal mode is begin
        OnModeSelected(_itemModeViews[0]);
        UpdateView();
    }

    private void UpdateView()
    {
        foreach (var item in _itemModeViews)
        {
            item.Setup(OnModeSelected);
        }
    }

    private void OnModeSelected(ItemModeView itemModeView)
    {
        if (_preSelectedModeItem == itemModeView) return;

        if (_preSelectedModeItem != null)
            _preSelectedModeItem.RemoveSelected();
        
        _preSelectedModeItem = itemModeView;
        _preSelectedModeItem.OnSelectedMode();
        
        _currentMode = itemModeView._gameMode;
    }
    public GameMode GetMode() => _currentMode;
}


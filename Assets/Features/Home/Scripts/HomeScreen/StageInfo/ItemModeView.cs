using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemModeView : MonoBehaviour
{
    [SerializeField] private Button _btn;
    [SerializeField] private Image _imageBg;
    [SerializeField] private Sprite _spriteSelectedMode;
    [SerializeField] private Sprite _spriteRemovedMode;
    
    public GameMode _gameMode;

    //Internal
    private Action<ItemModeView> _onSelected;
    private void Awake()
    {
        _btn.onClick.AddListener(OnSelectedMode);
    }

    public void Setup(Action<ItemModeView> onAction)
    {
        _onSelected = onAction;
    }
    public void OnSelectedMode()
    {
        _imageBg.sprite = _spriteSelectedMode;
        _onSelected?.Invoke(this);
    }

    public void RemoveSelected()
    {
        _imageBg.sprite = _spriteRemovedMode;
    }
}


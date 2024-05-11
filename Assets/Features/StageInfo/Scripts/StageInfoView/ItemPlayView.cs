using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemPlayView : MonoBehaviour
{
    [SerializeField] private Button _btnPlay;

    // Internal
    private Action<ItemPlayView> _onSelected;
    private void Awake()
    {
        _btnPlay.onClick.AddListener(OnButtonPlayClick);
    }
    
    public void Setup(Action<ItemPlayView> onAction)
    {
        _onSelected = onAction;
    }

    private void OnButtonPlayClick()
    {
        _onSelected?.Invoke(this);
    }
}


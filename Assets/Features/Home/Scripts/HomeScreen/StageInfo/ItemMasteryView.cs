using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemMasteryView : MonoBehaviour
{
    [SerializeField] private Button _btnMastery;

    private Action<ItemMasteryView> _onSelected;
    private void Awake()
    {
        _btnMastery.onClick.AddListener(OnButtonMasteryClick);
    }

    public void Setup(Action<ItemMasteryView> onAction)
    {
        _onSelected = onAction;
    }
    private void OnButtonMasteryClick()
    {
        _onSelected?.Invoke(this);
    }
}


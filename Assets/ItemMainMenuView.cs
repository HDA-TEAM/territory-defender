using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemMainMenuView : MonoBehaviour
{
    [SerializeField] private Button _btn;

    public AdditionMode _additionMode;
    private Action<ItemMainMenuView> _onSelected;

    // Start is called before the first frame update
    public void Setup(Action<ItemMainMenuView> onAction)
    {
        _onSelected = onAction;
        
        _btn.onClick.AddListener(OnSelectedItem);
    }

    private void OnSelectedItem()
    {
        _onSelected?.Invoke(this);
    }
}

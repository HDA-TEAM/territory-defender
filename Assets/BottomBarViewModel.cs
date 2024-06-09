using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BottomBarViewModel : MonoBehaviour
{
    [SerializeField] private List<ItemMainMenuView> _listItemMainMenuViews;
    [SerializeField] private GameObject _gameObject;

    private ItemMainMenuView _preItemSelected;
    void Awake()
    {
        _gameObject.SetActive(false);
        UpdateView();
    }
    private void UpdateView()
    {
        for (int i = 0; i < _listItemMainMenuViews.Count; i++)
        {
            _listItemMainMenuViews[i].Setup(OnSelectedAddition);
        }
    }

    private void OnSelectedAddition(ItemMainMenuView itemMainMenuView)
    {
        if (_preItemSelected != null)
            _preItemSelected.GameObject().SetActive(false);
        
        _preItemSelected = itemMainMenuView;
        _gameObject.SetActive(itemMainMenuView._additionMode == AdditionMode.On);
    }
}

public enum AdditionMode
{
    Off,
    On
}

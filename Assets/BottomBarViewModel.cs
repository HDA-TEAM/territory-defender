using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BottomBarViewModel : MonoBehaviour
{
    [SerializeField] private List<ItemMainMenuView> _listItemMainMenuViews;
    [SerializeField] private GameObject _gameObject;

    //private ItemMainMenuView _preItemSelected;
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
        //_preItemSelected = itemMainMenuView;
        if (itemMainMenuView._additionMode == AdditionMode.On)
        {
            _listItemMainMenuViews.Find(menu => menu._additionMode == AdditionMode.On).GameObject().SetActive(false);
            _gameObject.SetActive(true);
        } else {
            _listItemMainMenuViews.Find(menu => menu._additionMode == AdditionMode.On).GameObject().SetActive(true);
            _gameObject.SetActive(false);
        }
        
    }
}

public enum AdditionMode
{
    Off,
    On
}

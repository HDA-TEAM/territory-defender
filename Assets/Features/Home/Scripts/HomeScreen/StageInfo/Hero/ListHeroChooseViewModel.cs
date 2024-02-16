
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ListHeroChooseViewModel : MonoBehaviour
{
    [Header("UI")] 
    [SerializeField] private List<ItemHeroChooseView> _itemHeroChooseViews;
    
    private List<HeroComposite> _currentHero;
    private void Awake()
    {
        Debug.Log("<<<<<<<<<<<<<<<<");
        GameEvents.OnListCompositeSelected += HandleHeroListUpdated;
    }

    void OnDisable()
    {
        GameEvents.OnListCompositeSelected -= HandleHeroListUpdated;
    }

    private void HandleHeroListUpdated(List<IComposite> composites)
    {
        Debug.Log($"Received {composites.Count} composites.");
        _currentHero = composites.OfType<HeroComposite>().ToList();

        UpdateView();
    }

    private void UpdateData()
    {
        UpdateView();
    }

    private void UpdateView()
    {
        //Debug.Log(_currentHero);
        for (int i = 0; i < _itemHeroChooseViews.Count; i++ ) 
        {
            _itemHeroChooseViews[i].Setup(_currentHero[i], OnHeroChooseSelected);
        }
        
    }

    private void OnHeroChooseSelected(ItemHeroChooseView itemHeroChooseView)
    {
        Debug.Log("Choose Hero");
    }
}


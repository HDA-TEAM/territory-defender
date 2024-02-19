using System.Collections.Generic;
using UnityEngine;

public class ListHeroChooseViewModel : MonoBehaviour
{
    [Header("UI")] 
    [SerializeField] private List<ItemHeroChooseView> _itemHeroChooseViews;
    
    private static List<HeroComposite> _currentHeroes;

    private void Start()
    {
        UpdateData();
    }
    
    private void UpdateData()
    {
        var heroDataManager = HeroDataManager.Instance;
        _currentHeroes = heroDataManager.HeroComposites;
        UpdateView();
    }

    private void UpdateView()
    {
        //Debug.Log(_currentHero);
        for (int i = 0; i < _itemHeroChooseViews.Count; i++ ) 
        {
            _itemHeroChooseViews[i].Setup(_currentHeroes[i], OnHeroChooseSelected);
        }
    }

    private void OnHeroChooseSelected(ItemHeroChooseView itemHeroChooseView)
    {
        Debug.Log("Choose Hero");
    }
}


using System.Collections.Generic;
using UnityEngine;

public class ListHeroChooseViewModel : MonoBehaviour
{
    [Header("UI")] 
    [SerializeField] private List<ItemHeroChooseView> _itemHeroChooseViews;
    
    // Internal
    private static List<HeroComposite> _currentHeroes;
    private static HeroComposite _preHeroChoose;
    private ItemHeroChooseView _preItemHeroChooseView;
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
        for (int i = 0; i < _itemHeroChooseViews.Count; i++ ) 
        {
            _itemHeroChooseViews[i].Setup(_currentHeroes[i], OnHeroChooseSelected);
        }
    }

    private void OnHeroChooseSelected(ItemHeroChooseView itemHeroChooseView)
    {
        if (_preItemHeroChooseView == itemHeroChooseView) return;

        if (_preItemHeroChooseView != null)
            _preItemHeroChooseView.RemoveSelected();

        _preItemHeroChooseView = itemHeroChooseView;
        _preHeroChoose = itemHeroChooseView.HeroComposite;
    }

    public HeroComposite GetHeroChoose() => _preHeroChoose;
}


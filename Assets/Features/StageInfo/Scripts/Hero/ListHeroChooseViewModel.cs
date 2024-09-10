using Common.Scripts.Data.DataAsset;
using System.Collections.Generic;
using UnityEngine;

public class ListHeroChooseViewModel : MonoBehaviour
{
    [Header("UI")] 
    [SerializeField] private List<ItemHeroChooseView> _itemHeroChooseViews;
    [SerializeField] private HeroDataAsset _heroDataAsset;
    // Internal
    private static List<HeroComposite> _currentHeroes;
    private static HeroComposite _preHeroChoose;
    private ItemHeroChooseView _preItemHeroChooseView;
    private void Start()
    {
        UpdateData();
        
        //Setup default state
        _itemHeroChooseViews[0].OnSelectedHeroChoose();
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
            if (i < _currentHeroes.Count && _heroDataAsset.ListOwnedHeroNft.Contains(_currentHeroes[i].HeroId))
            {
                _itemHeroChooseViews[i].Setup(_currentHeroes[i], OnHeroChooseSelected); 
                _itemHeroChooseViews[i].gameObject.SetActive(true);
                
            } else {
                _itemHeroChooseViews[i].gameObject.SetActive(false);    
            }
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



using System;
using System.Collections.Generic;
using System.Linq;
using SuperMaxim.Messaging;
using UnityEngine;

public struct ListCompositePayload
{
    public List<IComposite> Composites;
}
public class DataSO : ScriptableObject
{
    public Dictionary<EHeroId, HeroComposite> _heroChooseComposites;
}
public class ListHeroChooseViewModel : MonoBehaviour
{
    [Header("UI")] 
    [SerializeField] private List<ItemHeroChooseView> _itemHeroChooseViews;
    //SO ListCompositeSo : => data
    private static List<IComposite> _currentHeroes;
    private void Awake()
    {
        Messenger.Default.Subscribe<ListCompositePayload>(Payload);
        Debug.Log("???????????????????");
        //GameEvents.OnListCompositeSelected += HandleHeroListUpdated;
        //HandleHeroListUpdated(GameEvents.CurHeroComposite.Cast<IComposite>().ToList());
    }
    
    private void Payload(ListCompositePayload payload)
    {
        Debug.Log(">>>>>>>>>>>>>>>>>>>>>>");
        _currentHeroes = payload.Composites.ToList();
        UpdateData();
    }
    
    private void OnDestroy()
    {
        Messenger.Default.Unsubscribe<ListCompositePayload>(Payload);
    }

    // private void HandleHeroListUpdated(List<IComposite> composites)
    // {
    //     Debug.Log($"Received {composites.Count} composites.");
    //     _currentHero = composites.OfType<HeroComposite>().ToList();
    //
    //     UpdateData();
    // }

    private void UpdateData()
    {
        UpdateView();
    }

    private void UpdateView()
    {
        //Debug.Log(_currentHero);
        List<HeroComposite> heroComposites = _currentHeroes.OfType<HeroComposite>().ToList();
        for (int i = 0; i < _itemHeroChooseViews.Count; i++ ) 
        {
            _itemHeroChooseViews[i].Setup(heroComposites[i], OnHeroChooseSelected);
        }
    }

    private void OnHeroChooseSelected(ItemHeroChooseView itemHeroChooseView)
    {
        Debug.Log("Choose Hero");
    }
}


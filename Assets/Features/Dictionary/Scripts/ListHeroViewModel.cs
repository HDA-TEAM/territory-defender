using System.Collections.Generic;
using UnityEngine;

public struct HeroComposite
{
    public string Name;
    public string Level;
    public string Hp;
    public string Atk;
    public string Def;
    public string Range;
    public Sprite Avatar;
}
public class ListHeroViewModel : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private List<ItemHeroView> _itemHeroViews;
    [SerializeField] private HeroDetailView _heroDetailView;

    [SerializeField] private ListHeroModeViewModel _listHeroModeViewModel;
    [Header("Data"), Space(12)] [SerializeField]
    private HeroesDataAsset _heroesDataAsset;

    // Internal
    private List<HeroComposite> _heroComposites;
    private ItemHeroView _preSelectedItem;

    private void Start()
    {
        _itemHeroViews[0].OnSelectedHero();
    }

    private void Awake()
    {
        _heroComposites = new List<HeroComposite>();
        UpdateData();
    }
    private void UpdateData()
    {
        List<HeroDataSO> heroDataSos = _heroesDataAsset.GetAllHeroData();
        
        foreach (var heroDataSo in heroDataSos)
        {
            _heroComposites.Add(
                new HeroComposite
                {
                    Name = heroDataSo._heroName,
                    Level = heroDataSo._heroLevel.ToString(),
                    Hp = heroDataSo._heroHp.ToString(),
                    Atk = heroDataSo._heroAtk.ToString(),
                    Def = heroDataSo._heroDef.ToString(),
                    Range = heroDataSo._heroRange.ToString("F2"),
                    Avatar = heroDataSo._heroImage
                }
            );
        }
        UpdateView();
    }
    private void UpdateView()
    {
        for (int i = 0; i < _itemHeroViews.Count; i++)
        {
            if (i < _heroComposites.Count)
            {
                _itemHeroViews[i].Setup(_heroComposites[i],OnSelectedItem);  
                _itemHeroViews[i].gameObject.SetActive(true);
            }
            else
            {
                _itemHeroViews[i].gameObject.SetActive(false);    
            }
            
        }
    }
    private void OnSelectedItem(ItemHeroView itemHeroView)
    {
        if (_preSelectedItem != null)
            _preSelectedItem.RemoveSelected();
        _preSelectedItem = itemHeroView;
        _heroDetailView.Setup(itemHeroView.HeroComposite);
        //_listHeroModeViewModel.Setup(itemHeroView.HeroComposite, EHeroViewMode.Skill);
    }
    
    
}

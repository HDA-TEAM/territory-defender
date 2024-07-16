using System.Collections.Generic;
using Common.Scripts;
using Common.Scripts.Data.DataConfig;
using Features.HeroInformation.Scripts.Hero;
using UnityEngine;

public class ListHeroViewModel : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private List<ItemHeroView> _itemHeroViews;
    [SerializeField] private List<ItemSkillView> _itemSkillViews;
    
    [SerializeField] private HeroDetailView _heroDetailView;
    [SerializeField] private ListModeViewModel _listModeViewModel;

    // Internal
    private List<HeroComposite> _heroComposites;
    private ItemHeroView _preSelectedItem;
    private ItemSkillView _preSelectedSkillItem;
    private bool _status;
    private void Start()
    {
        UpdateData();
        OnSelectedItem(_itemHeroViews[0]);
    }
    
    private void OnDisable()
    {
        ResetView();
    }
    private void UpdateData()
    {
        // Access the singleton instance directly.
        var heroDataManager = HeroDataManager.Instance;
    
        if (heroDataManager == null) return;
        if (heroDataManager.HeroComposites == null) return;

        // Update data from list hero data to HeroComposite
        _heroComposites = heroDataManager.HeroComposites;
    
        UpdateView();
        
    }
    private void UpdateView()
    {
        for (int i = 0; i < _itemHeroViews.Count; i++)
        {
            if (i < _heroComposites.Count)
            {
                // Setup hero property
                _itemHeroViews[i].Setup(_heroComposites[i],OnSelectedItem);  
                _itemHeroViews[i].gameObject.SetActive(true);
                
                // Setup hero skill
                foreach (var itemSkill in _itemSkillViews)
                {
                    itemSkill.Setup(OnSkillSelected);
                }
            } else {
                _itemHeroViews[i].gameObject.SetActive(false);    
            }
        }
    }
    private void OnSelectedItem(ItemHeroView itemHeroView)
    {
        //Prevent multiple clicks
        if (_preSelectedItem == itemHeroView) return;
        
        if (_preSelectedItem != null)
            _preSelectedItem.RemoveSelected();
        
        _preSelectedItem = itemHeroView;
        _preSelectedItem.OnSelected();

        // Setup hero detail view
        _heroDetailView.Setup(itemHeroView.HeroComposite);
        
        // reset to Skill view when switch another hero
        if (_preSelectedSkillItem != null)
        {
            _preSelectedSkillItem.ResetItemSkillView();
        }
        _listModeViewModel.ResetToSkillView(EHeroViewMode.Skill);
        _listModeViewModel.Setup(itemHeroView.HeroComposite, EHeroViewMode.Skill);
        
        OnSkillSelected(_itemSkillViews[0]);
    }
    private void OnSkillSelected(ItemSkillView itemSkillView)
    {
        foreach (var itemSkill in _itemSkillViews)
        {
            _status = itemSkill.SkillDescribeButton() == itemSkillView ? true : false;
            itemSkill.DescribeSkillImage().gameObject.SetActive(_status);
        }

        _preSelectedSkillItem = itemSkillView;
    }
    public void ResetView()
    {
        // Reset the selected hero
        if (_preSelectedItem != null)
        {
            _preSelectedItem.RemoveSelected();
            _preSelectedItem = null;
        }

        // Reset the selected skill
        if (_preSelectedSkillItem != null)
        {
            _preSelectedSkillItem.ResetItemSkillView(); // Assuming such a method exists
            _preSelectedSkillItem = null;
        }

        // Reset other UI elements
        // Select the first hero by default if the list is not empty
        if (_itemHeroViews != null)
        {
            _itemHeroViews[0].OnSelected();
        }
    }
}

public struct HeroComposite: IComposite
{
    public UnitId.Hero HeroId;
    public string Name;
    public string Level;
    public string Hp;
    public string Atk;
    public string Def;
    public string Range;
    public Sprite Avatar;
    public Sprite HeroChoose;
    public Sprite HeroOwned;
    
    public ESkillId ActiveSkillId;
    public ESkillId PassiveSkillId;
}

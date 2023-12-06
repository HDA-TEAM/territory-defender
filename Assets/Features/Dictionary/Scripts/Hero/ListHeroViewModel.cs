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

    public List<SkillDataSO> Skills;
}
public struct SkillComposite
{
    public string Name;
    public string SkillText;
    public Sprite SkillImage;
}
public class ListHeroViewModel : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private List<ItemHeroView> _itemHeroViews;
    [SerializeField] private List<ItemSkillView> _itemSkillViews;
    
    [SerializeField] private HeroDetailView _heroDetailView;
    [SerializeField] private ListModeViewModel _listModeViewModel;
    
    [Header("Data"), Space(12)] 
    [SerializeField] private HeroesDataAsset _heroesDataAsset;

    [SerializeField] private List<OnClickHandle> _onClickHandles;
    // Internal
    private List<HeroComposite> _heroComposites;
    private ItemHeroView _preSelectedItem;
    private ItemSkillView _preSelectedSkillItem;
    private bool _status;
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

        // Update data from list hero data to HeroComposite
        foreach (var heroDataSo in heroDataSos)
        {
            _heroComposites.Add(
                new HeroComposite
                {
                    Name = heroDataSo._stats.GetInformation(InformationId.Name),
                    Level = heroDataSo._stats.GetStat(StatId.Level).ToString(""),
                    Hp = heroDataSo._stats.GetStat(StatId.MaxHeal).ToString(""),
                    Atk = heroDataSo._stats.GetStat(StatId.AttackDamage).ToString(""),
                    Def = heroDataSo._stats.GetStat(StatId.Armour).ToString(""),
                    Range = heroDataSo._stats.GetStat(StatId.AttackRange).ToString("F2"),
                    Avatar = heroDataSo._heroImage,
                   
                    Skills = heroDataSo._heroSkills.GetAllSkillData()
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
                // Setup hero property
                _itemHeroViews[i].Setup(_heroComposites[i],OnSelectedItem);  
                _itemHeroViews[i].gameObject.SetActive(true);
                
                // Setup hero skill
                foreach (var itemSkill in _itemSkillViews)
                {
                    itemSkill.Setup(OnSkillSelected);
                }
            } 
            else 
            {
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
        // Use OnClickHandle to delay the setup action
        // _onClickHandles.ForEach(handle => handle.DelayedAction(1f, () => {
        //     _heroDetailView.Setup(itemHeroView.HeroComposite);
        //     _listModeViewModel.ResetToSkillView(EHeroViewMode.Skill);
        //     _listModeViewModel.Setup(itemHeroView.HeroComposite, EHeroViewMode.Skill);
        // }));
        
        _heroDetailView.Setup(itemHeroView.HeroComposite);
        
        // reset to Skill view when switch another hero
        if (_preSelectedSkillItem != null)
        {
            _preSelectedSkillItem.ResetSkillViews();
        }
        _listModeViewModel.ResetToSkillView(EHeroViewMode.Skill);
        _listModeViewModel.Setup(itemHeroView.HeroComposite, EHeroViewMode.Skill);
        
    }

    private void OnSkillSelected(ItemSkillView itemSkillView)
    {
        foreach (ItemSkillView itemSkill in _itemSkillViews)
        {
            _status = itemSkill.SkillDescribeButton() == itemSkillView ? true : false;
            itemSkill.DescribeSkillImage().gameObject.SetActive(_status);
        }

        _preSelectedSkillItem = itemSkillView;
    }
}

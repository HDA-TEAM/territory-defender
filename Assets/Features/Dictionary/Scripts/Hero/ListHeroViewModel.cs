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
    public SkillComposite PassiveSkill;
    public SkillComposite ActiveSkill;
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
    [SerializeField] private SkillsDataAsset _skillsDataAsset;

    // Internal
    private List<HeroComposite> _heroComposites;
    private List<SkillComposite> _skillComposites;
    private ItemHeroView _preSelectedItem;
    private ItemSkillView _peSelectedSkillItem;
    private bool _status;
    private void Start()
    {
        _itemHeroViews[0].OnSelectedHero();
    }

    private void Awake()
    {
        _heroComposites = new List<HeroComposite>();
        _skillComposites = new List<SkillComposite>();
        UpdateData();
    }
    private void UpdateData()
    {
        List<HeroDataSO> heroDataSos = _heroesDataAsset.GetAllHeroData();
        List<SkillDataSO> skillDataSos = _skillsDataAsset.GetAllSkillData();
        
        // Todo: fix the reverse list
        //skillDataSos.Reverse();
        
        // Update data from list skill data to SkillComposite
        foreach (var skillDataSo in skillDataSos)
        {
            _skillComposites.Add(
                new SkillComposite
                {
                    Name = skillDataSo._skillName,
                    SkillText = skillDataSo._skillText,
                    SkillImage = skillDataSo._skillImage
                }
            );
        }

        // Update data from list hero data to HeroComposite
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
                    Avatar = heroDataSo._heroImage,
                    PassiveSkill = _skillComposites[0],
                    ActiveSkill = _skillComposites[1]
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
        if (_preSelectedItem != null)
            _preSelectedItem.RemoveSelected();
        _preSelectedItem = itemHeroView;
        _heroDetailView.Setup(itemHeroView.HeroComposite);
        _listModeViewModel.Setup(itemHeroView.HeroComposite, EHeroViewMode.Skill);
    }

    private void OnSkillSelected(ItemSkillView itemItemSkillView)
    {
        foreach (ItemSkillView itemSkill in _itemSkillViews)
        {
            _status = itemSkill.SkillDescribeButton() == itemItemSkillView ? true : false;
            itemSkill.DescribeSkillImage().gameObject.SetActive(_status);
        }
    }
    
    
}

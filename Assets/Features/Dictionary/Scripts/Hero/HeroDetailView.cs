using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroDetailView : MonoBehaviour, IHeroModePageView
{
    [Header("Property"), Space(8)]
    [SerializeField] private TextMeshProUGUI _txtHeroName;
    [SerializeField] private TextMeshProUGUI _txtLevel;
    [SerializeField] private TextMeshProUGUI _txtHp;
    [SerializeField] private TextMeshProUGUI _txtAtk;
    [SerializeField] private TextMeshProUGUI _txtDef;
    [SerializeField] private TextMeshProUGUI _txtRange;
    [SerializeField] private Image _imgHero;
    
    [Header("Active Skill"), Space(8)]
    [SerializeField] private TextMeshProUGUI _txtActiveSkillName;
    [SerializeField] private TextMeshProUGUI _txtActiveSkillText;
    [SerializeField] private Image _imgActiveSkill;
    
    [Header("Passive Skill"), Space(8)]
    [SerializeField] private TextMeshProUGUI _txtPassiveSkillName;
    [SerializeField] private TextMeshProUGUI _txtPassiveSkillText;
    [SerializeField] private Image _imgPassiveSkill;

    [Header("Object Hero Detail"), Space(4)]
    [SerializeField] private GameObject _objHeroDetailView;
    
    #region Core
    public void Setup(HeroComposite heroComposite)
    {
        _txtHeroName.text = heroComposite.Name;
        _txtLevel.text = heroComposite.Level;
        _txtHp.text = heroComposite.Hp;
        _txtAtk.text = heroComposite.Atk;
        _txtDef.text = heroComposite.Def;
        _txtRange.text = heroComposite.Range;
        _imgHero.sprite = heroComposite.Avatar;
        
        _txtPassiveSkillName.text = heroComposite.Skills[0]._skillName;
        _txtPassiveSkillText.text = heroComposite.Skills[0]._skillText;
        _imgPassiveSkill.sprite = heroComposite.Skills[0]._skillImage;
        
        _txtActiveSkillName.text = heroComposite.Skills[1]._skillName;
        _txtActiveSkillText.text = heroComposite.Skills[1]._skillText;
        _imgActiveSkill.sprite = heroComposite.Skills[1]._skillImage;
    }

    public void PageSelected(bool isSelected)
    {
        _objHeroDetailView.gameObject.SetActive(isSelected);
    }

    #endregion
    
}

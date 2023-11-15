using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroDetailView : MonoBehaviour
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
        
        _txtPassiveSkillName.text = heroComposite.PassiveSkill.Name;
        _txtPassiveSkillText.text = heroComposite.PassiveSkill.SkillText;
        _imgPassiveSkill.sprite = heroComposite.PassiveSkill.SkillImage;
        
        _txtActiveSkillName.text = heroComposite.ActiveSkill.Name;
        _txtActiveSkillText.text = heroComposite.ActiveSkill.SkillText;
        _imgActiveSkill.sprite = heroComposite.ActiveSkill.SkillImage;
    }
    #endregion
    
}

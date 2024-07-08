using Common.Scripts.Data;
using Common.Scripts.Data.DataConfig;
using GamePlay.Scripts.Character.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.HeroInformation.Scripts.Hero
{
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
        [SerializeField] private SkillDataConfig _skillDataConfig;
    
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
            _imgHero.SetNativeSize();

            SetSkillInformation(heroComposite);

            //TODO: implement Skill Description for each Hero
            // if (heroComposite.Skills.Count > 3) 
            // {
            //     for (int i = 0; i < heroComposite.Skills.Count; i++)
            //     {
            //         if (i == 0)
            //         {
            //             _txtPassiveSkillName.text = heroComposite.Skills[i].GetInformation(InformationId.Name);
            //             _txtPassiveSkillText.text = heroComposite.Skills[i].GetInformation(InformationId.Description);
            //             _imgPassiveSkill.sprite = heroComposite.Skills[i]._skillImage;
            //         } else {
            //             _txtActiveSkillName.text = heroComposite.Skills[i].GetInformation(InformationId.Name);
            //             _txtActiveSkillText.text = heroComposite.Skills[i].GetInformation(InformationId.Description);
            //             _imgActiveSkill.sprite = heroComposite.Skills[i]._skillImage;
            //         }
            //     }
            // }
        }
        private void SetSkillInformation(HeroComposite heroComposite)
        {
            SkillDataSO activeSkill = _skillDataConfig.GetSkillDataById(heroComposite.ActiveSkillId);
            _txtActiveSkillName.text = activeSkill.GetInformation(InformationId.Name);
            _txtActiveSkillText.text = activeSkill.GetInformation(InformationId.Description);
        
            SkillDataSO passiveSkill = _skillDataConfig.GetSkillDataById(heroComposite.PassiveSkillId);
            _txtPassiveSkillName.text = passiveSkill.GetInformation(InformationId.Name);
            _txtPassiveSkillText.text = passiveSkill.GetInformation(InformationId.Description);
        }
        public void PageSelected(bool isSelected)
        {
            _objHeroDetailView.gameObject.SetActive(isSelected);
        }

        #endregion
    
    }
}

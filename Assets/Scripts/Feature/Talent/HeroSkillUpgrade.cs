using System;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace Feature.Talent
{
    public class CharacterSkillUnitConfig
    {
        public float Damage { get; set; }
    }
    public interface ISkill
    {
        public void UpgradeLevelSkill(CharacterSkillUnitConfig skillConfig);
        ISkill Apply(CharacterSkillUnitConfig skillConfig);
    }
    
    public class UpgradeHeroSkill : ISkill
    {
        public CharacterSkillUnitConfig SkillConfig { get; set; }

        public void UpgradeLevelSkill(CharacterSkillUnitConfig skillConfig)
        {
            SkillConfig.Damage += skillConfig.Damage;
        }

        public ISkill Apply(CharacterSkillUnitConfig skillConfig)
        {
            UpgradeLevelSkill(skillConfig);
            return this;
        }
    }
    
    public class HeroSkillUpgrade
    {
        public void UpgradeHeroSkill(CharacterSkillUnitConfig skillConfig)
        {
            UpgradeHeroSkill heroSkill = new UpgradeHeroSkill();
            heroSkill.Apply(skillConfig);
        }
    }
}
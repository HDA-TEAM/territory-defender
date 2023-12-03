namespace Features.Talent.Hero
{
    public class HeroSkillUpgrade : IHeroSkill
    {
        public SkillConfig SkillConfig { get; set; }
        
        public void UpgradeLevelSkill(SkillConfig skillConfig)
        {
            if (skillConfig is CharacterSkillConfig)
            {
                var characterConfig = (CharacterSkillConfig) skillConfig ;
                SkillConfig.Damage += characterConfig.Damage;
            }
            
            SkillConfig.Damage += skillConfig.Damage;
        }

        public ISkill Apply(SkillConfig skillConfig)
        {
            UpgradeLevelSkill(skillConfig);
            return this;
        }
    }
    
}
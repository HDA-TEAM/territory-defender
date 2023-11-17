namespace Features.Talent.Tower
{
    public class TowerSkillUpgrade : ITowerSkill
    {
        public SkillConfig SkillConfig { get; set; }

        public void UseSkill()
        {
            //Todo: something
        }

        public void UpgradeLevelSkill(SkillConfig skillConfig)
        {
            if (skillConfig is TowerSkillConfig)
            {
                var characterConfig = (TowerSkillConfig) skillConfig ;
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
    
    // //Usage
    // public class UsageTowerSkillUpgrade
    // {
    //     public void UpgradeHeroSkill(SkillConfig skillConfig)
    //     {
    //         TowerSkillUpgrade towerSkill = new TowerSkillUpgrade();
    //         towerSkill.Apply(skillConfig);
    //     }
    // }
        
    
}
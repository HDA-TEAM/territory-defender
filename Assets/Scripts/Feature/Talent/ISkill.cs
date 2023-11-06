namespace Features.Talent
{
    public class SkillConfig
    {
        public float Damage { get; set; }
    }
    
    // Child classes for SkillConfig
    public class CharacterSkillConfig : SkillConfig
    {
        public float Health { get; set; }
    }

    public class TowerSkillConfig : SkillConfig
    {
        public int Level { get; set; }
    }

    // Parent interface
    public interface ISkill
    {
        //void UseSkill();
        public void UpgradeLevelSkill(SkillConfig skillConfig);
        public ISkill Apply(SkillConfig skillConfig);
        
    }
    
    // Child interfaces
    public interface IHeroSkill : ISkill
    {
        //void SpecialAbility();
    }

    public interface ITowerSkill : ISkill
    {
        //void DefensiveAbility();
    }

}
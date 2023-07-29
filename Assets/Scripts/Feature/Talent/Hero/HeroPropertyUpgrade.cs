using System;

namespace Feature.Talent.Hero
{
    public class CharacterUnitConfig
    {
        public float Damage { get; set; }
        public float Health { get; set; }
        public float EffectRate { get; set; }
    }
    public interface IHero
    {
        public CharacterUnitConfig Config { get; set; }
        public void UpgradeProperty(CharacterUnitConfig unitConfig);
        public void Apply(CharacterUnitConfig unitConfig);
    }
    public class AssassinBase : IHero
    {
        public CharacterUnitConfig Config { get; set; }
        public void UpgradeProperty(CharacterUnitConfig unitConfig)
        {
            Config.Damage += unitConfig.Damage;
            Config.Health += unitConfig.Health;
            Config.EffectRate += unitConfig.EffectRate;
        }
        public void Apply(CharacterUnitConfig unitConfig) { }
    }
    public class WarriorBase : IHero
    {
        public CharacterUnitConfig Config { get; set; }
        public void UpgradeProperty(CharacterUnitConfig unitConfig)
        {
            Config.Damage += unitConfig.Damage;
            Config.Health += unitConfig.Health;
            Config.EffectRate += unitConfig.EffectRate;
        }
        public void Apply(CharacterUnitConfig unitConfig) { }
    }
    public class ShieldBase : IHero
    {
        public CharacterUnitConfig Config { get; set; }
        public void UpgradeProperty(CharacterUnitConfig unitConfig)
        {
            Config.Damage += unitConfig.Damage;
            Config.Health += unitConfig.Health;
            Config.EffectRate += unitConfig.EffectRate;
        }
        public void Apply(CharacterUnitConfig unitConfig) { }
    }
    public class CrowdControlBase : IHero
    {
        public CharacterUnitConfig Config { get; set; }
        public void UpgradeProperty(CharacterUnitConfig unitConfig)
        {
            Config.Damage += unitConfig.Damage;
            Config.Health += unitConfig.Health;
            Config.EffectRate += unitConfig.EffectRate;
        }
        public void Apply(CharacterUnitConfig unitConfig) { }
    }
    
    public static class HeroFactory
    {
        public static IHero CreateHero(HeroTypeEnum type)
        {
            switch (type)
            {
                case HeroTypeEnum.Assassin:
                    return new AssassinBase();

                case HeroTypeEnum.Warrior:
                    return new WarriorBase();

                case HeroTypeEnum.Shield:
                    return new ShieldBase();
                
                case HeroTypeEnum.CrowdControl:
                    return new CrowdControlBase();

                default:
                    throw new ArgumentException("Invalid hero type.");
            }
        }
    }
    
    //Property of the Hero
    public enum HeroTypeEnum{
        Warrior,
        Shield,
        CrowdControl,
        Assassin
    }
    
}
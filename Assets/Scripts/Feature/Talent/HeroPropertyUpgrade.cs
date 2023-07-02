using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace Feature.Talent
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
        void Apply(CharacterUnitConfig unitConfig);
    }
    
    public abstract class Hero : IHero
    {
        public CharacterUnitConfig Config { get; set; }
        public abstract void UpgradeProperty(CharacterUnitConfig unitConfig);
        public abstract void Apply(CharacterUnitConfig unitConfig);
    }
    
    public class AssassinBase : Hero
    {
        public override void UpgradeProperty(CharacterUnitConfig unitConfig)
        {
            Config.Damage += unitConfig.Damage;
            Config.Health += unitConfig.Health;
            Config.EffectRate += unitConfig.EffectRate;
        }
        public override void Apply(CharacterUnitConfig unitConfig) { }
    }
    public class WarriorBase : Hero
    {
        public override void UpgradeProperty(CharacterUnitConfig unitConfig)
        {
            Config.Damage += unitConfig.Damage;
            Config.Health += unitConfig.Health;
            Config.EffectRate += unitConfig.EffectRate;
        }
        public override void Apply(CharacterUnitConfig unitConfig) { }
    }
    public class ShieldBase : Hero
    {
        public override void UpgradeProperty(CharacterUnitConfig unitConfig)
        {
            Config.Damage += unitConfig.Damage;
            Config.Health += unitConfig.Health;
            Config.EffectRate += unitConfig.EffectRate;
        }
        public override void Apply(CharacterUnitConfig unitConfig) { }
    }
    public class CrowdControlBase : Hero
    {
        public override void UpgradeProperty(CharacterUnitConfig unitConfig)
        {
            Config.Damage += unitConfig.Damage;
            Config.Health += unitConfig.Health;
            Config.EffectRate += unitConfig.EffectRate;
        }
        public override void Apply(CharacterUnitConfig unitConfig) { }
    }
    
    public static class HeroFactory
    {
        public static Hero CreateHero(HeroTypeEnum type)
        {
            switch (type)
            {
                case HeroTypeEnum.Assassin:
                    return new AssassinBase {};

                case HeroTypeEnum.Warrior:
                    return new WarriorBase {};

                case HeroTypeEnum.Shield:
                    return new ShieldBase {};
                
                case HeroTypeEnum.CrowdControl:
                    return new CrowdControlBase {};

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
    
    // Hero class
    public class HeroPropertyUpgrade
    {
        private HeroTypeEnum _heroType;
        
        public void UpgradeClassAssassinProperty(CharacterUnitConfig unitConfig)
        {
            _heroType = HeroTypeEnum.Assassin;
            Hero assassinPage = HeroFactory.CreateHero(_heroType);
            assassinPage.UpgradeProperty(unitConfig);
        }
         
        public void UpgradeClassWarriorProperty(CharacterUnitConfig unitConfig)
        {
            _heroType = HeroTypeEnum.Warrior;
            Hero warriorPage = HeroFactory.CreateHero(_heroType);
            warriorPage.UpgradeProperty(unitConfig);
        }
        
        public void UpgradeClassCrowdControlProperty(CharacterUnitConfig unitConfig)
        {
            _heroType = HeroTypeEnum.CrowdControl;
            Hero crowdControlPage = HeroFactory.CreateHero(_heroType);
            crowdControlPage.UpgradeProperty(unitConfig);
        }
        
        public void UpgradeClassShieldProperty(CharacterUnitConfig unitConfig)
        {
            _heroType = HeroTypeEnum.Shield;
            Hero shieldPage = HeroFactory.CreateHero(_heroType);
            shieldPage.UpgradeProperty(unitConfig);
        }
        
    }

}
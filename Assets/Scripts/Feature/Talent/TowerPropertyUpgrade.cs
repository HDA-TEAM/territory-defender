using System;

namespace Feature.Talent
{
    public class TowerUnitConfig
    {
        public float Damage { get; set; }
        public float Health { get; set; }
        public float EffectRate { get; set; }
    }
    
    public interface ITower
    {
        public TowerUnitConfig Config { get; set; }
        public void UpgradeProperty(TowerUnitConfig unitConfig);
        void Apply(TowerUnitConfig unitConfig);
    }
    
    public abstract class Tower : ITower
    {
        public TowerUnitConfig Config { get; set; }
        public abstract void UpgradeProperty(TowerUnitConfig unitConfig);
        public abstract void Apply(TowerUnitConfig unitConfig);
    }
    
    public class BowBase : Tower
    {
        public override void UpgradeProperty(TowerUnitConfig unitConfig)
        {
            Config.Damage += unitConfig.Damage;
            Config.Health += unitConfig.Health;
            Config.EffectRate += unitConfig.EffectRate;
        }
        public override void Apply(TowerUnitConfig unitConfig) { }
    }
    public class DrumBase : Tower
    {
        public override void UpgradeProperty(TowerUnitConfig unitConfig)
        {
            Config.Damage += unitConfig.Damage;
            Config.Health += unitConfig.Health;
            Config.EffectRate += unitConfig.EffectRate;
        }
        public override void Apply(TowerUnitConfig unitConfig) { }
    }
    public class StickBase : Tower
    {
        public override void UpgradeProperty(TowerUnitConfig unitConfig)
        {
            Config.Damage += unitConfig.Damage;
            Config.Health += unitConfig.Health;
            Config.EffectRate += unitConfig.EffectRate;
        }
        public override void Apply(TowerUnitConfig unitConfig) { }
    }
    public class ElephantBase : Tower
    {
        public override void UpgradeProperty(TowerUnitConfig unitConfig)
        {
            Config.Damage += unitConfig.Damage;
            Config.Health += unitConfig.Health;
            Config.EffectRate += unitConfig.EffectRate;
        }
        public override void Apply(TowerUnitConfig unitConfig) { }
    }
    
    public static class TowerFactory
    {
        public static Tower CreateTower(TowerTypeEnum type)
        {
            switch (type)
            {
                case TowerTypeEnum.Bow:
                    return new BowBase() {};

                case TowerTypeEnum.Drum:
                    return new DrumBase() {};

                case TowerTypeEnum.Stick:
                    return new StickBase() {};
                
                case TowerTypeEnum.Elephant:
                    return new ElephantBase() {};

                default:
                    throw new ArgumentException("Invalid hero type.");
            }
        }
    }
    
    //Property of the Tower
    public enum TowerTypeEnum
    {
        Bow,
        Drum,
        Stick,
        Elephant
    }
    public class TowerPropertyUpgrade
    {
        private TowerTypeEnum _towerType;
        
        public void UpgradeClassBowProperty(TowerUnitConfig unitConfig)
        {
            _towerType = TowerTypeEnum.Bow;
            Tower bowPage = TowerFactory.CreateTower(_towerType);
            bowPage.UpgradeProperty(unitConfig);
        }
        
        public void UpgradeClassDrumProperty(TowerUnitConfig unitConfig)
        {
            _towerType = TowerTypeEnum.Drum;
            Tower drumPage = TowerFactory.CreateTower(_towerType);
            drumPage.UpgradeProperty(unitConfig);
        }
        
        public void UpgradeClassStickProperty(TowerUnitConfig unitConfig)
        {
            _towerType = TowerTypeEnum.Stick;
            Tower stickPage = TowerFactory.CreateTower(_towerType);
            stickPage.UpgradeProperty(unitConfig);
        }
        
        public void UpgradeClassElephantProperty(TowerUnitConfig unitConfig)
        {
            _towerType = TowerTypeEnum.Elephant;
            Tower elephantPage = TowerFactory.CreateTower(_towerType);
            elephantPage.UpgradeProperty(unitConfig);
        }
    }
}
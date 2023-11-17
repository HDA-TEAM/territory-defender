using System;

namespace Features.Talent.Tower
{
    public class TowerUnitConfig
    {
        public float Damage { get; set; }
        public float Level { get; set; }
    }
    
    public interface ITower
    {
        public TowerUnitConfig Config { get; set; }
        public void UpgradeProperty(TowerUnitConfig unitConfig);
        public void Apply(TowerUnitConfig unitConfig);
    }
    public class BowBase : ITower
    {
        public TowerUnitConfig Config { get; set; }
        public void UpgradeProperty(TowerUnitConfig unitConfig)
        {
            Config.Damage += unitConfig.Damage;
            Config.Level += unitConfig.Level;
        }
        public void Apply(TowerUnitConfig unitConfig) { }
    }
    public class DrumBase : ITower
    {
        public TowerUnitConfig Config { get; set; }
        public void UpgradeProperty(TowerUnitConfig unitConfig)
        {
            Config.Damage += unitConfig.Damage;
            Config.Level += unitConfig.Level;
        }
        public void Apply(TowerUnitConfig unitConfig) { }
    }
    public class StickBase : ITower
    {
        public TowerUnitConfig Config { get; set; }
        public  void UpgradeProperty(TowerUnitConfig unitConfig)
        {
            Config.Damage += unitConfig.Damage;
            Config.Level += unitConfig.Level;
        }
        public void Apply(TowerUnitConfig unitConfig) { }
    }
    public class ElephantBase : ITower
    {
        public TowerUnitConfig Config { get; set; }
        public void UpgradeProperty(TowerUnitConfig unitConfig)
        {
            Config.Damage += unitConfig.Damage;
            Config.Level += unitConfig.Level;
        }
        public void Apply(TowerUnitConfig unitConfig) { }
    }
    
    public static class TowerFactory
    {
        public static ITower CreateTower(TowerTypeEnum type)
        {
            switch (type)
            {
                case TowerTypeEnum.Bow:
                    return new BowBase();

                case TowerTypeEnum.Drum:
                    return new DrumBase();

                case TowerTypeEnum.Stick:
                    return new StickBase();
                
                case TowerTypeEnum.Elephant:
                    return new ElephantBase();

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
}
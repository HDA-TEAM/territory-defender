namespace Common.Scripts
{
    
public static class UnitId
{
    public enum BaseId
    {
        Ally = 1,
        Enemy = 2,
        Hero = 3,
        Tower = 4,
    }
    public static bool IsAllySide(BaseId baseId) => baseId is BaseId.Ally or BaseId.Hero or BaseId.Tower;
    public static bool IsEnemySide(BaseId baseId) => baseId == BaseId.Enemy;
    public static bool IsUnitInBaseId(int unitId, BaseId baseId) => unitId == (int)baseId;
    
    public enum Ally
    {
        Warrior = 100,
    }

    public enum Enemy
    {
        ShieldMan = 200,
        ArcherMan = 210,
        AssassinMan = 220,
    }

    public enum Hero
    {
        TrungTrac = 400,
        TrungNhi = 410,
    }

    public enum Tower
    {
        ArcherTower = 300,
        MarksmanTower = 301,
        SharpShooterTower = 302,
        SpearTower = 310,
        PikeTower = 311,
        MasterTower = 312,
        ElephantTower = 320,
        WarElephantTower = 321,
        ArmoredElephantTower = 322,
        DrumTower = 330,
        WarDrumTower = 331,
        MasterDrumTower = 332,
    }
    public enum Projectile
    {
        None = 1000,
        Arrow = 1001,
        WaterBomb = 1002,
    }
}

}

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
        SpearTower = 310,
        ElephantTower = 320,
        DrumTower = 330
    }
    public enum Projectile
    {
        None = 1000,
        Arrow = 1001,
        WaterBomb = 1002,
    }
}

}

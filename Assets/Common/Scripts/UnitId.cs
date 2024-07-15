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
            SpearMan = 101,
            PikeMan = 102,
            MasterPikeMan = 103,
        }

        public enum Enemy
        {
            ShieldMan = 201,
            ArcherMan = 211,
            AssassinMan = 221,
        }

        public enum Hero
        {
            TrungTrac = 400,
            TrungNhi = 410,
        }

        #region Tower
        public static TowerType GetTowerType(Tower tower)
        {
            switch (tower)
            {
                case Tower.ArcherTower:
                case Tower.MarksmanTower:
                case Tower.SharpShooterTower: return TowerType.ArcherTower;

                case Tower.SpearTower:
                case Tower.PikeTower:
                case Tower.MasterTower: return TowerType.SpearTower;

                case Tower.ElephantTower:
                case Tower.WarElephantTower:
                case Tower.ArmoredElephantTower: return TowerType.ElephantTower;

                case Tower.DrumTower:
                case Tower.WarDrumTower:
                case Tower.MasterDrumTower: return TowerType.DrumTower;

                default: return TowerType.ArcherTower;
            }
        }

        public enum TowerType
        {
            ArcherTower = 300,
            SpearTower = 310,
            ElephantTower = 320,
            DrumTower = 330,
        }

        public enum Tower
        {
            ArcherTower = 301,
            MarksmanTower = 302,
            SharpShooterTower = 303,

            SpearTower = 311,
            PikeTower = 312,
            MasterTower = 313,

            ElephantTower = 321,
            WarElephantTower = 322,
            ArmoredElephantTower = 323,

            DrumTower = 331,
            WarDrumTower = 332,
            MasterDrumTower = 333,
        }
        #endregion

        public enum Projectile
        {
            None = 1000,
            Arrow = 1001,
            WaterBomb = 1002,
        }
    }
}

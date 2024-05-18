using Common.Scripts;
using Features.Home.Scripts.HomeScreen.Common;

namespace Features.MasteryPage.Scripts.Tower
{
    public class ResetTowerRuneStrategy : ITowerRune
    {
        private UnitId.Tower _towerId;

        public ResetTowerRuneStrategy(UnitId.Tower id)
        {
            _towerId = id;
        }

        public void Execute(TowerRuneDataController controller)
        {
            controller.ResetTowerRuneData(_towerId);
        }
    }
}

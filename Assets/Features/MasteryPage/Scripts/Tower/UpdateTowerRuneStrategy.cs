using Common.Scripts;
using Features.Home.Scripts.HomeScreen.Common;

namespace Features.MasteryPage.Scripts.Tower
{
    public class UpdateTowerRuneStrategy : ITowerRune
    {
        private UnitId.Tower _towerId;
        private RuneComposite _runeComposite;

        public UpdateTowerRuneStrategy(UnitId.Tower id, RuneComposite composite)
        {
            _towerId = id;
            _runeComposite = composite;
        }

        public void Execute(TowerRuneDataController controller)
        {
            controller.UpgradeTowerRuneData(_towerId, _runeComposite);
        }
    }
}

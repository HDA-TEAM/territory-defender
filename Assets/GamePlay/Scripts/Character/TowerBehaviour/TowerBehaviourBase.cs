using GamePlay.Scripts.Character.Stats;
using GamePlay.Scripts.Tower.TowerKIT;

namespace GamePlay.Scripts.Character.TowerBehaviour
{
    public class TowerBehaviourBase : UnitBaseComponent
    {
        protected TowerKit _towerKit;
        public virtual void Setup(TowerKit towerKit)
        {
            _towerKit = towerKit;
            
            var rangeVal= _unitBaseParent.UnitStatsHandlerComp().GetCurrentStatValue(StatId.AttackRange);
            towerKit.TowerRangingHandler().SetUp(rangeVal);
        }
        public virtual void ShowTool()
        {
            _towerKit.TowerRangingHandler().SetShowRanging(true);
        }
    }
}

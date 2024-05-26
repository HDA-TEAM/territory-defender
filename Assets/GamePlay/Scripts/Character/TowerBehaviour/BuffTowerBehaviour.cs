using GamePlay.Scripts.Character.Stats;
using GamePlay.Scripts.Tower.TowerKIT;

namespace GamePlay.Scripts.Character.TowerBehaviour
{
    public class BuffTowerBehaviour : TowerBehaviourBase
    {
        public override void Setup(TowerKit towerKit)
        {
            _towerKit = towerKit;
            
            var rangeVal= _unitBaseParent.UnitStatsHandlerComp().GetCurrentStatValue(StatId.BuffRange);
            towerKit.TowerRangingHandler().SetUp(rangeVal);
        }
    }
}

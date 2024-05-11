using GamePlay.Scripts.Tower.TowerKIT;

namespace GamePlay.Scripts.Character.StateMachine.TowerBehaviour
{
    public class TowerBehaviourBase : UnitBaseComponent
    {
        protected TowerKit _towerKit;
        public virtual void Setup(TowerKit towerKit)
        {
            _towerKit = towerKit;
        }
        
    }
}

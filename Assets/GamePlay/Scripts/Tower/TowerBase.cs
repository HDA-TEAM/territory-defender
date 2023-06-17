using GamePlay.Scripts.Unit;

namespace GamePlay.Scripts.Tower
{
    public abstract class TowerBase : UnitBase
    {
        public abstract void TowerUpdate();
        public abstract void Sell();
        public abstract void Build();
        public abstract void Detail();
        
        // public abstract void Flag();
        public void Attack()
        {
            
        }
        public void Idle()
        {
            
        }
        public void Move()
        {
            
        }
        public void Destroy()
        {
            
        }

    }
}

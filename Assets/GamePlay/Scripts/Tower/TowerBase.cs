using UnityEngine;

namespace GamePlay.Scripts.Tower
{
    public abstract class TowerBase : MonoBehaviour
    {
        public abstract void TowerUpdate();
        public abstract void Sell();
        public abstract void Build();
        public abstract void Detail();
        // public abstract void Attack();
        // public abstract void Flag();
        

    }
}

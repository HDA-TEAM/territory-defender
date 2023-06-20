using GamePlay.Scripts.Tower;
using GamePlay.Scripts.Unit;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Scripts.Character
{
    public abstract class CharacterBase : UnitBase
    {
        // public void Attack()
        // {
        //     
        // }
        // public void Idle()
        // {
        //     
        // }
        // public void Move()
        // {
        //     
        // }
        public override void Destroy()
        {
            if (unitAttribute.health <= 0)
            {
                this.gameObject.SetActive(false);
                battleEventManager.RemoveUnit(this);
            }
        }
        public override void TakingDame(float dame)
        {
            this.unitAttribute.health -= dame;
            if (this.unitAttribute.health > 0 )
            {
                return;
            }
            this.CurrentActionEnum = ActionEnum.Destroy;
            Destroy();
        }
    }
}

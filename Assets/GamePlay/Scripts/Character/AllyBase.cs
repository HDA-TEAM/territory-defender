using GamePlay.Scripts.Tower;
using GamePlay.Scripts.Unit;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Scripts.Character
{
    public class AllyBase : CharacterBase
    {
        public Vector2 guardingPlace;
        protected override void Awake()
        {
            base.Awake();
            CombatConfigRule = new AllySideCombatConfigRule();
            guardingPlace = this.gameObject.transform.position;
        }
        public bool IsInGuardingPlace()
        {
            return VectorUtility.IsTwoPointReached(this.gameObject.transform.position, guardingPlace);
        }
        public void ReturnGuardingPlace()
        {
            this.gameObject.transform.position =  VectorUtility.Vector2MovingAToB(
                this.gameObject.transform.position,
                guardingPlace,
                this.unitAttribute.movementSpeed);
        }
        
    }
}

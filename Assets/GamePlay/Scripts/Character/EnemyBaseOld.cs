using GamePlay.Scripts.Tower;
using GamePlay.Scripts.Unit;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Scripts.Character
{
    public class EnemyBaseOld : CharacterBaseOld
    {
        public LineRenderer routeLineToDesGate;
        public int currentIndexInRouteLine;
        protected override void Awake()
        {
            base.Awake();
            CombatConfigRule = new EnemySideCombatConfigRule();
        }
        
        protected void ApproachTarget()
        {
            this.gameObject.transform.position = VectorUtility.Format3dTo2dZeroZ(Vector3.MoveTowards(
                this.gameObject.transform.position,
                target.gameObject.transform.position,
                Time.deltaTime * this.unitAttribute.movementSpeed));
        }
        protected void WaitingTargetComing()
        {
            //do nothing
        }
        protected void InAttackProcess()
        {
            unitAttribute.attackCoolDown -= Time.deltaTime;
            if (unitAttribute.attackCoolDown <= 0)
            {
                // ExcuteAnimator();
                target.TakingDame(unitConfig.attackDamage);
                Debug.Log("Enemy attack");
                unitAttribute.attackCoolDown = AttackMachineUtility.GetCooldownTime(unitAttribute.attackSpeedMin,unitAttribute.attackSpeedMax);
            }

        }
        protected void GoingToDestinationGate()
        {
            if (IsReachedDestinationGate())
            {
                this.gameObject.SetActive(false);
                return;
                // reduce player heath
                // return pooler
            }
            if (VectorUtility.IsTwoPointReached(
                    gameObject.transform.position, 
                    routeLineToDesGate.GetPosition(currentIndexInRouteLine)))
            {
                currentIndexInRouteLine += 1;
            }
            OnMoving();
        }
        private bool IsReachedDestinationGate()
        {
            return (currentIndexInRouteLine == routeLineToDesGate.positionCount - 1);
        }
        private void OnMoving()
        {
            this.gameObject.transform.position =VectorUtility.Vector2MovingAToB(
                this.gameObject.transform.position,
                routeLineToDesGate.GetPosition(currentIndexInRouteLine),
                this.unitAttribute.movementSpeed);
        }
        public override void Idle()
        {
            
        }
        public override void Attack()
        {
            
        }
    }
}

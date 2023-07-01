using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Scripts.Unit
{
    public class TowerDefenseBase : TowerBase
    {
        // public abstract void TowerUpdate();
        // public abstract void Sell();
        // public abstract void Build();
        // public abstract void Detail();
    
        // public abstract void Flag();
        public override void Attack()
        {
            if (target == null)
            {
                CurrentActionEnum = ActionEnum.Idle;
            }
            else
            {
                InAttackProcess();
            }
        }
        private void InAttackProcess()
        {
            unitAttribute.attackCoolDown -= Time.deltaTime;
            if (unitAttribute.attackCoolDown <= 0)
            {
                // ExcuteAnimator();
                unitAttribute.attackCoolDown = AttackMachineUtility.GetCooldownTime(unitAttribute.attackSpeedMin,unitAttribute.attackSpeedMax);
            }

        }
        public UnitBase FindNearestTargetInDetectRange(List<UnitBase> units)
        {
            float nearestDis = float.MaxValue;
            UnitBase targetUnit = null;
            foreach (var unit in units)
            {
                float curDis = Vector2.Distance(this.gameObject.transform.position,
                    unit.gameObject.transform.position);
                
                if ( curDis <= unitConfig.detectRange && curDis < nearestDis)
                {
                    targetUnit = unit;
                }
            }
            return targetUnit;
        }
        public override void Idle()
        {
            List<UnitBase> units = battleEventManager.FindUnitCollectionByTag("Enemy");
            this.target = FindNearestTargetInDetectRange(units);
            if (this.target != null)
            {
                this.CurrentActionEnum = ActionEnum.Attack;
            }
        }
        public void Move()
        {
            // Do nothing
        }
        public override void Destroy()
        {
        
        }
    }
}

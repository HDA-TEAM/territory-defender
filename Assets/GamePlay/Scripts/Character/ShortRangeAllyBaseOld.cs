using GamePlay.GameLogic.Scripts;
using GamePlay.Scripts.Tower;
using GamePlay.Scripts.Unit;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Scripts.Character
{
    public class ShortRangeAllyBaseOld : AllyBaseOld
    {
        protected override void Awake()
        {
            base.Awake();
            unitType = UnitType.ShortRangeTroop;
        }
        public override void Attack()
        {
            if (target != null)
            {
                if (GameObjectUtility.Distance2dOfTwoGameObject(this.gameObject, target.gameObject) <= unitConfig.attackRange)
                {
                    InAttackProcess();
                }
                else
                {
                    ApproachTarget();
                }
            }
            else
            {
                CurrentActionEnum = ActionEnum.Idle;
            }
            
        }
        
        void InAttackProcess()
        {
            unitAttribute.attackCoolDown -= Time.deltaTime;
            if (unitAttribute.attackCoolDown <= 0)
            {
                // ExcuteAnimator();
                Debug.Log("Ally short range attack");
                target.TakingDame(unitConfig.attackDamage);
                unitAttribute.attackCoolDown = AttackMachineUtility.GetCooldownTime(unitAttribute.attackSpeedMin,unitAttribute.attackSpeedMax);
            }

        }
        void ApproachTarget()
        {
            this.gameObject.transform.position =  VectorUtility.Vector2MovingAToB(
                this.gameObject.transform.position,
                target.gameObject.transform.position,
                this.unitAttribute.movementSpeed);
        }
        public UnitBaseOld FindTargetInDetectRange(List<UnitBaseOld> units)
        {
            float nearestDis = float.MaxValue;
            foreach (var unit in units)
            {
                float curDis = Vector2.Distance(guardingPlace, unit.gameObject.transform.position);
                if (curDis <= unitAttribute.detectRange && battleEventManager.IsCanFocusTarget(this,unit))
                {
                    if (unit.BeingTarget(this))
                    {
                        return unit;
                    }
                }
            }
            return null;
        }
        public override void Idle()
        {
            List<UnitBaseOld> units = battleEventManager.FindUnitCollectionByTag(UnitSideLabel.Enemy.ToString());
            this.target = FindTargetInDetectRange(units);
            if (this.target != null)
            {
                this.CurrentActionEnum = ActionEnum.Attack;
            }
            else
            {
                this.target = null;
                if (IsInGuardingPlace())
                {
                    //Do nothing
                }
                else
                {
                    ReturnGuardingPlace();
                }
            }
        }
    }
}

using GamePlay.Scripts.Tower;
using GamePlay.Scripts.Unit;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Scripts.Character
{
    public class ShortRangeEnemyBaseOld : EnemyBaseOld
    {
        protected override void Awake()
        {
            base.Awake();
            unitType = UnitType.ShortRangeTroop;
        }
        // public UnitBase FindNearestTargetInDetectRange(List<UnitBase> units)
        // {
        //     float nearestDis = float.MaxValue;
        //     UnitBase targetUnit = null;
        //     foreach (var unit in units)
        //     {
        //         float curDis = Vector2.Distance(this.gameObject.transform.position,
        //             unit.gameObject.transform.position);
        //         
        //         if ( curDis <= unitConfig.detectRange && curDis < nearestDis)
        //         {
        //             nearestDis = curDis;
        //             targetUnit = unit;
        //         }
        //     }
        //     return targetUnit;
        // }
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
                    WaitingTargetComing();
                }
            }
            else
            {
                CurrentActionEnum = ActionEnum.Idle;
            }
        }
        public override void Idle()
        {
            base.Idle();
            if (this.target != null)
            {
                this.CurrentActionEnum = ActionEnum.Attack;
            }
            else
            {
                GoingToDestinationGate();
            }
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Scripts.Unit
{
    public class TowerDefenseBaseOld : TowerBaseOld
    {
        [SerializeField] private GameObject bulletPrefab;
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
                Debug.Log("tower fire");
                var bullet = Instantiate(bulletPrefab);
                bullet.transform.SetParent(this.transform);
                bullet.transform.position = this.transform.position;
                var bulletBase = bullet.GetComponent<BulletBase>();
                bulletBase.SetUp(target.gameObject,unitAttribute.attackDamage);
                unitAttribute.attackCoolDown = AttackMachineUtility.GetCooldownTime(unitAttribute.attackSpeedMin,unitAttribute.attackSpeedMax);
            }

        }
        public UnitBaseOld FindNearestTargetInDetectRange(List<UnitBaseOld> units)
        {
            float nearestDis = float.MaxValue;
            UnitBaseOld targetUnit = null;
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
            List<UnitBaseOld> units = battleEventManager.FindUnitCollectionByTag("Enemy");
            this.target = FindNearestTargetInDetectRange(units);
            Debug.Log("Tower target " + target);
            if (this.target != null)
            {
                this.CurrentActionEnum = ActionEnum.Attack;
            }
        }
        public override void Destroy()
        {
        
        }
    }
}

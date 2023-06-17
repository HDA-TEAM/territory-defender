using GamePlay.Scripts.Tower;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Scripts.Unit
{
    public enum ActionEnum
    {
        Idle,
        Attack,
        Move,
        Destroy
    }
    public abstract class UnitBase : MonoBehaviour, IBaseAction
    {
        public UnitConfig unitConfig;
        public ICombatConfigRule CombatConfigRule; 
        public UnitType unitType;
        public UnitBase FindNearestTargetInDetectRange(List<UnitBase> units)
        {
            float nearestDis = float.MaxValue;
            UnitBase targetUnit = null;
            foreach (var unit in units)
            {
                float curDis = GameObjectUtility.Distance2dOfTwoGameObject(this.gameObject, unit.gameObject);
                if ( curDis <= unitConfig.detectRange && curDis < nearestDis)
                {
                    targetUnit = unit;
                }
            }
            return targetUnit;
        }
        
        
        public virtual void Attack()
        {
            
        }
        public virtual void Idle()
        {
            
        }
        public virtual void Move()
        {
            
        }
        public virtual void Destroy()
        {
            
        }
    }
}

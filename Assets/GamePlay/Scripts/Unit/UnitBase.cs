using GamePlay.GameLogic.Scripts;
using GamePlay.Scripts.Tower;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Scripts.Unit
{
    public enum ActionEnum
    {
        Idle,
        Attack,
        Destroy
    }
    public enum UnitType
    {
        Tower,
        ShortRangeTroop,
        MediumRangeTroop,
        LongRangeTroop,
        ShortLongRangeTroop,
        ShortMediumRangeTropp,
    }
    public abstract class UnitBase : MonoBehaviour
    {
        public UnitConfig unitConfig;
        public ICombatConfigRule CombatConfigRule;
        public CombatConfigRule CurrentCombatConfig;
        public UnitType unitType;
        public ActionEnum CurrentActionEnum;
        public BattleEventManager battleEventManager;
        public UnitBase target = null;
        public UnitAttribute unitAttribute;
        protected virtual void Awake()
        {
            unitAttribute = new UnitAttribute(unitConfig);
            if (battleEventManager == null)
            {
                battleEventManager = GameObject.FindObjectOfType<BattleEventManager>();
            }
        }
        
        private void OnEnable()
        {
            battleEventManager.AddUnit(this);
        }
        private void OnDisable()
        {
            // remove listen event
        }
        public string GetTagTarget()
        {
            return "";
        }
        
        
        
        public virtual void Attack()
        {
            
        }
        public virtual void Idle()
        {
            unitAttribute.attackCoolDown = 0f;
        }
        public virtual void Destroy()
        {
            
        }
        public void CheckTargetOnDestroy(UnitBase unitBase)
        {
            if (target == unitBase)
            {
                this.target = null;
                CurrentActionEnum = ActionEnum.Idle;
            }
        }
        public virtual void TakingDame(float dame)
        {
            
        }
        public bool BeingTarget(UnitBase unitBase)
        {
            if (this.target == null)
            {
                this.target = unitBase;
                this.CurrentActionEnum = ActionEnum.Attack;
                return true;
            }
            return false;
        }

    }
}

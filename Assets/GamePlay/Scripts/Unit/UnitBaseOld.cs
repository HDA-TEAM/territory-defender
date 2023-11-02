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
    public abstract class UnitBaseOld : MonoBehaviour
    {
        
        public ICombatConfigRule CombatConfigRule;
        public CombatConfigRule CurrentCombatConfig;
        public UnitType unitType;
        public ActionEnum CurrentActionEnum;
        public BattleEventManager battleEventManager;
        public UnitBaseOld target = null;
        public UnitAttribute unitAttribute;
        protected virtual void Awake()
        {
            if (battleEventManager == null)
            {
                battleEventManager = GameObject.FindObjectOfType<BattleEventManager>();
            }
        }
        
        private void Start()
        {
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



        public abstract void Attack();
        public abstract void Idle();
        public abstract void Destroy();
        public void CheckTargetOnDestroy(UnitBaseOld unitBaseOld)
        {
            if (target == unitBaseOld)
            {
                this.target = null;
                CurrentActionEnum = ActionEnum.Idle;
            }
        }
        public virtual void TakingDame(float dame)
        {
                
        }
        public bool BeingTarget(UnitBaseOld unitBaseOld)
        {
            if (this.target == null)
            {
                this.target = unitBaseOld;
                this.CurrentActionEnum = ActionEnum.Attack;
                return true;
            }
            return false;
        }

    }
}

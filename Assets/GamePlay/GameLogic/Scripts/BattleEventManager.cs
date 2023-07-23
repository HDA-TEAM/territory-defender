using GamePlay.Scripts.Tower;
using GamePlay.Scripts.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GamePlay.GameLogic.Scripts
{
    public enum UnitSideLabel
    {
        Ally,
        Enemy
    }
    public class BattleEventManager :Singleton<BattleEventManager>
    {
        private Dictionary<string, List<UnitBaseOld>> _unitDictionary = new Dictionary<string, List<UnitBaseOld>>();
        private List<BattleEvent> _battleEvents = new List<BattleEvent>();
        
        private void FixedUpdate()
        {
            if (_unitDictionary.Count != 0)
            {
                CheckExcuteState();
            }
        }
        void CheckExcuteState()
        {
            foreach (var unitCollection in _unitDictionary.Values.ToList())
            {
                foreach (var unit in unitCollection)
                {
                    switch (unit.CurrentActionEnum)
                    {
                        case ActionEnum.Idle: 
                        {
                            unit.Idle();
                            break;
                        }
                        case ActionEnum.Attack: 
                        {
                            unit.Attack();
                            break;
                        }
                        case ActionEnum.Destroy:
                        {
                            NotifyAllUnitCheckTargetOnDestroy(unit);
                            unit.Destroy();
                            break;
                        }
                    }
                    
                }
            }
        }
        void NotifyAllUnitCheckTargetOnDestroy(UnitBaseOld unitBaseOld)
        {
            foreach (var unitCollection in _unitDictionary.Values.ToList())
            {
                foreach (var unit in unitCollection)
                {
                    unit.CheckTargetOnDestroy(unitBaseOld);
                }
            }
        }
        public bool IsCanFocusTarget(UnitBaseOld unitAlly, UnitBaseOld unitEnemy)
        {
            if (unitEnemy.target == null)
            {
                return true;
            }
            switch (unitAlly.unitType)
            {
                case UnitType.ShortRangeTroop:
                {
                    if (CheckOnBattleWithThisTarget(unitEnemy))
                    {
                        return true;
                    }
                    break;
                }
            }
            return false;
        }
        bool CheckOnBattleWithThisTarget(UnitBaseOld enemyTarget)
        {
            CombatConfigRule combatConfigRule = enemyTarget.CombatConfigRule.ApplyRule(new CombatConfigRule());
            _unitDictionary.TryGetValue(UnitSideLabel.Ally.ToString(), out List<UnitBaseOld> unitBases);
            foreach (var ally in unitBases)
            {
                if (ally.target == enemyTarget )
                {
                    if (!combatConfigRule.IsRuleApproved(ally))
                    {
                        Debug.Log("MeleeCompetitorCounterAlly " + combatConfigRule.MeleeCompetitorCounter);
                        return false;
                    }
                }
            }
            return true;
        }
        
        // void FindTarget(UnitBase finder)
        // {
        //     foreach (var unitCollection in _unitDictionary)
        //     {
        //         if (finder.gameObject.tag != unitCollection.Key)
        //         {
        //             finder.FindNearestTargetInDetectRange(unitCollection.Value);
        //         }
        //     }
        // }
        public void AddUnit(UnitBaseOld unit)
        {
            List<UnitBaseOld> units = FindUnitCollectionByTag(unit.gameObject.tag);
            units.Add(unit);
        }
        public List<UnitBaseOld> FindUnitCollectionByTag(string tag)
        {
            if (_unitDictionary.ContainsKey(tag) == false)
            {
                _unitDictionary.Add(tag,new List<UnitBaseOld>());
            }
            _unitDictionary.TryGetValue(tag, out List<UnitBaseOld> unitBases);
            return unitBases;
        }
        public void RemoveUnit(UnitBaseOld unit)
        {
            List<UnitBaseOld> units = FindUnitCollectionByTag(unit.gameObject.tag);
            if (units.Contains(unit))
            {
                NotifyAllUnitCheckTargetOnDestroy(unit);
                units.Remove(unit);
            }
        }
    }

    public interface BattleEvent
    {
        void OnBattleChange();
    }

    public class UnitAttack : BattleEvent
    {
        public void OnBattleChange()
        {
            
        }
    }
    public class UnitDie : BattleEvent
    {
        public void OnBattleChange()
        {
            // Notify all character don't focus died enemy
        }
    }
}

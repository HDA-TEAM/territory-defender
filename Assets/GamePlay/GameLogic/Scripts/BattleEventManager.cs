using GamePlay.Scripts.Tower;
using GamePlay.Scripts.Unit;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.GameLogic.Scripts
{
    public class BattleEventManager : MonoBehaviour
    {
        private Dictionary<string, List<UnitBase>> _unitDictionary = new Dictionary<string, List<UnitBase>>();
        private List<BattleEvent> _battleEvents = new List<BattleEvent>();
        private void Update()
        {
            
        }
        void CheckIdleState()
        {
            foreach (var unitCollection in _unitDictionary.Values)
            {
                
                foreach (var unit in unitCollection)
                {
                    
                }
            }
            
        }
        
        
        void FindTarget(UnitBase finder)
        {
            foreach (var unitCollection in _unitDictionary)
            {
                if (finder.gameObject.tag != unitCollection.Key)
                {
                    finder.FindNearestTargetInDetectRange(unitCollection.Value);
                }
            }
        }
        public void AddUnit(UnitBase unit)
        {
            List<UnitBase> units = FindUnitCollectionByTag(unit.gameObject.tag);
            if (units != null)
            {
                units.Add(unit);
            }
        }
        private List<UnitBase> FindUnitCollectionByTag(string tag)
        {
            _unitDictionary.TryGetValue(tag, out List<UnitBase> units);
            if (units != null)
            {
                return units;
            }
            return null;
        }
        public void RemoveUnit(UnitBase unit)
        {
            List<UnitBase> units = FindUnitCollectionByTag(unit.gameObject.tag);
            if (units.Contains(unit))
            {
                units.Remove(unit);
            }
        }
        private void NotifyAllUnit()
        {
            foreach (var battleEvent in _battleEvents)
            {
                battleEvent.OnBattleChange();
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

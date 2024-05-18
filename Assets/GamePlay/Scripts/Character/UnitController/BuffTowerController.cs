using GamePlay.Scripts.Character.Stats;
using System.Collections.Generic;

namespace GamePlay.Scripts.Character.UnitController
{
    public class BuffTowerController : UnitController
    {
        private readonly List<UnitBase> _unitsBuffed = new List<UnitBase>();
        protected override void OnDisable()
        {
            base.OnDisable();
            RemoveBuffOnUnit();
        }
        public override void UpdateStatus(List<UnitBase> targets)
        {
            _unitsBuffed.Clear();
            foreach (var unit in targets)
            {
                if (unit == _unitBaseParent)
                    break;
            
                float betweenDistance = GameObjectUtility.Distance2dOfTwoGameObject(unit.gameObject, gameObject);

                if (betweenDistance < _unitBaseParent.UnitStatsHandlerComp().GetCurrentStatValue(StatId.BuffRange))
                {
                    _unitsBuffed.Add(unit);
                    unit.UnitStatsHandlerComp().BuffHandler.AddAttributeBuff(PrepareStatsBuff());
                }
            }
        }
        private AttributeBuff PrepareStatsBuff()
        {
            return new AttributeBuff(
                statIds: _unitBaseParent.UnitStatsHandlerComp().GetBaseStats().GetStatsCanBuff(),
                buffPercent: _unitBaseParent.UnitStatsHandlerComp().GetCurrentStatValue(StatId.BuffPercent)
            );
        }
        private void RemoveBuffOnUnit()
        {
            foreach (var unit in _unitsBuffed)
                unit.UnitStatsHandlerComp().BuffHandler.RemoveAttributeBuff();
        }
    }
}

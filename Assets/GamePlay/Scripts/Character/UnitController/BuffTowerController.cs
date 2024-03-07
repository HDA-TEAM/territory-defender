using System.Collections.Generic;

public class BuffTowerController : UnitController
{

    public override void UpdateStatus(List<UnitBase> targets)
    {
        foreach (var unit in targets)
        {
            float betweenDistance = GameObjectUtility.Distance2dOfTwoGameObject(unit.gameObject, gameObject);

            if (betweenDistance < _unitBaseParent.UnitStatsHandlerComp().GetCurrentStatValue(StatId.BuffRange))
            {
                unit.UnitStatsHandlerComp().BuffHandler().AddAttributeBuff(PrepareStatsBuff());
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
}

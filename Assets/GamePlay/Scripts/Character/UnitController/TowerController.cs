using GamePlay.Scripts.Character.Stats;
using System.Collections.Generic;

public class TowerController : UnitController
{
    public override void UpdateStatus(List<UnitBase> targets)
    {
        _unitBaseParent.CharacterStateMachine().UpdateStateMachine();
        // if (!CheckTargetAvailable())
        //     return;

        float nearestUnit = float.MaxValue;
        UnitBase target = null;
        foreach (var unit in targets)
        {
            float betweenDistance = GameObjectUtility.Distance2dOfTwoGameObject(unit.gameObject, this.gameObject);

            if (betweenDistance < _unitBaseParent.UnitStatsHandlerComp().GetCurrentStatValue(StatId.DetectRange))
            {
                if (nearestUnit > betweenDistance)
                {
                    nearestUnit = betweenDistance;
                    target = unit;
                }
            }
        }

        OnChangeTarget(target, BeingTargetCommand.None);
    }

}

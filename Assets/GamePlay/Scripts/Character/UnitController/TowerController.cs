using Common.Scripts.Utilities;
using GamePlay.Scripts.Character.Stats;
using GamePlay.Scripts.GamePlayController;
using System.Collections.Generic;

namespace GamePlay.Scripts.Character.UnitController
{
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
                if (!unit || !unit.HealthComp() || unit.HealthComp().IsDie())
                    continue;
                
                float betweenDistance = GameObjectUtility.Distance2dOfTwoGameObject(unit.gameObject, gameObject);

                if (betweenDistance < _unitBaseParent.UnitStatsHandlerComp().GetCurrentStatValue(StatId.AttackRange))
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
}

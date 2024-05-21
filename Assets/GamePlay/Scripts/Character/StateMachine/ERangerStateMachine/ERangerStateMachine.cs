using Common.Scripts.Utilities;
using GamePlay.Scripts.Character.StateMachine.EnemyStateMachine;
using GamePlay.Scripts.Character.Stats;

public class ERangerStateMachine : BaseEnemyStateMachine
{
    public override void CheckAttackingOrWaiting()
    {
        _isStopToAttack = CurrentTarget && GameObjectUtility.Distance2dOfTwoGameObject(gameObject, CurrentTarget.gameObject) < CharacterStats.GetCurrentStatValue(StatId.AttackRange);
        _isStopToWaiting = false;
    }
}

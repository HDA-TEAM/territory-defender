using GamePlay.Scripts.Character.StateMachine;
using UnityEngine;

public class EnemyDieState : CharacterDieState
{
    private readonly BaseEnemyStateMachine _context;
    public EnemyDieState(BaseEnemyStateMachine currentContext) : base(currentContext)
    {
        IsRootState = true; 
        _context = currentContext;
    }
    public override void EnterState()
    {
        AnimationClip deadClip = Context.AnimationController.DeadClip;
        Context.AnimationController.PlayClip(deadClip);
        _durationDie = deadClip.length;
    }
    public override void UpdateState()
    {
        _durationDie -= Time.deltaTime;
        CheckSwitchState();
    }
    public override void ExitState()
    {
        Context.AnimationController.StopAllClip();
        _context.gameObject.SetActive(false);
    }
    // public override void CheckSwitchState()
    // {
    //     if (_durationDie <= 0)
    //     {
    //         _durationDie = 0;
    //         ExitState();
    //     }
    // }
    public override void InitializeSubState()
    {
    }
}

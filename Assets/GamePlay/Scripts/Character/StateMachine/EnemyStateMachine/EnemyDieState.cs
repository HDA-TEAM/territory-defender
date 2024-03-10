using UnityEngine;

public class EnemyDieState : CharacterDieState
{
    private readonly BaseEnemyStateMachine _context;
    private static readonly int IsDie = Animator.StringToHash("IsDie");
    public EnemyDieState(BaseEnemyStateMachine currentContext) : base(currentContext)
    {
        IsRootState = true; 
        _context = currentContext;
    }
    public override void EnterState()
    {
        Animator animator = _context.CharacterAnimator;
        animator.SetBool(IsDie,true);
        _durationDie = animator.runtimeAnimatorController.animationClips[0].length;
    }
    public override void UpdateState()
    {
        _durationDie -= Time.deltaTime;
        CheckSwitchState();
    }
    public override void ExitState()
    {
        _context.CharacterAnimator.SetBool("IsDie",false);
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

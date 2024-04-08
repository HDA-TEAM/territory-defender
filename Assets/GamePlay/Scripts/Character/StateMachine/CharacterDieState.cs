using UnityEngine;

namespace GamePlay.Scripts.Character.StateMachine
{
    public class CharacterDieState : CharacterBaseState
    {
        protected float _durationDie;
        protected CharacterDieState(CharacterStateMachine currentContext) : base(currentContext)
        {
            IsRootState = true; 
        }
        public override void EnterState()
        {
            AnimationClip deadClip = Context.AnimationController.DeadClip;
            _durationDie = deadClip.length;
            Context.AnimationController.PlayClip(deadClip);
        }
        public override void UpdateState()
        {
            _durationDie -= Time.deltaTime;
            CheckSwitchState();
        }
        public override void ExitState()
        {
            Context.AnimationController.StopAllClip();
        }
        public override void CheckSwitchState()
        {
            if (_durationDie <= 0)
            {
                _durationDie = 0;
                ExitState();
            }
        }
        public override void InitializeSubState()
        {
        }
    }
}

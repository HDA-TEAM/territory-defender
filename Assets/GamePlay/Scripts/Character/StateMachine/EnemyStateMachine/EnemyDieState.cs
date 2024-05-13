using Common.Scripts;
using GamePlay.Scripts.GamePlayController;
using SuperMaxim.Messaging;
using UnityEngine;

namespace GamePlay.Scripts.Character.StateMachine.EnemyStateMachine
{
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
            Messenger.Default.Publish(new AudioPlayOneShotPayload
            {
                AudioClip = Context.AudioClipDeath,
            });
        
            Context.AnimationController.StopAllClip();
            Messenger.Default.Publish(new OnReturnObjectToPoolPayload
            {
                GameObject = Context.gameObject,
            });
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
}

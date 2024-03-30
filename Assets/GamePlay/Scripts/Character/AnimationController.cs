using Animancer;
using UnityEngine;

namespace GamePlay.Scripts.Character.StateMachine
{
    public class AnimationController : UnitBaseComponent
    {
        [SerializeField] private AnimancerComponent _animancerComponent;
        public AnimationClip MovingClip;
        public AnimationClip NormalAttackClip;
        public AnimationClip IdleClip;
        public AnimationClip DeadClip;

        public void PlayClip(AnimationClip animationClip)
        {
            if (animationClip == null)
            {
                Debug.LogError(" animationClip is null");
                return;
            }
            _animancerComponent.Play(animationClip);
        }
        public void StopAllClip()
        {
            _animancerComponent.Stop();
        }
    }
    
}

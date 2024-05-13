using Animancer;
using CustomInspector;
using UnityEngine;

namespace GamePlay.Scripts.Character
{
    public class AnimationController : UnitBaseComponent
    {
#if UNITY_EDITOR
        [Button(nameof(TestPlayClip))]
        public AnimationClip _testClip;
        public void TestPlayClip()
        {
            _animancerComponent.Stop();
            _animancerComponent.Play(_testClip);
        }
#endif
        [SerializeField] private AnimancerComponent _animancerComponent;
        
        public AnimationClip MovingClip;
        public AnimationClip NormalAttackClip;
        public AnimationClip IdleClip;
        public AnimationClip DeadClip;
        public AnimationClip FirstActiveSkillClip;
        
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

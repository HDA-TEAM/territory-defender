using Cysharp.Threading.Tasks;
using DG.Tweening;
using GamePlay.Scripts.Character;
using System;
using UnityEngine;

namespace GamePlay.Scripts.Projectile
{
    public class ProjectileMovement : ProjectileBaseComponent
    {
        [SerializeField] private AnimationCurve _movementCurve;
        [SerializeField] private ParticleSystem _particleCompleted;
        [SerializeField] private float _duration;
        [SerializeField] private float _unitHeight;
        private UnitBase _target;
        private string _attackSource;
        private Tween _movingTween;
        // Get route between cur pos to target 
        public void SetLineRoute(Vector2 posSpawn, EProjectileType bulletType, UnitBase target, string attackSource)
        {
            _target = target;
            _attackSource = attackSource;
            transform.position = posSpawn;
            _movingTween = new ProjectileTrajectoryRouteLine().ApplyLineRoute(
                curWeapon: gameObject,
                target: target, 
                customCurve: _movementCurve,
                duration: _duration,
                unitHeight: _unitHeight,
                callback: OnCompleted);
        }
        private void OnDisable() => _movingTween.Kill();
        private async void OnCompleted()
        {
            _projectileBase.GetProjectileDamage().DealDamage(_target, _attackSource);
        
            if (_particleCompleted)
            {
                _particleCompleted.Play();
                await UniTask.Delay(
                    TimeSpan.FromSeconds(_particleCompleted.main.duration));
                if (_particleCompleted)
                    _particleCompleted.Stop();
            }
        
            gameObject.SetActive(false);
        }

    }
}

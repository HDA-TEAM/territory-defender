using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class ProjectileMovement : ProjectileBaseComponent
{
    [SerializeField] private AnimationCurve _movementCurve;
    [SerializeField] private ParticleSystem _particleCompleted;
    [SerializeField] private float _duration;
    [SerializeField] private float _unitHeight;

    private UnitBase _target;
    // Get route between cur pos to target 
    public void GetLineRoute(Vector2 posSpawn, EProjectileType bulletType, UnitBase target)
    {
        _target = target;
        transform.position = posSpawn;
        new ProjectileTrajectoryRouteLine().ApplyLineRoute(
            curWeapon: gameObject,
            target: target, 
            customCurve: _movementCurve,
            duration: _duration,
            unitHeight: _unitHeight,
            callback: OnCompleted);
    }
    private async void OnCompleted()
    {
        _projectileBase.GetProjectileDamage().DealDamage(_target);
        
        if (_particleCompleted)
        {
            _particleCompleted.Play();
            await UniTask.Delay(
                TimeSpan.FromSeconds(_particleCompleted.main.duration));
            _particleCompleted.Stop();
        }
        
     
        this.gameObject.SetActive(false);
    }

}

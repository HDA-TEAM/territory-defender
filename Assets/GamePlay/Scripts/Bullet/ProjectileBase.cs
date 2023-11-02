using GamePlay.Scripts.Tower;
using System;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    [SerializeField] private ProjectileMovement _projectileMovement;
    [SerializeField] private ProjectileDamage _projectileDamage;
    public ProjectileMovement GetProjectileMovement() => _projectileMovement;
    public ProjectileDamage GetProjectileDamage() => _projectileDamage;
    private void OnValidate()
    {
        _projectileMovement ??= GetComponent<ProjectileMovement>();
        _projectileDamage ??= GetComponent<ProjectileDamage>();
    } 
}
public class ProjectileBaseComponent : MonoBehaviour
{
    [SerializeField] protected ProjectileBase _projectileBase;
    private void OnValidate() => _projectileBase ??= GetComponent<ProjectileBase>();
}

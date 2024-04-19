using Common.Scripts;
using SuperMaxim.Messaging;
using System.Linq;
using UnityEngine;

public enum EProjectileDealDamageType
{
    Crowd = 1,
    Single = 2,
    CrowdControl = 11,
    SingleControl = 12,
}
public class ProjectileDamage : ProjectileBaseComponent
{
    [SerializeField] private float _dame;
    [SerializeField] private float _affectRange;
    [SerializeField] private EProjectileDealDamageType _dealDamageType;
    [Header("Sounds"),Space(12)]
    [SerializeField] private AudioClip _audioClipHit;
    
    public void DealDamage(UnitBase target)
    {
        Messenger.Default.Publish(new AudioPlayOneShotPayload
        {
            AudioClip = _audioClipHit,
        });
        switch (_dealDamageType)
        {
            case EProjectileDealDamageType.Crowd:
                {
                    var dealType = new DealCrowdDamage();
                    dealType.SetUp(_affectRange, gameObject);
                    dealType.ApplyDealDamage(target,_dame);
                    return;
                }
            case EProjectileDealDamageType.Single:
                {
                    new DealSingleDamage().ApplyDealDamage(target,_dame);
                    return;
                }
        }
    }
    
}
public interface IProjectileDealDamageType
{
    void ApplyDealDamage(UnitBase mainTarget, float dame);
}

public class DealCrowdDamage: IProjectileDealDamageType
{
    private float _affectRange;
    private GameObject _projectile;
    public void SetUp(float range, GameObject projectile)
    {
        _affectRange = range;
        _projectile = projectile;
    }
    public void ApplyDealDamage(UnitBase mainTarget, float dame)
    {
        var targetList = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        foreach (var target in targetList)
        {
            if (GameObjectUtility.Distance2dOfTwoGameObject(_projectile, target) <= _affectRange)
            {
                var healComp = target.GetComponent<UnitBase>().HealthComp();
                if(healComp) healComp.PlayHurting(dame);
            }
        }
    }
}

public class DealSingleDamage : IProjectileDealDamageType
{
    public void ApplyDealDamage(UnitBase mainTarget, float dame)
    {
        if (mainTarget &&mainTarget.HealthComp())
            mainTarget.HealthComp().PlayHurting(dame);
    }
}

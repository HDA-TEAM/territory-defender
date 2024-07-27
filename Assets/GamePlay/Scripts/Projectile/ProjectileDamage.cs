using Common.Scripts;
using Common.Scripts.Utilities;
using GamePlay.Scripts.Character;
using SuperMaxim.Messaging;
using System.Linq;
using UnityEngine;

namespace GamePlay.Scripts.Projectile
{
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

        public void Setup(float dame, float affectRange)
        {
            _dame = dame;
            _affectRange = affectRange;
        }
        public void DealDamage(UnitBase target, string attackSource)
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
                        dealType.ApplyDealDamage(target,_dame, attackSource);
                        return;
                    }
                case EProjectileDealDamageType.Single:
                    {
                        new DealSingleDamage().ApplyDealDamage(target,_dame, attackSource);
                        return;
                    }
            }
        }
    
    }
    public interface IProjectileDealDamageType
    {
        void ApplyDealDamage(UnitBase mainTarget, float dame, string attackSource);
    }

    public class DealCrowdDamage: IProjectileDealDamageType
    {
        private float _affectRange;
        private string _attackSource;
        private GameObject _projectile;
        public void SetUp(float range, GameObject projectile)
        {
            _affectRange = range;
            _projectile = projectile;
        }
        public void ApplyDealDamage(UnitBase mainTarget, float dame, string attackSource)
        {
            var targetList = GameObject.FindGameObjectsWithTag("Enemy").ToList();
            foreach (var target in targetList)
            {
                if (GameObjectUtility.Distance2dOfTwoGameObject(_projectile, target) <= _affectRange)
                {
                    var healComp = target.GetComponent<UnitBase>().HealthComp();
                    if(healComp) healComp.PlayHurting(dame,attackSource);
                }
            }
        }
    }

    public class DealSingleDamage : IProjectileDealDamageType
    {
        public void ApplyDealDamage(UnitBase mainTarget, float dame, string attackSource)
        {
            if (!mainTarget || !mainTarget.HealthComp() || mainTarget.HealthComp().IsDie())
                return;
            
            HealthComp healthComp = mainTarget.HealthComp();
            healthComp.PlayHurting(dame, attackSource);

        }
    }
}
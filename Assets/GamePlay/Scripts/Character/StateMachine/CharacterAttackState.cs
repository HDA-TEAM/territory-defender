using Common.Scripts;
using DG.Tweening;
using GamePlay.Scripts.Character.AttackingComponent;
using GamePlay.Scripts.Character.Stats;
using GamePlay.Scripts.GamePlayController;
using GamePlay.Scripts.Projectile;
using SuperMaxim.Messaging;
using UnityEngine;

namespace GamePlay.Scripts.Character.StateMachine
{
    public class CharacterAttackState : CharacterBaseState
    {
        private float _cooldownNextAttack;
        private float _attackDame;
        private float _onceNormalAttackDuringTime;
        protected Sequence _attackSequence;
        protected CharacterAttackState(CharacterStateMachine currentContext) : base(currentContext)
        {
            IsRootState = true;
        }

        public override void EnterState()
        {
            _attackSequence = DOTween.Sequence();
            _attackDame = Context.CharacterStats.GetCurrentStatValue(StatId.AttackDamage);

            // Reset cooldown next attack time
            SetAttackAnim(true);
            float originAttack = Context.CharacterStats.GetCurrentStatValue(StatId.AttackSpeed);
            if (originAttack <= 0)
                originAttack = 1;
            _cooldownNextAttack = 1 / originAttack;
            _onceNormalAttackDuringTime = Context.AnimationController.NormalAttackClip.length;

            HandleAttackProcessing();
        }
        public override void UpdateState()
        {
            CheckSwitchState();
        }
        public override void ExitState()
        {
            _attackSequence.Kill();
            Context.AnimationController.PlayClip(Context.AnimationController.IdleClip);
        }
        public override void CheckSwitchState() { }
        public override void InitializeSubState() { }
        private void SetAttackAnim(bool isInAttack)
        {
            Context.AnimationController.PlayClip(isInAttack ? Context.AnimationController.NormalAttackClip : Context.AnimationController.IdleClip);
        }
        private void HandleAttackProcessing()
        {

            Tween attackOnceTime = DOVirtual.DelayedCall(_onceNormalAttackDuringTime, () =>
            {
                SetAttackAnim(false);
                PlayingAttackOnceTime();
            }, ignoreTimeScale: false);

            Tween waitingNextAttack = DOVirtual.DelayedCall(_cooldownNextAttack, () => SetAttackAnim(true), ignoreTimeScale: false);

            _attackSequence.Append(attackOnceTime);
            _attackSequence.Append(waitingNextAttack);

            _attackSequence.SetLoops(-1);
            _attackSequence.Play();
        }
        private void PlayingAttackOnceTime()
        {
            Messenger.Default.Publish(new AudioPlayOneShotPayload
            {
                AudioClip = Context.AudioClipAttack,
            });

            switch (Context.CharacterTroopBehaviourType)
            {
                case TroopBehaviourType.Ranger:
                case TroopBehaviourType.Tower:
                    {
                        // Tower don't need to check distance, it always fire any target exist
                        if (Context.CharacterProjectileIUnitId == UnitId.Projectile.None)
                            return;

                        Messenger.Default.Publish(new OnSpawnObjectPayload
                        {
                            ActiveAtSpawning = false,
                            ObjectType = Context.CharacterProjectileIUnitId.ToString(),
                            InitPosition = Vector3.zero,
                            OnSpawned = SetUpProjectile,
                        });
                        return;
                    }
                case TroopBehaviourType.Melee:
                    {
                        new CharacterAttackingFactory().GetAttackingStrategy(TroopBehaviourType.Melee).PlayAttacking(Context.CurrentTarget, _attackDame);

                        return;
                    }
                default:
                    {
                        return;
                    }
            }
        }
        private void SetUpProjectile(GameObject projectile)
        {
            ProjectileBase prjBase = projectile.GetComponent<ProjectileBase>();
            var dameComp = prjBase.GetComponent<ProjectileDamage>();
            dameComp.Setup(Context.UnitBaseParent().UnitStatsHandlerComp().GetCurrentStatValue(StatId.AttackDamage));
            prjBase.GetProjectileMovement().SetLineRoute(Context.StartAttackPoint.position, EProjectileType.Arrow, Context.CurrentTarget);
            projectile.SetActive(true);
        }
    }
}

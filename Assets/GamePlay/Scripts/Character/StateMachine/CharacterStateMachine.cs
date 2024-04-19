using Common.Scripts;
using GamePlay.Scripts.Character.Stats;
using UnityEngine;

namespace GamePlay.Scripts.Character.StateMachine
{
    public class  CharacterStateMachine : UnitBaseComponent
    {
        [SerializeField] private string _curStateLabel;
        [SerializeField] protected TroopBehaviourType _troopBehaviourType;
        [SerializeField] protected AnimationController _animationController;
        protected CharacterBaseState _currentState;
        protected StatsHandlerComponent _stats;
        [SerializeField] private ProjectileDataAsset _projectileDataAsset;
        [SerializeField] private UnitId.Projectile _projectileId;
        [SerializeField] private BeingTargetCommand _beingTargetCommand;
        [SerializeField] private Transform _startAttackPoint;
        
        [Header("Sounds"),Space(12)]
        [SerializeField] private AudioClip _audioClipAttack;
        [SerializeField] private AudioClip _audioClipDeath;
        
        #region Setter and getter
        public CharacterBaseState CurrentState
        {
            set
            {
                _currentState = value;
                _curStateLabel = _currentState.ToString();
            }
            get { return _currentState; }
        }
        public Transform StartAttackPoint
        {
            get
            {
                if (_startAttackPoint == null)
                    return transform;
                return _startAttackPoint;
            }
        }
        public AudioClip AudioClipAttack { get { return _audioClipAttack; } }
        public AudioClip AudioClipDeath { get { return _audioClipDeath; } }
        public UnitBase CurrentTarget { get { return _unitBaseParent.CurrentTarget; } }
        public BeingTargetCommand BeingTargetCommand { get { return _beingTargetCommand; } }
        public ProjectileDataAsset CharacterProjectileDataAsset { get { return _projectileDataAsset; } }
        public UnitId.Projectile CharacterProjectileIUnitId { get { return _projectileId; } }
        public TroopBehaviourType CharacterTroopBehaviourType { get { return _troopBehaviourType; } }
        public AnimationController AnimationController { get { return _animationController; } }
        public StatsHandlerComponent CharacterStats { get { return _stats; } }
        #endregion
        protected override void Awake()
        {
            _animationController = _unitBaseParent.AnimationController();
            _stats = _unitBaseParent.UnitStatsHandlerComp();
        }
        public void UpdateStateMachine() => _currentState.UpdateStates();

        protected virtual void OnEnable()
        {
            _unitBaseParent.OnTargetChanging += OnTargetChanging;
        }
        protected virtual void OnDisable()
        {
            _unitBaseParent.OnTargetChanging -= OnTargetChanging;
        }
        protected virtual void SetDefaultStatus()
        {
         
        }
        // // Handle target is null
        // private void OnRecheckTarget()
        // {
        //     if (CurrentTarget == null || !CurrentTarget.gameObject.activeSelf)
        //     {
        //         OnTargetChanging(new UnitBase.OnTargetChangingComposite()
        //         {
        //             Target = null,
        //             BeingTargetCommand = BeingTargetCommand.None
        //         });
        //     }
        // }

        protected virtual void OnTargetChanging(UnitBase.OnTargetChangingComposite composite)
        {
            _unitBaseParent.CurrentTarget = composite.Target;
            _beingTargetCommand = composite.BeingTargetCommand;
        }
    }
}

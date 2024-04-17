using GamePlay.Scripts.Character.Stats;
using GamePlay.Scripts.Data;
using UnityEngine;

namespace GamePlay.Scripts.Character.StateMachine.EnemyStateMachine
{
    public class BaseEnemyStateMachine : CharacterStateMachine
    {
        [SerializeField] private LineRenderer _routeToGate;
        [SerializeField] private InGameInventoryRuntimeData _inGameInventoryRuntimeData;

        private EnemyStateFactory _factory;
        private int _currentIndexInRouteLine;
        private bool _isMovingToGate;
        private bool _isDie;
        protected bool _isStopToAttack;
        protected bool _isStopToWaiting;

        #region Event
        protected override void OnEnable()
        {
            base.OnEnable();
            SetDefaultStatus();
            _unitBaseParent.OnDie += OnDie;
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            _unitBaseParent.OnDie -= OnDie;
        }
        protected override void SetDefaultStatus()
        {
            _isDie = false;
            _factory = new EnemyStateFactory(this);
            _currentState = _factory.GetState(CharacterState.Idle);
            _currentState.EnterState();
            _isMovingToGate = true;
            _isStopToAttack = false;
        }
        #endregion
        private void OnDie(bool isDie) => _isDie = isDie;

        #region Setter and Getter
        public EnemyStateFactory StateFactory { get { return _factory; } }
        public bool IsDie { get { return _isDie; } }
        public bool IsStopToAttackingOrWaiting() => (_isStopToWaiting || _isStopToAttack) && !_isMovingToGate;
        public bool IsStopToAttack { get { return _isStopToAttack; } }
        public bool IsStopToWaiting { get { return _isStopToWaiting; } }
        public virtual void CheckAttackingOrWaiting()
        {
            _isStopToAttack = CurrentTarget && GameObjectUtility.Distance2dOfTwoGameObject(gameObject, CurrentTarget.gameObject) < CharacterStats.GetCurrentStatValue(StatId.AttackRange);
            _isStopToWaiting = !_isStopToAttack;
        }
        public bool IsMovingToGate { get { return _isMovingToGate; } }
        public LineRenderer RouteToGate
        {
            get { return _routeToGate; }
            set
            {
                _currentIndexInRouteLine = 0;
                _isMovingToGate = true;
                _routeToGate = value;
            }
        }
        public int CurrentIndexInRouteLine { get { return _currentIndexInRouteLine; } set { _currentIndexInRouteLine = value; } }
        public InGameInventoryRuntimeData InGameInventoryData { get { return _inGameInventoryRuntimeData; } }
        #endregion

        protected override void Awake()
        {
            base.Awake();
            _factory = new EnemyStateFactory(this);
            _currentState = _factory.GetState(CharacterState.Idle);
            _currentState.EnterState();
        }
        protected override void OnTargetChanging(UnitBase.OnTargetChangingComposite composite)
        {
            base.OnTargetChanging(composite);

            bool isTargetValid = composite.Target == null;
            _isMovingToGate = isTargetValid;
            CheckAttackingOrWaiting();
        }

    }
}

using UnityEngine;

public class BaseAllyStateMachine : CharacterStateMachine
{
    private AllyStateFactory _factory;
    private bool _isAttack;
    private bool _isMovingToTarget;
    private UnitBase _target;
    private UserActionController _userActionController;

    private bool _isDie;

    #region Event
    protected override void OnEnable()
    {
        base.OnEnable();
        _unitBaseParent.OnDie += OnDie;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        _unitBaseParent.OnDie -= OnDie;
    }
    
    #endregion
    private void OnDie(bool isDie) => _isDie = isDie;

    #region Setter and Getter
    public bool IsDie { get { return _isDie; } }
    public AllyStateFactory StateFactory { get { return _factory; } }
    public bool IsAttack { get { return _isAttack; } }
    public bool IsMovingToTarget { get { return _isMovingToTarget; } }
    public UnitBase Target { get { return _target; } }
    public UserActionController UserActionController { get { return _userActionController; } }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        _userActionController = _unitBaseParent.UserActionController();
        _factory = new AllyStateFactory(this);
        _currentState = _factory.GetState(CharacterState.Idle);
        _currentState.EnterState();
    }
    protected override void OnTargetChanging(UnitBase.OnTargetChangingComposite composite)
    {
        base.OnTargetChanging(composite);
        
        _target = composite.Target;

        CheckingAttackOrApproaching(_target);
    }
    #region Logic checking
    private void CheckingAttackOrApproaching(UnitBase target)
    {
        if (target == null)
        {
            _isAttack = false;
            _isMovingToTarget = false;
            return;
        }
        
        var isInAttackRange = GameObjectUtility.Distance2dOfTwoGameObject(gameObject, target.gameObject) < _stats.GetCurrentStatValue(StatId.AttackRange);
        _isAttack = isInAttackRange;
        _isMovingToTarget = !isInAttackRange;
    }
    #endregion
}

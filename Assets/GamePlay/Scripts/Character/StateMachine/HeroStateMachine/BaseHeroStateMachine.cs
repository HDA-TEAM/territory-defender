using UnityEngine;

public class BaseHeroStateMachine : CharacterStateMachine
{
    private HeroStateFactory _factory;
    private bool _isAttack;
    private bool _isMovingToTarget;
    private UnitBase _target;
    
    private bool _isDie;

    #region Event
    protected virtual void OnEnable()
    {
        base.OnEnable();
        _unitBaseParent.OnDie += OnDie;
    }
    protected virtual void OnDisable()
    {
        base.OnDisable();
        _unitBaseParent.OnDie -= OnDie;
    }
    
    #endregion
    private void OnDie(bool isDie) => _isDie = isDie;

    #region Setter and Getter
    public bool IsDie { get { return _isDie; } }
    public HeroStateFactory StateFactory { get { return _factory; } }
    public bool IsAttack { get { return _isAttack; } }
    public bool IsMovingToTarget { get { return _isMovingToTarget; } }
    public UnitBase Target { get { return _target; } }
    
    #endregion
    protected override void Awake()
    {
        _factory = new HeroStateFactory(this);
        _currentState = _factory.GetState(CharacterState.Idle);
        _currentState.EnterState();
    }
    protected override void OnTargetChanging(UnitBase target)
    {
        base.OnTargetChanging(target);
                
        _target = target;

        CheckingAttackOrMoving(_target);
    }
    #region Logic checking
    private void CheckingAttackOrMoving(UnitBase target)
    {
        if (target == null)
        {
            _isAttack = false;
            _isMovingToTarget = false;
            return;
        }
        
        var isInAttackRange = GameObjectUtility.Distance2dOfTwoGameObject(gameObject, target.gameObject) < _stats.GetStat(StatId.AttackRange);
        _isAttack = isInAttackRange;
        _isMovingToTarget = !isInAttackRange;
    }
    #endregion
}

using UnityEngine;

public class BaseHeroStateMachine : CharacterStateMachine
{
    [SerializeField] private LineRenderer _routeToGate;
    
    private HeroStateFactory _factory;
    private bool _isAttack;
    private bool _isMoving;
    private UnitBase _target;
    
    #region Setter and Getter
    public HeroStateFactory StateFactory { get { return _factory; } }
    public bool IsAttack { get { return _isAttack; } }
    public bool IsMoving { get { return _isMoving; } }
    public UnitBase Target { get { return _target; } }
    
    #endregion
    protected override void Awake()
    {
        _factory = new HeroStateFactory(this);
        _currentState = _factory.GetState(CharacterState.Moving);
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
            _isMoving = false;
            return;
        }
        
        var isInAttackRange = GameObjectUtility.Distance2dOfTwoGameObject(gameObject, target.gameObject) < _stats.GetStat(StatId.AttackRange);
        _isAttack = isInAttackRange;
        _isMoving = !_isAttack;
    }
    #endregion
}

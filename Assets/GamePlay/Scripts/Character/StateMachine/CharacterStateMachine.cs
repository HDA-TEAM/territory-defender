using UnityEngine;

public class  CharacterStateMachine : UnitBaseComponent
{
    [SerializeField] private string _curStateLabel;
    [SerializeField] protected TroopBehaviourType _troopBehaviourType;
    [SerializeField] protected Animator _animator;
    protected CharacterBaseState _currentState;
    protected StatsHandlerComponent _stats;
    [SerializeField] private ProjectileDataAsset _projectileDataAsset;
    [SerializeField] private UnitId.Projectile _projectileId;
    [SerializeField] private BeingTargetCommand _beingTargetCommand;
    [SerializeField] private Transform _startAttackPoint;
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
    public UnitBase CurrentTarget { get { return _unitBaseParent.CurrentTarget; } }
    public BeingTargetCommand BeingTargetCommand { get { return _beingTargetCommand; } }
    public ProjectileDataAsset CharacterProjectileDataAsset { get { return _projectileDataAsset; } }
    public UnitId.Projectile CharacterProjectileIUnitId { get { return _projectileId; } }
    public TroopBehaviourType CharacterTroopBehaviourType { get { return _troopBehaviourType; } }
    public Animator CharacterAnimator { get { return _animator; } }
    public StatsHandlerComponent CharacterStats { get { return _stats; } }
    #endregion
    protected override void Awake()
    {
        _stats = _unitBaseParent.UnitStatsHandlerComp();
    }
    protected void Update() => _currentState.UpdateStates();

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

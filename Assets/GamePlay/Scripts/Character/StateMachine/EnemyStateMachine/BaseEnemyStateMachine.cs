using UnityEngine;

public class BaseEnemyStateMachine : CharacterStateMachine
{
    [SerializeField] private LineRenderer _routeToGate;
    [SerializeField] private InGameInventoryDataAsset _inGameInventoryDataAsset;
    
    private EnemyStateFactory _factory;
    private int _currentIndexInRouteLine;
    private bool _isMovingToGate;
    private bool _isDie;
    private bool _isStopToAttack;

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
    public EnemyStateFactory StateFactory{ get { return _factory; } }
    public bool IsDie { get { return _isDie; } }
    public bool IsStopToAttack { get { return _isStopToAttack; } }
    public bool IsMovingToGate { get { return _isMovingToGate; } }
    public LineRenderer RouteToGate { get { return _routeToGate; } set {
            _currentIndexInRouteLine = 0;
            _isMovingToGate = true;
            _routeToGate = value; }}
    public int CurrentIndexInRouteLine { get { return _currentIndexInRouteLine;} set { _currentIndexInRouteLine = value; } }
    public InGameInventoryDataAsset InGameInventoryData{ get { return _inGameInventoryDataAsset; } }
    #endregion
    
    protected override void Awake()
    {
        _factory = new EnemyStateFactory(this);
        _currentState = _factory.GetState(CharacterState.Idle);
        _currentState.EnterState();
    }
    protected override void OnTargetChanging(UnitBase.OnTargetChangingComposite composite)
    {
        base.OnTargetChanging(composite);
        
        bool isTargetValid = composite.Target == null;
        _isMovingToGate = isTargetValid;
        _isStopToAttack = !isTargetValid;
    }

}

using UnityEngine;

public class BaseEnemyStateMachine : CharacterStateMachine
{
    [SerializeField] private LineRenderer _routeToGate;
    [SerializeField] private InGameInventoryDataAsset _inGameInventoryDataAsset;
    
    private EnemyStateFactory _factory;
    private int _currentIndexInRouteLine = 0;
    private bool _isMovingToGate;
    
    #region Setter and Getter
    public EnemyStateFactory StateFactory{ get { return _factory; } }
    public bool IsMovingToGate { get { return _isMovingToGate; } }
    public LineRenderer RouteToGate { get { return _routeToGate; } set {
            _currentIndexInRouteLine = 0;
            _isMovingToGate = true;
            _routeToGate = value; } }
    public int CurrentIndexInRouteLine { get { return _currentIndexInRouteLine;} set { _currentIndexInRouteLine = value; } }
    public InGameInventoryDataAsset InGameInventoryData{ get { return _inGameInventoryDataAsset; } }
    #endregion
    
    protected override void Awake()
    {
        _factory = new EnemyStateFactory(this);
        _currentState = _factory.GetState(CharacterState.Moving);
        _currentState.EnterState();
    }
}

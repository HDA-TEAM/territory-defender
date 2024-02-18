public class BaseTowerStateMachine : CharacterStateMachine
{
    public bool IsAttack { get; set; }
    
    private TowerStateFactory _factory;

    #region Event
    protected virtual void OnEnable()
    {
        base.OnEnable();
    }
    protected virtual void OnDisable()
    {
        base.OnDisable();
    }
    
    #endregion
    
    #region Setter and Getter
    public TowerStateFactory StateFactory{ get { return _factory; } }
    
    #endregion
    
    protected override void Awake()
    {
        base.Awake();
        _factory = new TowerStateFactory(this);
        _currentState = _factory.GetState(CharacterState.Idle);
        _currentState.EnterState();
    }
    protected override void OnTargetChanging(UnitBase.OnTargetChangingComposite composite)
    {
        base.OnTargetChanging(composite);
        
        bool isTargetValid = composite.Target == null;
        IsAttack = !isTargetValid;
    }
}

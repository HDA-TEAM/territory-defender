public abstract class CharacterBaseState
{
    private bool _isRootState = false;
    private readonly CharacterStateMachine _context;
    private CharacterBaseState _currentSubState;
    private CharacterBaseState _currentSuperState;

    protected bool IsRootState { set { _isRootState = value; } }
    protected CharacterStateMachine Context { get { return _context; } }
    // private CharacterStateFactory _factory;
    // protected CharacterStateFactory Factory { get { return _factory; } }
    
    protected CharacterBaseState(CharacterStateMachine currentContext, CharacterStateFactory characterStateFactory)
    {
        _context = currentContext;
    }
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchState();
    public abstract void InitializeSubState();

    public void UpdateStates()
    {
        UpdateState();
        if (_currentSubState != null)
            _currentSubState.UpdateState();
    }
    public void SwitchState(CharacterBaseState newState)
    {
        // Current state exits state
        ExitState();
        
        newState.EnterState();

        if (_isRootState)
            _context.CurrentState = newState;
        else if(_currentSuperState != null)
            _currentSubState.SetSubState(newState);
    }
    protected void SetSuperState(CharacterBaseState newSuperState) => _currentSuperState = newSuperState;
    protected void SetSubState(CharacterBaseState newSubState) => _currentSubState = newSubState;
}

using System;
using UnityEngine;

public class CharacterStateMachine : UnitBaseComponent
{
    [SerializeField] protected Animator _animator;
    [SerializeField] protected CharacterBaseState _currentState;
    [SerializeField] protected Stats _stats;
    
    #region Setter and getter
    public CharacterBaseState CurrentState
    {
        set { _currentState = value;}
        get { return _currentState; }
    }
    public Animator CharacterAnimator
    {
        set { _animator = value;}
        get { return _animator; }
    }
    public Stats CharacterStats
    {
        set { _stats = value;}
        get { return _stats; }
    }
    
    // public bool IsAttack() => _isAttack;
    #endregion
    protected virtual void Awake() {}
    protected void Update()
    {
        _currentState.UpdateStates();
    }
    
    private void OnEnable()
    {
        _unitBaseParent.OnTargetChanging += OnTargetChanging;
    }
    private void OnDisable()
    {
        _unitBaseParent.OnTargetChanging -= OnTargetChanging;
    }
    protected virtual void OnTargetChanging(UnitBase unitBase) { }
}

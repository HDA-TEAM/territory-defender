using System;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterStateMachine : UnitBaseComponent
{
    [FormerlySerializedAs("_attackingType")]
    [SerializeField] protected TroopBehaviourType _troopBehaviourType;
    [SerializeField] protected Animator _animator;
    [SerializeField] protected CharacterBaseState _currentState;
    [SerializeField] protected Stats _stats;
    [SerializeField] private ProjectileDataAsset _projectileDataAsset;
    [SerializeField] private UnitId _projectileId;
    [SerializeField] private UnitBase _curTarget;
    #region Setter and getter
    public CharacterBaseState CurrentState
    {
        set { _currentState = value;}
        get { return _currentState; }
    }
    public UnitBase CurrentTarget { get { return _curTarget; } }
    public ProjectileDataAsset CharacterProjectileDataAsset { get { return _projectileDataAsset; } }
    public UnitId CharacterProjectileIUnitId { get { return _projectileId; } }
    public TroopBehaviourType CharacterTroopBehaviourType { get { return _troopBehaviourType; } }
    public Animator CharacterAnimator { get { return _animator; } }
    public Stats CharacterStats { get { return _stats; } }
    
    // public bool IsAttack() => _isAttack;
    #endregion
    protected virtual void Awake() {}
    protected void Update()
    {
        _currentState.UpdateStates();
    }
    
    protected virtual void OnEnable()
    {
        _unitBaseParent.OnTargetChanging += OnTargetChanging;
    }
    protected virtual void OnDisable()
    {
        _unitBaseParent.OnTargetChanging -= OnTargetChanging;
    }
    protected virtual void OnTargetChanging(UnitBase unitBase)
    {
        _curTarget = unitBase;
    }
}

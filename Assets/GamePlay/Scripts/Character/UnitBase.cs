using System;
using UnityEngine;
using UnityEngine.Serialization;

public class UnitBase : MonoBehaviour
{
    #region Component
    [SerializeField] private CharacterStateMachine _characterStateMachine;
    [SerializeField] private TargetDetecting _targetDetecting;
    [SerializeField] private HealthComp _healthComp;
    [SerializeField] private AttackingComp _attackingComp;
    [SerializeField] private CheckingCombatJoinInComp _checkingCombatJoinInComp;
    [SerializeField] private Stats _unitStatsComp;
    [SerializeField] private EnemyReachingDestination _enemyReachingDestination;
    #endregion

    #region Access
    protected CharacterStateMachine CharacterStateMachine() => _characterStateMachine;
    protected TargetDetecting TargetDetecting() => _targetDetecting;
    public HealthComp HealthComp() => _healthComp;
    public AttackingComp AttackingComp() => _attackingComp;
    public CheckingCombatJoinInComp CheckingCombatJoinIn() => _checkingCombatJoinInComp;
    public EnemyReachingDestination EnemyReachingDestinationComp => _enemyReachingDestination;
    public Stats UnitStatsComp() => _unitStatsComp;
    #endregion

    #region Validate
    private void OnValidate()
    {
        _characterStateMachine ??= GetComponent<CharacterStateMachine>(); 
        _targetDetecting ??= GetComponent<TargetDetecting>(); 
        _healthComp ??= GetComponent<HealthComp>(); 
        _attackingComp ??= GetComponent<AttackingComp>();
        _checkingCombatJoinInComp ??= GetComponent<CheckingCombatJoinInComp>();
    }
    #endregion



    #region Event
    public Action<UnitBase> OnTargetChanging;
    public Action OnUpdateStats;
    #endregion
}

public class UnitBaseComponent : MonoBehaviour
{
    [SerializeField] protected UnitBase _unitBaseParent;
    private void OnValidate() => _unitBaseParent ??= GetComponent<UnitBase>();
    protected virtual void StatsUpdate()
    {
        
    }
    private void Awake()
    {
        _unitBaseParent.OnUpdateStats += StatsUpdate;
    }
    private void OnDestroy()
    {
        _unitBaseParent.OnUpdateStats -= StatsUpdate;
    }
}

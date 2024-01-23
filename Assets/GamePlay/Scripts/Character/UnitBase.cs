using System;
using UnityEngine;
using UnityEngine.Serialization;

public class UnitBase : MonoBehaviour
{
    #region Component
    [SerializeField] private UnitController _unitController;
    [SerializeField] private CharacterStateMachine _characterStateMachine;
    // [SerializeField] private DefenderDetecting _defenderDetecting;
    [SerializeField] private HealthComp _healthComp;
    [SerializeField] private CheckingCombatJoinInComp _checkingCombatJoinInComp;
    [SerializeField] private Stats _unitStatsComp;
    [SerializeField] private EnemyReachingDestination _enemyReachingDestination;
    [SerializeField] private TargetChallenging _targetChallenging;
    [SerializeField] private UnitShowingInformation _unitShowingInformation;
    [SerializeField] private UnitType _unitType;
    #endregion
    public enum UnitType
    {
        Tower = 1,
        Hero = 2,
        Melee = 3,
        Range = 4,
        Mixed = 5,
    }
    #region Access
    public UnitController UnitController() => _unitController;
    protected CharacterStateMachine CharacterStateMachine() => _characterStateMachine;
    public HealthComp HealthComp() => _healthComp;
    public UnitType UnitTypeId() => _unitType;
    
    // public DefenderDetecting DefenderDetecting() => _defenderDetecting;
    public CheckingCombatJoinInComp CheckingCombatJoinIn() => _checkingCombatJoinInComp;
    public EnemyReachingDestination EnemyReachingDestinationComp => _enemyReachingDestination;
    public TargetChallenging TargetChallengingComp() => _targetChallenging;
    public UnitShowingInformation UnitShowingInformationComp() => _unitShowingInformation;
    public Stats UnitStatsComp() => _unitStatsComp;
    #endregion

    #region Validate
    private void OnValidate()
    {
        _characterStateMachine ??= GetComponent<CharacterStateMachine>(); 
        _healthComp ??= GetComponent<HealthComp>(); 
        // _attackingComp ??= GetComponent<AttackingComp>();
        _checkingCombatJoinInComp ??= GetComponent<CheckingCombatJoinInComp>();
    }
    #endregion
    
    public UnitState GetUnitState()
    {
        return UnitState.Free;
    }
    public enum UnitState
    {
        Free = 0
    }
    #region Event
    public UnitBase CurrentTarget;
    public Action<UnitBase> OnOutOfHeal;
    public Action<OnTargetChangingComposite> OnTargetChanging;
    public Action OnRecheckTarget;
    public Action OnResetFindTarget;
    public Action<bool> OnDie;
    public Action OnUpdateStats;
    public struct OnTargetChangingComposite
    {
        public UnitBase Target;
        public BeingTargetCommand BeingTargetCommand;
        public void SetDefault()
        {
            Target = null;
            BeingTargetCommand = BeingTargetCommand.None;
        }
    }
    
    #endregion
}

public class UnitBaseComponent : MonoBehaviour
{
    [SerializeField] protected UnitBase _unitBaseParent;
    private void OnValidate() => _unitBaseParent ??= GetComponent<UnitBase>();
    
    public UnitBase UnitBaseParent()=> _unitBaseParent;
    protected virtual void StatsUpdate()
    {
        
    }
    protected void Awake()
    {
        _unitBaseParent.OnUpdateStats += StatsUpdate;
    }
    private void OnDestroy()
    {
        _unitBaseParent.OnUpdateStats -= StatsUpdate;
    }
}

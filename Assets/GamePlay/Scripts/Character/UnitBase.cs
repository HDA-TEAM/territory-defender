using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    #region Component
    [SerializeField] private List<UnitId.BaseId> _targetSidesNeeding;
    [SerializeField] private UnitController _unitController;
    [SerializeField] private CharacterStateMachine _characterStateMachine;
    [SerializeField] private HealthComp _healthComp;
    [SerializeField] private StatsHandlerComponent _unitStatsComp;
    [SerializeField] private UnitShowingInformation _unitShowingInformation;
    [SerializeField] private UnitType _unitType;
    [SerializeField] private UserActionController _userActionController;
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
    public List<UnitId.BaseId> TargetSideNeeding() => _targetSidesNeeding;
    protected CharacterStateMachine CharacterStateMachine() => _characterStateMachine;
    public HealthComp HealthComp() => _healthComp;
    public UnitType UnitTypeId() => _unitType;
    public UserActionController UserActionController()
    {
        return _userActionController;
    }
    public UnitShowingInformation UnitShowingInformationComp() => _unitShowingInformation;
    public StatsHandlerComponent UnitStatsHandlerComp() => _unitStatsComp;
    #endregion

    #region Validate
    private void OnValidate()
    {
        _characterStateMachine ??= GetComponent<CharacterStateMachine>(); 
        _healthComp ??= GetComponent<HealthComp>(); 
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
    public Action<bool> OnDie;
    public Action OnUpdateStats;
    public Action OnUpdateBuffs;
    public struct OnTargetChangingComposite
    {
        public UnitBase Target;
        public BeingTargetCommand BeingTargetCommand;
        public EUserAction EUserAction;
        public void SetDefault()
        {
            Target = null;
            BeingTargetCommand = BeingTargetCommand.None;
        }
    }
    
    #endregion
}

public abstract class UnitBaseComponent : MonoBehaviour
{
    [SerializeField] protected UnitBase _unitBaseParent;
    private void OnValidate() => _unitBaseParent ??= GetComponent<UnitBase>();
    
    public UnitBase UnitBaseParent()=> _unitBaseParent;
    
    // Auto call to syn data
    protected virtual void StatsUpdate() {}
    protected virtual void BuffUpdate() {}
    
    protected virtual void Awake()
    {
        _unitBaseParent.OnUpdateBuffs += BuffUpdate;
        _unitBaseParent.OnUpdateStats += StatsUpdate;
    }
    protected virtual void Start()
    {
    }
    protected virtual void OnDestroy()
    {
        _unitBaseParent.OnUpdateBuffs -= BuffUpdate;
        _unitBaseParent.OnUpdateStats -= StatsUpdate;
    }
}

using Common.Scripts;
using GamePlay.Scripts.Character;
using GamePlay.Scripts.Character.StateMachine;
using GamePlay.Scripts.Character.StateMachine.TowerBehaviour;
using GamePlay.Scripts.Character.UnitController;
using GamePlay.Scripts.GamePlayController;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    #region Component
    [SerializeField] private UnitId.BaseId _unitSideId;
    [SerializeField] private List<UnitId.BaseId> _targetSidesNeeding;
    [SerializeField] private UnitController _unitController;
    [SerializeField] private CharacterStateMachine _characterStateMachine;
    [SerializeField] private HealthComp _healthComp;
    [SerializeField] private StatsHandlerComponent _unitStatsComp;
    [SerializeField] private UnitShowingInformation _unitShowingInformation;
    [SerializeField] private UserActionController _userActionController;
    [SerializeField] private UnitReviveHandler _unitReviveHandler;
    [SerializeField] private AnimationController _animationController;
    [SerializeField] private TowerBehaviourBase _towerBehaviourBase;
    #endregion

    #region Access
    public string UnitId;
    public UnitId.BaseId UnitSide => _unitSideId;
    public UnitController UnitController() => _unitController;
    public List<UnitId.BaseId> TargetSideNeeding() => _targetSidesNeeding;
    public CharacterStateMachine CharacterStateMachine() => _characterStateMachine;
    public AnimationController AnimationController() => _animationController;
    public HealthComp HealthComp() => _healthComp;
    public UserActionController UserActionController() => _userActionController;
    public TowerBehaviourBase TowerBehaviourBase() => _towerBehaviourBase;
    public UnitReviveHandler UnitReviveHandlerComp() => _unitReviveHandler;
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

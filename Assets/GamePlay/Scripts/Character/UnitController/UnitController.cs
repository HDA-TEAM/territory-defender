using System.Collections.Generic;

public class UnitController : UnitBaseComponent
{
    private void OnEnable()
    {
        if (UnitManager.IsAlive())
            UnitManager.Instance.Subscribe(_unitBaseParent);
    }
    protected virtual void OnDisable()
    {
        if (UnitManager.IsAlive())
            UnitManager.Instance.UnSubscribe(_unitBaseParent);
    }
    public virtual void UpdateStatus(List<UnitBase> targets)
    {
        _unitBaseParent.CharacterStateMachine().UpdateStateMachine();
        if (!IsSelfAvailableTargeting())
            return;

        if (!IsCurrentTargetAvailable())
        {
            SetDefaultState();
            return;
        }
        
        float nearestUnit = float.MaxValue;
        UnitBase target = null;
        foreach (var unit in targets)
        {
            float betweenDistance = GameObjectUtility.Distance2dOfTwoGameObject(unit.gameObject, gameObject);

            if (betweenDistance < _unitBaseParent.UnitStatsHandlerComp().GetCurrentStatValue(StatId.DetectRange))
            {
                if (nearestUnit > betweenDistance)
                {
                    nearestUnit = betweenDistance;
                    target = unit;
                }
            }
        }

        var defenderTargetChangingComposite = new UnitBase.OnTargetChangingComposite
        {
            Target = target,
            BeingTargetCommand = BeingTargetCommand.Block
        };
        _unitBaseParent.OnTargetChanging?.Invoke(defenderTargetChangingComposite);
        
    }
    private bool IsSelfAvailableTargeting() => _unitBaseParent.CurrentTarget == null;

    protected bool IsCurrentTargetAvailable()
    {
        UnitBase target = _unitBaseParent.CurrentTarget;
        return target != null && target.gameObject.activeSelf;
    }
    protected bool IsUnitAvailable(UnitBase unitBase)
    {
        return unitBase != null && unitBase.gameObject.activeSelf;
    }
    protected void SetDefaultState()
    {
        var targetChangingComposite = new UnitBase.OnTargetChangingComposite
        {
            Target = null,
            BeingTargetCommand = BeingTargetCommand.None
        };
        _unitBaseParent.OnTargetChanging?.Invoke(targetChangingComposite);
    }
    protected void OnChangeTarget(UnitBase target, BeingTargetCommand beingTargetCommand)
    {
        var defenderTargetChangingComposite = new UnitBase.OnTargetChangingComposite
        {
            Target = target,
            BeingTargetCommand = BeingTargetCommand.None
        };
        _unitBaseParent.OnTargetChanging?.Invoke(defenderTargetChangingComposite);
    }
}

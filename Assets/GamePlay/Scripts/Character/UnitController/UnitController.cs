using System.Collections.Generic;

public class UnitController : UnitBaseComponent
{
    private void OnEnable()
    {
        if (UnitManager.IsAlive())
            UnitManager.Instance.Subscribe(_unitBaseParent);
    }
    private void OnDisable()
    {
        if (UnitManager.IsAlive())
            UnitManager.Instance.UnSubscribe(_unitBaseParent);
    }
    public virtual void UpdateStatus(List<UnitBase> targets)
    {

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

            if (betweenDistance < _unitBaseParent.UnitStatsComp().GetStat(StatId.DetectRange))
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
    protected bool IsSelfAvailableTargeting() => _unitBaseParent.CurrentTarget == null;

    protected bool IsCurrentTargetAvailable()
    {
        UnitBase target = _unitBaseParent.CurrentTarget;
        return target != null && target.gameObject.activeSelf;
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
}

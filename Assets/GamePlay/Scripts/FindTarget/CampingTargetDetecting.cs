using SuperMaxim.Messaging;
using System;
using System.Collections.Generic;
using UnityEngine;

public interface IFindTarget
{
    public void FindTarget();
}
public class CampingTargetDetecting : UnitBaseComponent, IFindTarget
{
    [SerializeField] private List<UnitBase> _allyUnits = new List<UnitBase>();
    [SerializeField] private CircleCollider2D _circleCollider2D;
    [SerializeField] private CharacterSide _characterSideNeedToTarget;
    
    private readonly List<UnitBase> _targets = new List<UnitBase>();
    private Transform _campingTransform;
    private float _rangeDetecting;

    public List<UnitBase> Targets => _targets;
    public List<UnitBase> AllyUnits => _allyUnits;
    public Transform CampingTransform => _campingTransform;
    public float RangeDetecting => _rangeDetecting;
    public bool CheckIsUnitInRoom(UnitBase unitBase)
    {
        return _targets.Contains(unitBase);
    }
    private void UpdateDetectRange()
    {
        _rangeDetecting = _unitBaseParent.UnitStatsComp().GetStat(StatId.DetectRange);
        _circleCollider2D.radius = _rangeDetecting;
    }
    protected new void Awake()
    {
        base.Awake();
        UpdateDetectRange();
        
    }
    private void OnEnable()
    {
        UnitObserver.Instance.CampingRoomController.OnRegister(this);
    }
    private void OnDisable()
    {
        UnitObserver.Instance?.CampingRoomController.OnUnRegister(this);
    }
    public void FindTarget()
    {
        int targetIndex = 0;
        for (int i = 0; i < _allyUnits.Count; i++)
        {
            _allyUnits[i].CurrentTarget = _targets[targetIndex];
            if (_allyUnits.Count == targetIndex)
            {
                // reset index: multiple ally can attack single enemy
                targetIndex = 0;
            }
            else
            {
                targetIndex++;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag(_characterSideNeedToTarget.ToString()))
            return;

        if (other.gameObject.activeSelf || GameObjectUtility.Distance2dOfTwoGameObject(gameObject, other.gameObject) < _rangeDetecting)
        {
            UnitBase target = other.gameObject.GetComponent<UnitBase>();
            if (target != null && !_targets.Exists((t) => t == target))
            {
                _targets.Add(target);
                target.OnResetFindTarget?.Invoke();
            }
        }
        else
            TryRemoveFromTargetList(other);
    }
    /// Handle case object is deActive
    private void OnTriggerExit2D(Collider2D other)
    {
        // Debug.Log("other.gameObject exit " + other.tag);
        if (other.gameObject.CompareTag(_characterSideNeedToTarget.ToString()))
            TryRemoveFromTargetList(other);
    }
    private void TryRemoveFromTargetList(Collider2D other)
    {
        UnitBase target = other.gameObject.GetComponent<UnitBase>();
        if (target != null && _targets.Exists((t) => t == target))
            _targets.Remove(target);

    }
    
}

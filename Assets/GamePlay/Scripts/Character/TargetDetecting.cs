using System.Collections.Generic;
using UnityEngine;

public enum CharacterSide
{
    Ally = 1,
    Enemy = 2,
}

public class TargetDetecting : UnitBaseComponent
{
    [SerializeField] private CircleCollider2D _circleCollider2D; 
    [SerializeField] private CharacterSide _characterSideNeedToTarget;
    [SerializeField] private bool _isTower;
    private readonly List<UnitBase> _targets = new List<UnitBase>();
    private UnitBase _curTarget;
    private float _rangeDetecting;

    private void UpdateDetectRange()
    {
        _rangeDetecting = _unitBaseParent.UnitStatsComp().GetStat(StatId.DetectRange);
        _circleCollider2D.radius = _rangeDetecting;
    }
    protected void Awake()
    {
        base.Awake();
        UpdateDetectRange();
    }
    private void Update() => CheckingTarget();
    
    private void CheckingTarget()
    {
        _curTarget = null;
        if (_targets.Count > 0)
        {
            if (_targets[0] == null || !_targets[0].gameObject.activeSelf)
                _targets.RemoveAt(0);
            else
            {
                _curTarget = _targets[0];
                // Tower don't challenges
                if (!_isTower)
                {
                    _curTarget.TargetChallengingComp().SetChallenger(_unitBaseParent);
                }
            }
        }
        else
            _curTarget = null;

        _unitBaseParent.OnTargetChanging?.Invoke(_curTarget);

    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag(_characterSideNeedToTarget.ToString()))
            return;

        if (other.gameObject.activeSelf || GameObjectUtility.Distance2dOfTwoGameObject(gameObject, other.gameObject) < _rangeDetecting)
        {
            UnitBase target = other.gameObject.GetComponent<UnitBase>();
            if (target != null && !_targets.Exists((t) => t == target))
                _targets.Add(target);
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

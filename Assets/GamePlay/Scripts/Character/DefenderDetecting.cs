using System;
using UnityEngine;

public enum UnitSide
{
    Ally = 1,
    Enemy = 2,
}
public class DefenderDetecting : UnitBaseComponent
{
    [SerializeField] private CircleCollider2D _circleCollider2D; 
    [SerializeField] private UnitSide _UnitSideNeedToTarget;
    private float _rangeDetecting;

    private Action<UnitBase> _onDetectSuspect;
    private Action<UnitBase> _onSuspectOut;
    private Transform _campingPlace;
    public void Setup(DefenseMilitaryComposite defenseMilitaryComposite)
    {
        _campingPlace = defenseMilitaryComposite.CampingPlace;
        _onDetectSuspect = defenseMilitaryComposite.OnDetectSuspect;
        _onSuspectOut = defenseMilitaryComposite.OnSuspectOut;
    }
    public void SetCampingPlace()
    {
        
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
    // private void OnEnable()
    // {
    //     UnitObserver.Instance.SingleTargetController.OnRegister(this);
    // }
    // private void OnDisable()
    // {
    //     UnitObserver.Instance?.SingleTargetController.OnUnRegister(this);
    // }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag(_UnitSideNeedToTarget.ToString()))
            return;
        
        if (other.gameObject.activeSelf || GameObjectUtility.Distance2dOfTwoGameObject(_campingPlace.gameObject, other.gameObject) < _rangeDetecting)
        {
            UnitBase target = other.gameObject.GetComponent<UnitBase>();
            _onDetectSuspect?.Invoke(target);
        }
        else
            TryRemoveFromTargetList(other);
    }
    /// Handle case object is deActive
    private void OnTriggerExit2D(Collider2D other)
    {
        // Debug.Log("other.gameObject exit " + other.tag);
        if (other.gameObject.CompareTag(_UnitSideNeedToTarget.ToString()))
            TryRemoveFromTargetList(other);
    }
    private void TryRemoveFromTargetList(Collider2D other)
    {
        UnitBase target = other.gameObject.GetComponent<UnitBase>();
        _onSuspectOut?.Invoke(target);
        target.OnResetFindTarget?.Invoke();
    }
}


using UnityEngine;

public class EnemyMovement : UnitBaseComponent
{
    [SerializeField] private LineRenderer routeToGate;
    [SerializeField] private float movementSpeed;
    public LineRenderer RouteToGate
    {
        get
        {
            return routeToGate; 
        }
        set
        {
            currentIndexInRouteLine = 0;
            routeToGate = value;
        }
    } 

    private int currentIndexInRouteLine = 0;

    private bool IsMovingToGate = true;
    
    #region Core
    private void OnEnable()
    {
        _unitBaseParent.OnTargetChanging += OnTargetChanging;
    }
    private void OnDisable()
    {
        _unitBaseParent.OnTargetChanging -= OnTargetChanging;
    }
    #endregion
    #region Data update
    private void Update()
    {
        if (IsMovingToGate == false || routeToGate == null)
            return;
        MovingToDestination();
    }
    private void OnTargetChanging(UnitBase target)
    {
        IsMovingToGate = (target == null);
    }
    protected override void StatsUpdate()
    {
        var stats = _unitBaseParent.UnitStatsComp();
        movementSpeed = stats.GetStat(StatId.MovementSpeed);
    }
    #endregion
    #region Moving Logic
    private void MovingToDestination()
    {
        if (IsReachedDestinationGate())
        {
            // remove route to stop moving
            routeToGate = null;
            _unitBaseParent.EnemyReachingDestinationComp.OnReachingDestination();
            return;
        }
        if (VectorUtility.IsTwoPointReached(
            gameObject.transform.position, 
            routeToGate.GetPosition(currentIndexInRouteLine)))
        {
            currentIndexInRouteLine += 1;
        }
        PlayMoving();
    }
    private bool IsReachedDestinationGate()
    {
        return (currentIndexInRouteLine == routeToGate.positionCount - 1);
    }
    private void PlayMoving()
    {
        this.gameObject.transform.position = VectorUtility.Vector3MovingAToB(
            gameObject.transform.position,
            routeToGate.GetPosition(currentIndexInRouteLine),
            movementSpeed);
    }
    #endregion
}

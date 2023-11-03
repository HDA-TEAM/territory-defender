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

    protected override void StatsUpdate()
    {
        var stats = _unitBaseParent.UnitStatsComp();
        movementSpeed = stats.GetStat(StatId.Movement);
    }
    
    private void OnEnable()
    {
        _unitBaseParent.OnCharacterChange += OnTargetChanging;
    }
    private void OnDisable()
    {
        _unitBaseParent.OnCharacterChange -= OnTargetChanging;
    }
    private void Update()
    {
        if (IsMovingToGate == false || routeToGate == null)
        {
            return;
        }
        MovingToDestination();
    }
    private void OnTargetChanging(UnitBase target)
    {
        IsMovingToGate = (target == null);
    }
    private void MovingToDestination()
    {
        if (IsReachedDestinationGate())
        {
            this.gameObject.SetActive(false);
            routeToGate = null;
            return;
            //todo 
            // reduce player heath
            // return pooler
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
        this.gameObject.transform.position =VectorUtility.Vector2MovingAToB(
            this.gameObject.transform.position,
            routeToGate.GetPosition(currentIndexInRouteLine),
            movementSpeed);
    }
}

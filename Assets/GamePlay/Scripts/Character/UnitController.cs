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
        
        if (!CheckTargetAvailable())
            return;
        float nearestUnit = float.MaxValue;
        UnitBase target = null;
        foreach (var unit in targets)
        {
            float betweenDistance = GameObjectUtility.Distance2dOfTwoGameObject(unit.gameObject, this.gameObject);
            
            if ( betweenDistance < _unitBaseParent.UnitStatsComp().GetStat(StatId.DetectRange))
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
    protected bool CheckTargetAvailable() => _unitBaseParent.CurrentTarget == null;
    // private Camera _camera;
    // private RaycastHit _raycastHit;
    // [SerializeField] private float _rayRadius;
    // public LayerMask enemyLayer;
    // private void Awake()
    // {
    //     _camera = Camera.main;
    // }
    private void Update()   
    {
        // Detect enemies within the specified radius
            // Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _rayRadius, enemyLayer);
            //
            // // Iterate through the colliders and do something with each detected enemy
            // foreach (Collider2D collider in colliders)
            // {
            //     // Check if the collider belongs to an enemy
            //     if (collider.CompareTag("Ally"))
            //     {
            //         // Do something with the detected enemy, for example, print its name
            //         Debug.Log("Detected enemy: " + collider.gameObject.name);
            //     }
            // }
    }
    
}

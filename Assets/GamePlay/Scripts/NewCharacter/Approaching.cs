using UnityEngine;

public class Approaching : MonoBehaviour
{
    [SerializeField] private UnitBase target;
    [SerializeField] private float movingSpeed;
    [SerializeField] private float attackingRange;
    public float MovingSpeed() => movingSpeed;

    private UnitBase _baseUnitBase;

    private void Awake()
    {
        Validate();
    }
    private void Validate()
    {
        if (_baseUnitBase == null)
        {
            _baseUnitBase = GetComponent<UnitBase>();
        }
    }
    private void OnEnable()
    {
        _baseUnitBase.OnCharacterChange += TargetApproaching;
    }
    private void OnDisable()
    {
        _baseUnitBase.OnCharacterChange -= TargetApproaching;
    }
    private void Start()
    {
        attackingRange = 5;
    }
   
    private void TargetApproaching(UnitBase target)
    {
        if (target == null)
            return;
        
        if (GameObjectUtility.Distance2dOfTwoGameObject(this.gameObject, target.gameObject) > attackingRange)
        {
            gameObject.transform.position = VectorUtility.Vector2MovingAToB(this.transform.position, target.transform.position, movingSpeed);
        }
    }
}

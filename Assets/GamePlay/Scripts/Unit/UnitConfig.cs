using UnityEngine;


public struct UnitAttribute
{
    public float attackRange;
    public float detectRange;
    public float health;
    public float movementSpeed;
    public float attackSpeed;
    public float attackDamage;
    public float attackCoolDown;
    // public UnitAttribute(float attackRange,float detectRange,float health,float movementSpeed,float attackSpeed,float attackDamage,float attackCoolDown = 0)
    // {
    public UnitAttribute(UnitConfig unitConfig)
    {
        this.attackRange = unitConfig.attackRange;
        this.detectRange = unitConfig.detectRange;
        this.health = unitConfig.health;
        this.movementSpeed = unitConfig.MovementSpeed;
        this.attackSpeed = unitConfig.attackSpeed;
        this.attackDamage = unitConfig.attackDamage;
        this.attackCoolDown = 0;
    }
    
}
[CreateAssetMenu(fileName = "Unit", menuName = "ScriptableObject/Unit/UnitBase")]
public class UnitConfig : ScriptableObject
{
    [Header("Range")]
    public float attackRange;
    public float detectRange;
    [Header("Health")]
    public float health;
    [Header("Speed")]
    [SerializeField] private float movementSpeed;
    public float attackSpeed;
    [Header("Dame")]
    public float attackDamage;
    // [Header("Skill")]
    // public float cooldown;
    public float MovementSpeed
    {
        get
        {
            return movementSpeed;
        }
    }
}

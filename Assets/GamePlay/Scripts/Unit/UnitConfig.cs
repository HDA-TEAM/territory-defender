using UnityEngine;

namespace GamePlay.Scripts.Unit
{
    public class UnitConfig : ScriptableObject
    {
        [Header("Range")]
        public float attackRange;
        public float detectRange;
        [Header("Health")]
        public float health;
        [Header("Speed")]
        public float movementSpeed;
        public float attackSpeed;
        [Header("Dame")]
        public float attackDamage;
        // [Header("Skill")]
        // public float cooldown;

    }
}

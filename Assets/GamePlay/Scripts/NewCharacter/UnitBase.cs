using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

// [RequireComponent(typeof(HealthComp),
//     typeof(HealthComp))]
public class UnitBase : MonoBehaviour
{
    [SerializeField] private CharacterStateSystem characterStateSystem;
    [SerializeField] private TargetDetecting targetDetecting;
    [SerializeField] private HealthComp healthComp;
    [SerializeField] private Attacking attacking;
    [SerializeField] private CheckingCombatJoinIn checkingCombatJoinIn;
    public CharacterStateSystem CharacterStateSystem () =>  characterStateSystem;
    public TargetDetecting TargetDetecting () => targetDetecting;
    public HealthComp HealthComp () =>  healthComp;
    public Attacking Attacking () =>  attacking;
    public CheckingCombatJoinIn CheckingCombatJoinIn () => checkingCombatJoinIn;

    #region Event
        public Action<UnitBase> OnCharacterChange;
    #endregion
    
    private void Reset()
    {
        if(characterStateSystem == null)
            characterStateSystem = GetComponent<CharacterStateSystem>();
        if(targetDetecting == null)
            targetDetecting = GetComponent<TargetDetecting>();
        if(healthComp == null)
            healthComp = GetComponent<HealthComp>();
        if(attacking == null)
            attacking = GetComponent<Attacking>();
        // if(characterStateSystem == null)
        //     characterStateSystem = GetComponent<CharacterStateSystem>();
        // if(targetDetecting == null)
        //     targetDetecting = GetComponent<TargetDetecting>();
        
        
    }
}
// #region Event
// public struct TargetChangingPayload
// {
//     public Character target;
// }
//     
// #endregion
public enum CharacterState
{
    Idle = 0,
    Moving = 1,
    Attacking = 2,
    Guarding = 3,
    TakingDame = 4,
    Die = 5
}
public class CharacterStateSystem : MonoBehaviour
{
    private CharacterState currentState;
    public CharacterState CurrentState() => currentState;
}

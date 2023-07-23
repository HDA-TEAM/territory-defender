using GamePlay.Scripts.Tower;
using SuperMaxim.Messaging;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Approaching : MonoBehaviour
{
    [SerializeField] private Character target;
    [SerializeField] private float movingSpeed;
    [SerializeField] private float attackingRange;
    public float MovingSpeed() => movingSpeed;

    private Character baseCharacter;

    private void Awake()
    {
        Validate();
    }
    private void Validate()
    {
        if (baseCharacter == null)
        {
            baseCharacter = GetComponent<Character>();
        }
    }
    private void OnEnable()
    {
        baseCharacter.OnCharacterChange += TargetApproaching;
    }
    private void OnDisable()
    {
        baseCharacter.OnCharacterChange -= TargetApproaching;
    }
    private void Start()
    {
        attackingRange = 5;
    }
   
    private void TargetApproaching(Character target)
    {
        if (target == null)
        {
            return;
        }
        if (GameObjectUtility.Distance2dOfTwoGameObject(this.gameObject, target.gameObject) > attackingRange)
        {
            gameObject.transform.position = VectorUtility.Vector2MovingAToB(this.transform.position, target.transform.position, movingSpeed);
        }
    }
}

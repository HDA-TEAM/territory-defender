using GamePlay.Scripts.Tower;
using SuperMaxim.Messaging;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private LineRenderer routeToGate;
    [SerializeField] private float movementSpeed;
    public LineRenderer RouteToGate() => routeToGate;

    private int currentIndexInRouteLine = 0;

    private bool IsMovingToGate = true;
    private Character baseCharacter;

    private void Awake()
    {
        Validate();
    }
    private void Validate()
    {
        if (baseCharacter == null)
            baseCharacter = GetComponent<Character>();
        // if (routeToGate == null)
        //     routeToGate = RouteSetController.Instance.CurrentRouteLineRenderers[0];
    }
    private void OnEnable()
    {
        baseCharacter.OnCharacterChange += OnTargetChanging;
    }
    private void OnDisable()
    {
        baseCharacter.OnCharacterChange -= OnTargetChanging;
    }
    private void Update()
    {
        if (IsMovingToGate == false)
        {
            return;
        }
        MovingToDestination();
    }
    private void OnTargetChanging(Character target)
    {
        IsMovingToGate = (target == null);
    }
    private void MovingToDestination()
    {
        if (IsReachedDestinationGate())
        {
            this.gameObject.SetActive(false);
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

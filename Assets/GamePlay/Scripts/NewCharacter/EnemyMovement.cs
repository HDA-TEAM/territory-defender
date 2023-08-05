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
    private UnitBase _baseUnitBase;

    private void Awake()
    {
        Validate();
    }
    private void Validate()
    {
        if (_baseUnitBase == null)
            _baseUnitBase = GetComponent<UnitBase>();
        // if (routeToGate == null)
        //     routeToGate = RouteSetController.Instance.CurrentRouteLineRenderers[0];
    }
    private void OnEnable()
    {
        _baseUnitBase.OnCharacterChange += OnTargetChanging;
    }
    private void OnDisable()
    {
        _baseUnitBase.OnCharacterChange -= OnTargetChanging;
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

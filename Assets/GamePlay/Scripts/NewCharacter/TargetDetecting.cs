using GamePlay.Scripts.Tower;
using SuperMaxim.Messaging;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public enum CharacterSide
{
    Ally = 1,
    Enemy = 2,
}

public class TargetDetecting : MonoBehaviour
{
    [SerializeField] private CharacterSide _characterSideNeedToTarget;
    [SerializeField] private float _rangeDetecting;
    private readonly List<UnitBase> _targets = new List<UnitBase>();
    private UnitBase _baseUnitBase;
    private UnitBase _curTarget;

    private void Awake()
    {
        Validate();
    }
    private void Start()
    {
    }
    private void Validate()
    {
        if (_baseUnitBase == null)
        {
            _baseUnitBase = GetComponent<UnitBase>();
        }
    }
    private void Update()
    {
        CheckingTarget();
    }
    private void CheckingTarget()
    {
        _curTarget = null;
        if (_targets.Count > 0)
        {
            if (_targets[0] == null)
                _targets.RemoveAt(0);
            else
            {
                //todo
                //add condition to Set target By CheckingCombatJoinIn
                _curTarget = _targets[0];
            }
        }
        else
            _curTarget = null;

        // if (prevTarget == curTarget)
        //     return;
        // else
        _baseUnitBase.OnCharacterChange?.Invoke(_curTarget);

    }
    private void OnTriggerStay2D(Collider2D other)
    {
        // Debug.Log("other.gameObject Stay " + other.tag);
        // Debug.Log(GameObjectUtility.Distance2dOfTwoGameObject(this.gameObject,other.gameObject));
        if (!other.gameObject.CompareTag(_characterSideNeedToTarget.ToString()))
            return;

        if (GameObjectUtility.Distance2dOfTwoGameObject(gameObject, other.gameObject) < _rangeDetecting)
        {
            UnitBase target = other.gameObject.GetComponent<UnitBase>();
            if (target != null && !_targets.Exists((t) => t == target))
            {
                _targets.Add(target);
                Debug.Log("target add ");
            }
        }
        else
            TryRemoveFromTargetList(other);
    }
    /// handle case object is deActive
    private void OnTriggerExit2D(Collider2D other)
    {
        // Debug.Log("other.gameObject exit " + other.tag);
        if (other.gameObject.CompareTag(_characterSideNeedToTarget.ToString()))
        {
            TryRemoveFromTargetList(other);
        }
    }
    private void TryRemoveFromTargetList(Collider2D other)
    {
        UnitBase target = other.gameObject.GetComponent<UnitBase>();
        if (target != null && _targets.Exists((t) => t == target))
        {
            _targets.Remove(target);
        }
    }
}

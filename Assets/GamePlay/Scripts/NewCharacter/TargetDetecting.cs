using GamePlay.Scripts.Tower;
using SuperMaxim.Messaging;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum CharacterSide
{
    Ally = 1,
    Enemy = 2,
}
public class TargetDetecting : MonoBehaviour
{
    [SerializeField] private CharacterSide characterSideNeedToTarget;
    [SerializeField] private float rangeDetecting;
    private List<UnitBase> targets = new List<UnitBase>();
    private UnitBase _baseUnitBase;
   
    private void Awake()
    {
        Validate();
    }
    private void Start()
    {
        rangeDetecting = 10;
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
    private UnitBase prevTarget = null;
    private void CheckingTarget()
    {
        UnitBase curTarget = null;
        if (targets.Count > 0)
        {
            if (targets[0] == null)
            {
                targets.RemoveAt(0);
            }
            else
            {
                //todo
                //add condition to Set target By CheckingCombatJoinIn
                curTarget = targets[0];
            }
        }
        else
        {
            curTarget = null;
        }
        // if (prevTarget == curTarget)
        //     return;
        // else
        _baseUnitBase.OnCharacterChange?.Invoke(curTarget); 

    }
    private void OnTriggerStay2D(Collider2D other)
    {
        // Debug.Log("other.gameObject Stay " + other.tag);
        // Debug.Log(GameObjectUtility.Distance2dOfTwoGameObject(this.gameObject,other.gameObject));
        if (other.gameObject.CompareTag(characterSideNeedToTarget.ToString())
            && GameObjectUtility.Distance2dOfTwoGameObject(this.gameObject,other.gameObject) < rangeDetecting)
        {
            UnitBase target = other.gameObject.GetComponent<UnitBase>();
            // Debug.Log("target " + target);
            if (target != null && !targets.Exists( (t) => t == target))
            {
                targets.Add(target);
                Debug.Log("target add ");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        // Debug.Log("other.gameObject exit " + other.tag);
        if (other.gameObject.CompareTag(characterSideNeedToTarget.ToString()))
        {
            UnitBase target = other.gameObject.GetComponent<UnitBase>();
            if (target != null && targets.Exists( (t) => t == target))
            {
                targets.Remove(target);
            }
        }
    }
}


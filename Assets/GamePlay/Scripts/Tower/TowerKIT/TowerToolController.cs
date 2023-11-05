using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerToolController : MonoBehaviour
{
    [SerializeField] private List<TowerToolBase> _towerToolBases;
    [SerializeField] private List<ConfirmHandle> _confirmHandles;
    public ConfirmHandle CurrentConfirmHandle;
    private ConfirmHandle _preConfirmHandle;
    private void Reset()
    {
        _towerToolBases.Clear();
        _confirmHandles.Clear();
        _towerToolBases = gameObject.GetComponentsInChildren<TowerToolBase>().ToList();
        foreach (TowerToolBase towerToolBase in _towerToolBases)
            towerToolBase.Reset();
        _confirmHandles = gameObject.GetComponentsInChildren<ConfirmHandle>().ToList();
    }
    private void Start()
    {
        foreach (var confirmHandle in _confirmHandles)
        {
            confirmHandle.SetUpSelected(SetCurrentSelectedConfirm);
        }
    }
    private void SetCurrentSelectedConfirm(ConfirmHandle confirmHandle)
    {
        if (confirmHandle != null)
        {
            confirmHandle.ResetToDefault();
        }
        CurrentConfirmHandle = confirmHandle;
        _preConfirmHandle = CurrentConfirmHandle;
    }

}

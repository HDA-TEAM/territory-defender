using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerToolController : MonoBehaviour
{
    [SerializeField] private List<TowerToolBase> _towerToolBases;

    private void Reset()
    {
        _towerToolBases.Clear();
        _towerToolBases = gameObject.GetComponentsInChildren<TowerToolBase>().ToList();
        foreach (TowerToolBase towerToolBase in _towerToolBases)
            towerToolBase.Reset();
    }
}

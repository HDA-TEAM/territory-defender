using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerKITManager : MonoBehaviour
{
    [SerializeField] private TowerToolController _selectionTool;
    [SerializeField] private TowerToolController _usingTool;

    private bool towerIsExist;
    
    private void ChangeKitState()
    {
        if (towerIsExist)
        {
            _selectionTool.gameObject.SetActive(false);
            _usingTool.gameObject.SetActive(true);
        }
        else
        {
            _selectionTool.gameObject.SetActive(true);
            _usingTool.gameObject.SetActive(false);
        }
    }

}


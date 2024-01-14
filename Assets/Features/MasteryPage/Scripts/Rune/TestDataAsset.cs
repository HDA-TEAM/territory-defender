
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TestDataAsset", menuName = "ScriptableObject/DataAsset/TestDataAsset")]
public class TestDataAsset : BaseDataAsset<TowerDataModel>
{
    public void InitData()
    {
        TowerSoSaver towerSoSaver = new TowerSoSaver()
        {
            _towerId = 0,
            _runeLevels = new List<RuneLevel>()
        };

        _model._towerList = new List<TowerSoSaver>();
        
        Debug.Log("Set default " + towerSoSaver);
        _model._towerList.Add(towerSoSaver);
    }
}



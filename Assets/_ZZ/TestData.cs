using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestData : MonoBehaviour
{
    [SerializeField] private TestDataAsset _testDataAsset;
    
    [ContextMenu("SaveData")]
    public void TestSaveData()
    {
        _testDataAsset.LoginDay = 3;
    }
    private void Start() => TestLoadData();

    [ContextMenu("LoadData")]
    public void TestLoadData()
    {
        _testDataAsset.LoadData();
        Debug.Log("login day" + _testDataAsset.LoginDay);
    }
}

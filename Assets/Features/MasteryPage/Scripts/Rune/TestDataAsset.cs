
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TestDataAsset", menuName = "ScriptableObject/DataAsset/TestDataAsset")]
public class TestDataAsset : BaseDataAsset<TestDataModel>
{
    public int LoginDay
    {
        get
        {
            return _model.LoginDay;
        }
        set
        {
            _model.LoginDay = value;
            SaveData();
        }
    }
}

[Serializable]
public struct TestDataModel : IDefaultCustom
{
    public int LoginDay;
    public bool IsEmpty()
    {
        return LoginDay == 0;
    }
    public void SetDefault()
    {
        LoginDay = 5;
    }
}



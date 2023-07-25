using GamePlay.Scripts.Tower;
using System;
using System.Collections.Generic;
using UnityEngine;


public enum PoolingTypeEnum
{
    Tower = 1,
    Ally = 100,
    Enemy = 200
}

public class UnitPooling : PoolingBase
{
        
}

[Serializable]
public struct PoolingComposite
{
    public int initNumber;
    public PoolingTypeEnum poolingType;
    public GameObject prefab;
}
public class PoolingManager : Singleton<PoolingManager>
{
    private Dictionary<PoolingTypeEnum,UnitPooling> dictUnitPooling;
    [SerializeField] private List<PoolingComposite> initPoolings;
    private void Start()
    {
        SetUp();
    }
    private void SetUp()
    {
        foreach (var pooling in initPoolings)
        {
            GameObject parent = GameObject.Find(pooling.poolingType.ToString());
            UnitPooling tmpPooling = new UnitPooling();
            tmpPooling.InitPoolWithParam(pooling.initNumber, pooling.prefab, parent);
            dictUnitPooling.Add(
                pooling.poolingType,
                tmpPooling
            );
        }
    }
}


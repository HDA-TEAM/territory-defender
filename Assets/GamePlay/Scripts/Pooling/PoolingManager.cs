using GamePlay.Scripts.Tower;
using System;
using System.Collections.Generic;
using UnityEngine;


public enum PoolingTypeEnum
{
    Tower = 1,
    Ally = 100,
    Enemy = 200,
    EnemyShieldMan = 201,
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
    [SerializeField] private GameObject poolingContainer;
    [SerializeField] private List<PoolingComposite> initPooling;
    
    private Dictionary<PoolingTypeEnum,UnitPooling> dictUnitPooling = new Dictionary<PoolingTypeEnum, UnitPooling>();
    
    private void Start()
    {
        SetUp();
    }
    private void SetUp()
    {
        foreach (var pooling in initPooling)
        {
            String poolingPath = $"/{poolingContainer.name}/{pooling.poolingType.ToString()}";
            Debug.Log("poolingPath " + pooling);
            GameObject parent =  GameObject.Find(poolingPath);
            UnitPooling tmpPooling = new UnitPooling();
            tmpPooling.InitPoolWithParam(pooling.initNumber, pooling.prefab, parent);
            dictUnitPooling.Add(
                pooling.poolingType,
                tmpPooling
            );
        }
    }
}


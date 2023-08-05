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


[Serializable]
public struct PoolingComposite
{
    public int initNumber;
    public PoolingTypeEnum poolingType;
    public GameObject prefab;
}
public class PoolingManager : Singleton<PoolingManager>
{
    [SerializeField] private List<UnitPooling> poolingList;
    private Dictionary<PoolingTypeEnum, PoolingBase> dictPooling = new Dictionary<PoolingTypeEnum, PoolingBase>();
    
    private void Start()
    {
        SetUp();
    }
    private void SetUp()
    {
        foreach (var pooling in poolingList)
        {
            PoolingComposite poolingComposite = pooling.UnitPool;
            pooling.InitPoolWithParam(poolingComposite.initNumber, poolingComposite.prefab, pooling.gameObject);
            dictPooling.Add(poolingComposite.poolingType,pooling);
        }
    }
    public PoolingBase GetPooling(PoolingTypeEnum poolingTypeEnum)
    {
        dictPooling.TryGetValue(poolingTypeEnum, out PoolingBase poolingBase );
        return poolingBase;
    }
    public GameObject SpawnObject(PoolingTypeEnum poolingType, Vector2 pos)
    {
        GameObject go = GetPooling(poolingType).GetInstance();
        go.SetActive(transform);
        go.transform.position = pos;
        return go;
    }
}

